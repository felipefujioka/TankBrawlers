using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerView : PhysicsObject
    {
        public Rigidbody2D body;
        public Collider2D collider;
        
        public float maxSpeed = 7;
        public float jumpTakeOffSpeed = 7;

        public Transform holdingPosition;

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


        public DestructiveProp TryGrab()
        {
            var count = Physics2D.RaycastNonAlloc(transform.position, direction > 0 ? Vector2.right : Vector2.left, grabHitBuffer,
                0.5f);
            
            Debug.DrawRay(transform.position, direction > 0 ? Vector2.right : Vector2.left, Color.red);

            for (int i = 0; i < count; i++)
            {
                var hit = hitBuffer[i];
                if (hit.collider == null)
                {
                    continue;
                }
                var destructible = hit.collider.GetComponent<DestructiveProp>();
                if (destructible != null)
                {
                    return destructible;
                }
            }

            return null;
        }
    }
}