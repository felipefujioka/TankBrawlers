using DefaultNamespace;
using UnityEngine;

public abstract class Prop: MonoBehaviour
{
    public bool CanStun;
    protected abstract void onCollide(Prop collidedProp);
    private Rigidbody2D rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    public void GrabProp(PlayerView playerView)
    {
        transform.SetParent(playerView.holdingPosition);
        transform.position = Vector3.zero;
    }

    protected void DropProp()
    {
        transform.SetParent(null);
        rigidbody.AddForce(Vector3.up * 10);
    }
}
