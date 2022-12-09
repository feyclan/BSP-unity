using UnityEngine;

namespace FSM
{
    public class BaseStateMachine : MonoBehaviour
    {
        //-- Borders --//
        public float minXPosition = -10.0f; // left border
        public float minYPosition = -4.0f; // left border
        public float maxXPosition = 10.0f; //  right border
        public float maxYPosition = 4.0f; //  right border
        
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
            var pos = gameObject.transform.position;
            gameObject.transform.position = new Vector3(Mathf.Clamp(transform.position.x, minXPosition, maxXPosition), 
                Mathf.Clamp(transform.position.y, minYPosition, maxYPosition), pos.z);
        }
    }
}