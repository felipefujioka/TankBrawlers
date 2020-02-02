using System;
using Unity.Mathematics;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerView : PhysicsObject
    {
        public Animator Animator;
        public Transform Center;
        
        public float maxSpeed = 7;
        public float jumpTakeOffSpeed = 7;

        public Transform holdingPosition;
        
        public PlayerController playerController;

        private Vector2 move;

        private float direction;

        private Prop highlightedProp;

        private bool isStuned;
        
        private RaycastHit2D[] grabHitBuffer = new RaycastHit2D[16];
        private static readonly int RunSpeed = Animator.StringToHash("RunSpeed");
        private static readonly int Grounded = Animator.StringToHash("Grounded");

        public void SetHorizontalMovement(float xMovement)
        {
            if (!CanControl())
                return;
            
            var absSpeed = Mathf.Abs(xMovement);
            
            move.x = xMovement;
            direction = absSpeed > 0.2f ? xMovement : direction;
            Animator.transform.localScale = new Vector3(Mathf.Sign(direction), Animator.transform.localScale.y);
            Animator.SetFloat(RunSpeed, absSpeed);
        }

        public void SetVerticalMovement(float yMovement)
        {
            if (grounded && CanControl())
            {
                Debug.Log("Was grounded!");
                velocity.y = yMovement;
            }
        }

        protected override void ComputeVelocity()
        {
            if (!CanControl())
                return;
            
            targetVelocity = move * maxSpeed;
            
            Animator.SetBool(Grounded, grounded);
        }

        public void TryStun()
        {
            isStuned = true;
            StartCoroutine(GameConstants.WaitForTime(GameConstants.STUNNED_TIME, () => { isStuned = false; }));
        }

        public Prop TryGrab(Vector2 direction)
        {
            if (!CanControl())
                return null;
            
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
                var prop = hit.collider.GetComponent<Prop>();
                if (prop != null)
                {
                    return prop;
                }
            }

            return null;
        }
        
        public void TryHighlight(Vector2 direction)
        {
            if (!CanControl())
                return;
            
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
                var prop = hit.collider.GetComponent<Prop>();
                if (prop != null)
                {
                    if (highlightedProp == null || (highlightedProp != null && prop != highlightedProp))
                    {
                        if(highlightedProp != null)
                            highlightedProp.DisableHighlight();
                        
                        highlightedProp = prop;
                        highlightedProp.HighlightProp();
                    }
                }
                else
                {
                    DisableHighlight();
                }
            }
        }

        private bool CanControl()
        {
            return !isStuned && GameInfo.Instance.IsRunning;
        }

        public void DisableHighlight()
        {
            if (highlightedProp != null)
            {
                highlightedProp.DisableHighlight();
                highlightedProp = null;
            }
        }
    }
}