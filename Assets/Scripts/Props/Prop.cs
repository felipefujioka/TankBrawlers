using DefaultNamespace;
using UnityEngine;

public abstract class Prop: MonoBehaviour
{
    public bool CanStun;
    protected abstract void onCollide(Prop collidedProp);
    public Rigidbody2D rb;

    public void GrabProp(PlayerView playerView)
    {
        transform.SetParent(playerView.holdingPosition);
        transform.position = Vector3.zero;
    }

    protected void DropProp()
    {
        transform.SetParent(null);
        rb.AddForce(Vector3.up * 10);
    }

    public void ThrowDrop(Vector3 direction)
    {
        CanStun = true;
        transform.SetParent(null);
        rb.AddForce(direction * 10);
    }

     public void cancelGravity()
    {
        rb.gravityScale = 0;
        rb.velocity = Vector3.zero;
    }
}
