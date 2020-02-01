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
        }
    }

    public void Throw(Vector2 direction)
    {
        if (holdingProp != null)
        {
            holdingProp.ThrowDrop(direction);
            holdingProp = null;
        }
    }
}
