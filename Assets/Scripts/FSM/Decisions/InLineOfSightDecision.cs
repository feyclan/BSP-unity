using FSM;
using UnityEngine;
namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Decisions/In Line Of Sight")]
    public class InLineOfSightDecision : Decision
    {
        public override bool Decide(BaseStateMachine stateMachine)
        {
            // Retrieve the player
            var player = GameObject.FindGameObjectsWithTag("Player")[0];
            var health = stateMachine.gameObject.GetComponent<Health>();
            var dist = Vector3.Distance(player.transform.position, stateMachine.gameObject.transform.position);
            if ((dist < 5))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}