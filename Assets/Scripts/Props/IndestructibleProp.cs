using UnityEngine;

public class IndestructibleProp : Prop
{
    public BoxCollider2D colliderInProps;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CanStun && collision.gameObject.layer == GameConstants.PROPS_LAYER)
        {
            var direction = collision.contacts[0].normal;
            rigidbody.velocity = new Vector2(direction.x * 5, 5);
        }
        
        CanStun = false;
    }
}
