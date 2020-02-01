public class PlayerStateController
{
    public PlayerState CurrentState;
    private PlayerGraphic playerGraphic;

    public PlayerStateController(PlayerGraphic graphic)
    {
        playerGraphic = graphic;
    }

    private void TryToIdle()
    {
        if (SetPlayerState(PlayerState.Idle))
        {
            playerGraphic.Idle();
        }
    }
    
    private void TryToJump()
    {
        if (SetPlayerState(PlayerState.Jumping))
        {
            playerGraphic.Jump();
        }
    }
    
    private void TryToWalk()
    {
        if (SetPlayerState(PlayerState.Walking))
        {
            playerGraphic.Walk();
        }
    }
    
    private void TryToGrab()
    {
        if (SetPlayerState(PlayerState.Grabbing))
        {
            playerGraphic.Grab();
        }
    }
    
    private void TryToThrow()
    {
        if (SetPlayerState(PlayerState.Throwing))
        {
            playerGraphic.Throw();
        }
    }

    private void TryToRepair()
    {
        if (SetPlayerState(PlayerState.Repairing))
        {
            playerGraphic.Repair();
        }
    }
    
    private void TryToShoot()
    {
        if (SetPlayerState(PlayerState.Shooting))
        {
            playerGraphic.Shoot();
        }
    }
    
    private void TryToStun()
    {
        if (SetPlayerState(PlayerState.Stunned))
        {
            playerGraphic.Stun();
            playerGraphic.StartCoroutine(GameConstants.WaitForTime(GameConstants.TIME_TO_END_STUN, () =>
                {
                    SetPlayerState(PlayerState.Idle);
                }));
        }
    }

    private bool SetPlayerState(PlayerState state)
    {
        if (CurrentState == PlayerState.Stunned)
            return false;

        CurrentState = state;
        return true;
    }
}
