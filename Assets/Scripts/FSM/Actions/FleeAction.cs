using FSM;
using UnityEngine;
using UnityEngine.AI;

namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Actions/Flee")]
    public class FleeAction : FSMAction
    {
        public int speed = 1;
        public override void Execute(BaseStateMachine stateMachine)
        {
            // var navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
            // var enemySightSensor = stateMachine.GetComponent<EnemySightSensor>();
            //
            // navMeshAgent.SetDestination(enemySightSensor.Player.position);
            // Debug.Log("Chasing...");
            var renderer = stateMachine.gameObject.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
	            renderer.color = new Color(0f, 0f, 255f, 1f);
            }

            //-- Chase the player
            var player = GameObject.FindGameObjectsWithTag("Player")[0];
            float step = speed * Time.deltaTime;

            // move sprite towards the target location
            // stateMachine.gameObject.transform.position = Vector2.MoveTowards(stateMachine.gameObject.transform.position, -player.transform.position, step);

			Vector3 dir = stateMachine.gameObject.transform.position - player.transform.position;
		    stateMachine.gameObject.transform.Translate(dir * speed * Time.deltaTime);

			//-- Health Recovery --//
			var h = stateMachine.gameObject.GetComponent<Health>();
			// if (h.health < 20) {h.Recover();}
        }
    }
}