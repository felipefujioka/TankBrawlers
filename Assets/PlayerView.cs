using System;
using Unity.Mathematics;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerView : PhysicsObject
    {
        public Animator Animator;
        public Transform Center;
        public Rigidbody2D body;
        public Collider2D collider;
        
        public float maxSpeed = 7;
        public float jumpTakeOffSpeed = 7;

        public Transform holdingPosition;

        private Vector2 move;

        private float direction;
        
        private RaycastHit2D[] grabHitBuffer = new RaycastHit2D[16];
        private static readonly int RunSpeed = Animator.StringToHash("RunSpeed");
        private static readonly int Grounded = Animator.StringToHash("Grounded");

        public void SetHorizontalMovement(float xMovement)
        {
            var absSpeed = Mathf.Abs(xMovement);
            move.x = xMovement;
            direction = absSpeed > 0.2f ? xMovement : direction;
            Animator.transform.localScale = new Vector3(Mathf.Sign(direction), Animator.transform.localScale.y);
            Animator.SetFloat(RunSpeed, absSpeed);
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
            
            Animator.SetBool(Grounded, grounded);
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