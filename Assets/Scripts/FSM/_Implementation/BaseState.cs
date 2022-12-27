using UnityEngine;

namespace FSM
{
    public class BaseState : ScriptableObject
    {
        // BaseState only contains definition for executing an action
        public virtual void Execute(BaseStateMachine machine) { }
    }
}