using FSM;
using UnityEngine;
using UnityEngine.AI;

namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Actions/Fight")]
    public class FightAction : FSMAction
    {
        public int damage = 1;
        public override void Execute(BaseStateMachine stateMachine)
        {
            //-- Chase the player
            var player = GameObject.FindGameObjectsWithTag("Player")[0];
            var h = player.transform.GetComponent<Health>();
            h.Hit(stateMachine.gameObject, damage);
            
            var renderer = stateMachine.gameObject.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                renderer.color = new Color(0f, 0f, 0f, 1f);
            }

        }
    }
}