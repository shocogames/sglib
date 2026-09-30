using System;
using System.Collections.Generic;

namespace SGLib.AI.Behaviour.StateMachines.Classic
{
    public class HFSM : StateMachineBase<HierarchicalStateBase>
    {
        private readonly HierarchicalStateBase root;
        private readonly List<HierarchicalStateBase> currentPath; // reversed active path

        private readonly HierarchicalStateBase initialState;

        public HFSM(HierarchicalStateBase root, HierarchicalStateBase initialState,
            IEqualityComparer<IState> stateComparer = null, int maxDepth = 16)
        {
            this.root = root ?? throw new ArgumentNullException(nameof(root));
            this.initialState = initialState ?? throw new ArgumentNullException(nameof(initialState));
            PreviousState = null;
            StateComparer = stateComparer ?? EqualityComparer<IState>.Default;

            currentPath = new(maxDepth);
        }

        public override void Run()
        {
            if (IsRunning) return;

            Reset();

            // from root to leaf
            for (int i = currentPath.Count - 1; i >= 0; i--)
            {
                var state = currentPath[i];
                state?.Enter();
            }

            IsRunning = true;
        }

        public override void Stop()
        {
            if (!IsRunning) return;

            IsRunning = false;

            foreach (var node in currentPath)
            {
                node?.Exit();
            }

            CurrentState = null;
        }

        public void Reset()
        {
            CurrentState = initialState;
            SetCurrentPath(CurrentState);
        }

        public override void Execute(float deltaTime)
        {
            if (!IsRunning) return;

            for (int i = currentPath.Count - 1; i >= 0; i--)
            {
                var state = currentPath[i];
                state.Execute(this, deltaTime);
            }
        }

        public override void TransitionTo(IState newState, bool doEnter = true, bool doExit = true, bool allowSameState = false)
        {
            if (newState == null)
                throw new ArgumentNullException(nameof(newState));

            if (newState is not HierarchicalStateBase nextState)
                throw new InvalidCastException(
                    $"Expected HierarchicalState but received {newState.GetType().Name}.");

            if (!allowSameState && IsSameState(CurrentState, nextState))
                return;

            // save current leaf
            PreviousState = CurrentState;

            // path updated
            CurrentState = nextState;
            SetCurrentPath(CurrentState);

            if (currentPath?.Count > 0)
            {
                var node = PreviousState;

                // get the lowest common node if we should do enters or exits
                var lca = doEnter || doExit ? LCA(node, nextState) : null;

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

                    // from root to leaf (now we cna uso the current path)
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

        private HierarchicalStateBase LCA(HierarchicalStateBase node1, HierarchicalStateBase node2)
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

        private int Depth(HierarchicalStateBase node)
        {
            int d = 0;

            while (node.Ancestor != null)
            {
                node = node.Ancestor;
                d++;
            }
            return d;
        }

        private void SetCurrentPath(HierarchicalStateBase leaf)
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
    }
}
