using UnityEngine;
public class TankPiece : Prop
{
    private Team team;
    public string Id;
    
    public Rigidbody2D rb;
    protected override void onCollide(Prop p)
    {
        throw new System.NotImplementedException();
    }

    public void cancelGravity()
    {
        rb.gravityScale = 0;
        rb.velocity = Vector3.zero;
    }
}
