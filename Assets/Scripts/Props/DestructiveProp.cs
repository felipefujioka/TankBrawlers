using UnityEngine;

public class DestructiveProp : Prop
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (CanStun && other.gameObject.layer == GameConstants.PROPS_LAYER)
        {
            SoundManager.Instance.PlaySFX("sfx_props_collide", false);

            if (other.gameObject.CompareTag(GameConstants.PROP_TAG))
            {
                other.gameObject.GetComponent<DestructiveProp>().Destroy();
            }

            Destroy();
        }
        
        CanStun = false;
    }

    public void Destroy()
    {
        //vfx
        Destroy(gameObject);
    }
}
