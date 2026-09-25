using System.Collections.Generic;
using UnityEngine;

namespace SGLib.AI.Behaviour.StateMachines.Component
{
    public abstract class StandardStateBase_So<T> : StateBase_So where T : StandardStateBase_So<T>
    {
        [SerializeField] protected List<T> TransitionList;

#if UNITY_EDITOR
        private void OnValidate()
        {
            var stateHashSet = new HashSet<T>();

            for (int i = 0; i < TransitionList.Count; i++)
            {
                var state = TransitionList[i];
                if (state == null) continue;

                if (!stateHashSet.Add(state))
                {
                    TransitionList[i] = null;
                }
            }
        }
#endif
    }
}

