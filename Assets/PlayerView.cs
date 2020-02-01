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
            direction = xMovement;
        }

        public void SetVerticalMovement(float yMovement)
        {
            if (grounded)
            {
                velocity.y = yMovement;   
            }
        }

        protected override void ComputeVelocity()
        {
            targetVelocity = move * maxSpeed;
        }


        public DestructiveProp TryGrab()
        {
            var count = body.Cast(direction > 0 ? Vector2.right : Vector2.left, contactFilter, grabHitBuffer);

            if (count > 0)
            {
                var hit = hitBuffer[0];
                return hit.collider.GetComponent<DestructiveProp>();
            }

            return null;
        }
    }
}