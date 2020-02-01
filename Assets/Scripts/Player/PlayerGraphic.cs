using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGraphic : MonoBehaviour
{
    private PlayerStateController playerStateController;
    [SerializeField] private Animator graphicAnimator;

    private void Start()
    {
        playerStateController = new PlayerStateController(this);
    }

    public void Idle()
    {
        SetAnimation(playerStateController.CurrentState.ToString());
    }
    
    public void Jump()
    {
        SetAnimation(playerStateController.CurrentState.ToString());
    }
    
    public void Walk()
    {
        SetAnimation(playerStateController.CurrentState.ToString());
    }
    
    public void Grab()
    {
        SetAnimation(playerStateController.CurrentState.ToString());
    }
    
    public void Throw()
    {
        SetAnimation(playerStateController.CurrentState.ToString());
    }
    
    public void Repair()
    {
        SetAnimation(playerStateController.CurrentState.ToString());
    }
    
    public void Shoot()
    {
        SetAnimation(playerStateController.CurrentState.ToString());
    }
    
    public void Stun()
    {
        SetAnimation(playerStateController.CurrentState.ToString());
    }

    private void SetAnimation(string animationTrigger)
    {
        graphicAnimator.SetTrigger(animationTrigger);
    }
}
