using DefaultNamespace;
using UnityEngine;

public abstract class Prop: MonoBehaviour
{
    public bool CanStun;
    protected abstract void onCollide(Prop collidedProp);
    public Rigidbody2D rigidbody;
    private Collider2D collider;

    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    public void GrabProp(PlayerView playerView)
    {
        transform.SetParent(playerView.holdingPosition);
        transform.localPosition = Vector3.zero;
        rigidbody.gravityScale = 0;
        rigidbody.bodyType = RigidbodyType2D.Kinematic;
        collider.enabled = false;
    }

    public void DropProp()
    {
        transform.SetParent(null);
        rigidbody.AddForce(Vector3.up * 10);
        rigidbody.gravityScale = 1f;
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        collider.enabled = true;
    }

    public void ThrowDrop(Vector3 direction)
    {
        CanStun = true;
        transform.SetParent(null);
        rigidbody.AddForce(direction * 10);
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        collider.enabled = true;
    }
    public void cancelGravity()
    {
        rigidbody.gravityScale = 0;
        rigidbody.velocity = Vector3.zero;
    }
}
