using DefaultNamespace;
using DG.Tweening;
using UnityEngine;

public abstract class Prop: MonoBehaviour
{
    public float ThrowBoost = 5f;
    public bool CanStun;
    protected abstract void onCollide(Prop collidedProp);
    public Rigidbody2D rigidbody;
    public Collider2D collider;
    

    public void GrabProp(PlayerView playerView)
    {
        transform.SetParent(playerView.holdingPosition);
        transform.DOLocalMove(Vector3.zero, 0.1f).Play();
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
        rigidbody.velocity = direction * ThrowBoost;
        rigidbody.gravityScale = 1f;
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        collider.enabled = true;
    }
    public void cancelGravity()
    {
        rigidbody.gravityScale = 0;
        rigidbody.velocity = Vector3.zero;
    }
}
