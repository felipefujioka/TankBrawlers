using System;
using System.Collections;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;

public abstract class Prop: MonoBehaviour
{
    public float ThrowBoost = 5f;
    public bool CanStun;
    public PlayerView throwingPlayer;
    protected abstract void onCollide(Prop collidedProp);
    public Rigidbody2D rigidbody;
    public Collider2D collider;
    public SpriteRenderer propSprite;
    public Material material;

    void Awake()
    {
        propSprite.material = new Material(material);
        propSprite.material.SetFloat(GameConstants.OUTLINE_BRIGHTNESS_TAG, 0f);
        propSprite.material.SetColor(GameConstants.OUTLINE_COLOR, Color.yellow);
        propSprite.material.SetFloat(GameConstants.OUTLINE_WIDTH, 0.01f);

        StartCoroutine(DisableParachute());
    }

    IEnumerator DisableParachute()
    {
        yield return new WaitForSeconds(1.2f);
        Transform parachute = transform.Find("Parachute");
        if(parachute != null)
            Destroy(parachute.gameObject);
    }

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

    public void ThrowDrop(Vector3 direction, PlayerView playerView)
    {
        CanStun = true;
        throwingPlayer = playerView;
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

    public void HighlightProp()
    {
        propSprite.material.SetFloat(GameConstants.OUTLINE_BRIGHTNESS_TAG, 4f);
    }

    public void DisableHighlight()
    {
        propSprite.material.SetFloat(GameConstants.OUTLINE_BRIGHTNESS_TAG, 0f);
    }

    private void OnDestroy()
    {
        PropSpawnerManager.Instance.RemoveProp(this);
    }
}
