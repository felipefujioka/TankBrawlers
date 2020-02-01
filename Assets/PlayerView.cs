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

        public void SetHorizontalMovement(float xMovement)
        {
            move.x = xMovement;
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
        
        
    }
}