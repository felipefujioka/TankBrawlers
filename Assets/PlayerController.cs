using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PlayerController
{
    public float axisOffsetX;
    public int ID;

    public PlayerView view;
    public bool IsGrabbing { get; set; }

    private float horizontalMovement;
    
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
        throw new System.NotImplementedException();
    }

    public void Throw(Vector2 direction)
    {
        throw new System.NotImplementedException();
    }
}
