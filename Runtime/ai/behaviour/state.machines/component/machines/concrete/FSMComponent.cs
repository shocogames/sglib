using System;
using System.Collections.Generic;
using SGLib.Utility.Management.Component.Capabilities;
using UnityEngine;

namespace SGLib.AI.Behaviour.StateMachines.Component
{
    public abstract class FSMComponent : SG_Component, IStateMachineComponent
    {
        [SerializeField] private State_So initialState;
        [SerializeField] private StateComparerBase_So customStateComparer;
        [SerializeField] private State_So[] ownedStates;

        private IStateSo currentState, previousState;
        private IEqualityComparer<IStateSo> stateComparer;
        private bool isRunning;

        public override void Initialize()
        {
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

            if (currentState == null) return;

            currentState.EvaluateTransitions();
        }

        public void Run()
        {
            if (isRunning) return;

            currentState = initialState;

            foreach (var state in ownedStates)
            {
                if (state == null) continue;
                state.Bind(this);
            }

            currentState.Enter();

            isRunning = true;
        }

        public void Stop()
        {
            if (!isRunning) return;

            isRunning = false;

            currentState.Exit();

            foreach (var state in ownedStates)
            {
                if (state == null) continue;
                state.Unbind(this);
            }

            currentState = null;
        }

        public void TransitionTo(IStateSo newState, bool doEnter = true, bool doExit = true, bool allowSameState = false)
        {
            if (newState == null)
                throw new ArgumentNullException(nameof(newState));

            if (!allowSameState && IsSameState(currentState, newState))
                return;

            previousState = currentState;

            if (doExit) currentState.Exit();

            currentState = newState;

            if (doEnter) currentState.Enter();
        }

        public void RevertToPrevious(bool doEnter = true, bool doExit = true, bool allowSameState = false)
        {
            TransitionTo(previousState, doEnter, doExit, allowSameState);
        }

        public bool IsSameState(IStateSo s1, IStateSo s2) => stateComparer.Equals(s1, s2);
        public bool IsCurrentState(IStateSo state) => IsSameState(currentState, state);

        public void Resume() => isRunning = true;
        public void Pause() => isRunning = false;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (ownedStates == null) return;

            var stateHashSet = new HashSet<State_So>();

            for (int i = 0; i < ownedStates.Length; i++)
            {
                var state = ownedStates[i];
                if (state == null) continue;

                if (!stateHashSet.Add(state))
                {
                    ownedStates[i] = null;
                }
            }
        }
#endif
    }
}

