using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int ID;

    public PlayerView view;
    public bool IsGrabbing { get; set; }

    private float horizontalMovement;

    private DestructiveProp holdingProp;
    
    public void ApplyHorizontalMovement(float horizontal)
    {
        view.SetHorizontalMovement(horizontal);
    }

    public void Jump()
    {
        view.SetVerticalMovement(view.jumpTakeOffSpeed);
    }

    public void Grab()
    {
        if (holdingProp != null)
        {
            //DROP
            holdingProp.DropProp();
            holdingProp = null;
        }
        
        holdingProp = view.TryGrab();

        if (holdingProp != null)
        {
            holdingProp.GrabProp(view);
        }
    }

    public void Throw(Vector2 direction)
    {
        throw new System.NotImplementedException();
    }
}
