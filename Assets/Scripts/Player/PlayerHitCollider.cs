using UnityEngine;

public class PlayerHitCollider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == GameConstants.BULLET_TAG || other.gameObject.tag == GameConstants.PROP_TAG ||
            other.gameObject.tag == GameConstants.TANK_PIECE_TAG)
        {
            Prop collidedProp = other.gameObject.GetComponent<Prop>();
            if (collidedProp.CanStun)
            {
                //Stun player
            }
        }
    }
}
