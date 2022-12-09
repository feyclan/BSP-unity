using FSM;
using UnityEngine;
namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Decisions/Enemy Close")]
    public class EnemyClose : Decision
    {
        public int minRange = 1;
        
        public override bool Decide(BaseStateMachine stateMachine)
        {
            // Retrieve the player
            var player = GameObject.FindGameObjectsWithTag("Player")[0];
            var health = stateMachine.gameObject.GetComponent<Health>();
            var dist = Vector3.Distance(player.transform.position, stateMachine.gameObject.transform.position);
            // Debug.Log($"Distance: {dist}");
            if ((dist < 1))
            {
                return true;
            }
            else
            {
                return false;
            }
            // var enemyInLineOfSight = stateMachine.GetComponent<EnemySightSensor>();
            // return enemyInLineOfSight.Ping();
        }
    }
}