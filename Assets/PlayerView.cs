using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerView : PhysicsObject
    {
        public Transform Center;
        
        public float maxSpeed = 7;
        public float jumpTakeOffSpeed = 7;

        public Transform holdingPosition;
        
        public PlayerController playerController;

        private Vector2 move;

        private float direction;
        
        private RaycastHit2D[] grabHitBuffer = new RaycastHit2D[16];

        public void SetHorizontalMovement(float xMovement)
        {
            move.x = xMovement;
            direction = Mathf.Abs(xMovement) > 0.2f ? xMovement : direction;
        }

        public void SetVerticalMovement(float yMovement)
        {
            if (grounded)
            {
                Debug.Log("Was grounded!");
                velocity.y = yMovement;   
            }
        }

        protected override void ComputeVelocity()
        {
            targetVelocity = move * maxSpeed;
        }


        public Prop TryGrab(Vector2 direction)
        {
            var count = Physics2D.RaycastNonAlloc(Center.transform.position, direction, grabHitBuffer,
                1f);
            
            Debug.DrawRay(Center.transform.position, direction, Color.red);

            for (int i = 0; i < count; i++)
            {
                var hit = grabHitBuffer[i];
                if (hit.collider == null)
                {
                    continue;
                }
                var destructible = hit.collider.GetComponent<Prop>();
                if (destructible != null)
                {
                    return destructible;
                }
            }

            return null;
        }
    }
}