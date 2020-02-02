using System;
using DefaultNamespace;
using UnityEngine;

public class PlayerHitCollider : MonoBehaviour
{
    private PlayerView playerView;

    private void Start()
    {
        playerView = GetComponent<PlayerView>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == GameConstants.BULLET_TAG || other.gameObject.tag == GameConstants.PROP_TAG ||
            other.gameObject.tag == GameConstants.TANK_PIECE_TAG)
        {
            Prop collidedProp = other.gameObject.GetComponent<Prop>();
            if (collidedProp != null && collidedProp.CanStun && collidedProp.throwingPlayer != playerView)
            {
                if (other.gameObject.tag == GameConstants.PROP_TAG)
                {
                    other.gameObject.GetComponent<DestructiveProp>().Destroy();
                }

                playerView.TryStun();
            }
        }
    }
}
