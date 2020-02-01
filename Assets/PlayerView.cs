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

        private SpriteRenderer spriteRenderer;
        private Animator animator;

        private Vector2 move;
        

        // Use this for initialization
        void Awake () 
        {
            spriteRenderer = GetComponent<SpriteRenderer> ();    
            animator = GetComponent<Animator> ();
        }

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