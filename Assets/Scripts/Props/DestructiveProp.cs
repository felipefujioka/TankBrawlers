using System;
using UnityEngine;

public class DestructiveProp : Prop
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (CanStun && other.gameObject.layer == GameConstants.PROPS_LAYER)
        {
            if(other.gameObject.tag == GameConstants.PROP_TAG)
                other.gameObject.GetComponent<DestructiveProp>().Destroy();
            
            Destroy();
        }
    }

    public void Destroy()
    {
        //vfx
        Destroy(gameObject);
    }
}
