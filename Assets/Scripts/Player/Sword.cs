using UnityEngine;

namespace Player
{
    public class Sword : MonoBehaviour
    {
        //-- References to objects --//
        // reference to the Swordman controller of the player
        public Swordman player;
        // reference to the animator of the player
        public Animator animator;
        // amount of damage that the player does to the enemies
        public int damage = 10;

        /// <summary>
        /// When the sword enters a collision with an object we check whether the object it collides with is an enemy.
        /// If this is the case we can trigger a hit to the Enemy
        /// </summary>
        /// <param name="collider">The object the sword collides with</param>
        void OnTriggerEnter2D(Collider2D collider)
        {
            //-- Detect collisions of the sword with the enemy --//
            if (collider.CompareTag("Enemy"))
            {
                // Get the current animation clip info
                AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
                // Check if any animations are currently being played
                if (clipInfo.Length > 0)
                {
                    // Get the name of the first animation clip
                    string clipName = clipInfo[0].clip.name;
                    // Check if the player is currently attacking to avoid counting bumps as attacks
                    if (clipName == "Attack")
                    {
                        // Each animation trigger should only count towards one hit, we keep track of how often the animation has been triggered
                        if (player.attackCounter > 1)
                        {
                            //-- Retrieve the Health controller NPC that is hit --//
                            collider.GetComponent<Health>().Hit(player.gameObject, damage);
                            //-- Remove one from the counter --//
                            player.attackCounter -= 1;
                        }
                    }
                }
            }
        }
    }
}
