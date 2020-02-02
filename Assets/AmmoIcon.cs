using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoIcon : MonoBehaviour
{
    public void BulletAnimationSFX()
    {
        SoundManager.Instance.PlaySFX("sfx_tank_reload");
    }
}
