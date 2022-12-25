using UnityEngine;

namespace FSM
{
    public class BaseStateMachine : MonoBehaviour
    {
        // Initial state of the FSM
        [SerializeField] private BaseState _initialState;

        //-- Awake --//
        private void Awake()
        {
            // set the machine to the initial state
            CurrentState = _initialState;
        }

        public BaseState CurrentState { get; set; }

        private void Update()
        {
            // execute the action of the current station
            CurrentState.Execute(this);
        }
    }
}