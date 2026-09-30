using System;
using System.Collections.Generic;
using SGLib.Utility.Management.Component.Capabilities;
using UnityEngine;

namespace SGLib.AI.Behaviour.StateMachines.Component
{
    public class HFSMComponent : SG_Component, IStateMachineComponent
    {
        [SerializeField] private HierarchicalStateBase_So root;
        [SerializeField] private HierarchicalStateBase_So initialLeaf;
        [SerializeField] private StateComparerBase_So customStateComparer;
        [SerializeField] protected HierarchicalStateBase_So[] OwnedStates;

        private HierarchicalStateBase_So currentLeaf; // lower active state
        private HierarchicalStateBase_So previousLeaf; // previous lower active state
        private List<HierarchicalStateBase_So> currentPath; // reversed active path (from leaf to root)
        private IEqualityComparer<IStateSo> stateComparer;
        private bool isRunning;

        public override void Initialize()
        {
            currentPath = new List<HierarchicalStateBase_So>();

            stateComparer = customStateComparer != null
                ? customStateComparer
                : EqualityComparer<IStateSo>.Default;
        }

        public override void Activate()
        {
            Run();
        }

        public override void Deactivate()
        {
            Stop();
        }

        private void Update()
        {
            if (!isRunning) return;

            if (currentLeaf == null) return;

            // only the leaf checks transitions 
            // not all the nodes of the path.
            currentLeaf.EvaluateTransitions();
        }

        public void Run()
        {
            if (isRunning) return;

            Reset();

            foreach (var state in OwnedStates)
            {
                if (state == null) continue;
                state.Bind(this);
            }

            // from root to leaf
            for (int i = currentPath.Count - 1; i >= 0; i--)
            {
                var state = currentPath[i];
                if (state == null) continue;
                state.Enter();
            }

            isRunning = true;
        }

        public void Stop()
        {
            if (!isRunning) return;

            isRunning = false;

            foreach (var node in currentPath)
            {
                if (node == null) continue;
                node.Exit();
            }

            foreach (var state in OwnedStates)
            {
                if (state == null) continue;
                state.Unbind(this);
            }

            currentLeaf = null;
        }

        public void Reset()
        {
            currentLeaf = initialLeaf;
            SetCurrentPath(currentLeaf);
        }


        public void TransitionTo(IStateSo newLeaf, bool doEnter = true, bool doExit = true, bool allowSameState = false)
        {
            if (newLeaf == null)
                throw new ArgumentNullException(nameof(newLeaf));

            if (newLeaf is not HierarchicalStateBase_So nextLeaf)
                return;

            if (!allowSameState && IsSameState(currentLeaf, nextLeaf))
                return;

            // save current leaf
            previousLeaf = currentLeaf;

            // path updated
            currentLeaf = nextLeaf;
            SetCurrentPath(currentLeaf);

            if (currentPath?.Count > 0)
            {
                var node = previousLeaf;

                // get the lowest common node if we should do enters or exits
                var lca = doEnter || doExit ? LCA(node, nextLeaf) : null;

                // exit from previous leaf to lca
                if (doExit)
                {
                    while (node != null && node != lca)
                    {
                        node.Exit();
                        node = node.Ancestor; // using ancestor instead of the path
                    }
                }

                // enter from lca to new leaf
                if (doEnter)
                {
                    bool lcaFound = false;

                    // from root to leaf
                    for (int i = currentPath.Count - 1; i >= 0; i--)
                    {
                        // until the lca is found in the path
                        if (!lcaFound)
                        {
                            lcaFound = currentPath[i] == lca;
                            continue;
                        }

                        node = currentPath[i];
                        if (node == null) continue;

                        // the lca is not entered again beacuse is an active state
                        node.Enter();
                    }
                }
            }
        }

        private HierarchicalStateBase_So LCA(HierarchicalStateBase_So node1, HierarchicalStateBase_So node2)
        {
            int depthNode1 = Depth(node1);
            int depthNode2 = Depth(node2);

            while (depthNode1 > depthNode2)
            {
                node1 = node1.Ancestor;
                depthNode1--;
            }

            while (depthNode2 > depthNode1)
            {
                node2 = node2.Ancestor;
                depthNode2--;
            }

            while (!IsSameState(node1, node2))
            {
                node1 = node1.Ancestor;
                node2 = node2.Ancestor;
            }

            return node1; // node1 = node2
        }

        private int Depth(HierarchicalStateBase_So node)
        {
            int d = 0;

            while (node.Ancestor != null)
            {
                node = node.Ancestor;
                d++;
            }
            return d;
        }

        private void SetCurrentPath(HierarchicalStateBase_So leaf)
        {
            currentPath.Clear();

            var state = leaf;
            while (!IsSameState(state, root))
            {
                currentPath.Add(state); // added from bottom to root
                state = state.Ancestor; // next node in path
            }

            currentPath.Add(root);
        }

        public void RevertToPrevious(bool doEnter = true, bool doExit = true, bool allowSameState = false)
        {
            TransitionTo(previousLeaf, doEnter, doExit, allowSameState);
        }

        public bool IsSameState(IStateSo s1, IStateSo s2) => stateComparer.Equals(s1, s2);
        public bool IsCurrentState(IStateSo state) => IsSameState(currentLeaf, state);

        public void Resume() => isRunning = true;
        public void Pause() => isRunning = false;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (OwnedStates == null) return;

            var stateHashSet = new HashSet<HierarchicalStateBase_So>();

            for (int i = 0; i < OwnedStates.Length; i++)
            {
                var state = OwnedStates[i];
                if (state == null) continue;

                if (!stateHashSet.Add(state))
                {
                    OwnedStates[i] = null;
                }
            }
        }
#endif
    }
}