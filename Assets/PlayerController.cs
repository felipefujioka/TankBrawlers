using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PlayerController
{
    public int ID;

    public PlayerView view;

    public Team playerTeam;
    
    public bool IsGrabbing { get; set; }

    private float horizontalMovement;

    private Prop holdingPropRef;
    
    private static readonly int Grab1 = Animator.StringToHash("Grab");
    private static readonly int Throw1 = Animator.StringToHash("Throw");
    public Prop holdingProp
    {
        get => holdingPropRef;
        set
        {
            holdingPropRef = value;
            IsGrabbing = value != null;
        }
    }
    
    public void ApplyHorizontalMovement(float horizontal)
    {
        view.SetHorizontalMovement(horizontal);
    }

    public void Jump()
    {
        Debug.Log("JUMP!");
        view.SetVerticalMovement(view.jumpTakeOffSpeed);
    }

    public void TargetProp(Vector2 direction)
    {
        view.TryHighlight(direction);
    }

    public void DisableTarget()
    {
        view.DisableHighlight();
    }

    public void Grab(Vector2 direction)
    {
        if (holdingProp != null)
        {
            //DROP
            holdingProp.DropProp();
            holdingProp = null;
        }
        
        holdingProp = view.TryGrab(direction);

        if (holdingProp != null)
        {
            holdingProp.GrabProp(view);
            view.Animator.SetTrigger(Grab1);
        }
    }

    public void Throw(Vector2 direction)
    {
        if (holdingProp != null)
        {
            holdingProp.ThrowDrop(direction, view);
            holdingProp = null;
            view.Animator.SetTrigger(Throw1);
        }
    }
}
