using FSM;
using UnityEngine;
namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Decisions/Critical Health")]
    public class CriticalHealth : Decision
    {
        public override bool Decide(BaseStateMachine stateMachine)
        {
            // Retrieve the player
            var player = GameObject.FindGameObjectsWithTag("Player")[0];
			var health = stateMachine.gameObject.GetComponent<Health>();
            var dist = Vector3.Distance(player.transform.position, stateMachine.gameObject.transform.position);
            // Get the Enemy script
            if (health.health < 20)
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