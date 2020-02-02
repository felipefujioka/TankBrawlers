using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerSFX : MonoBehaviour
{
    public void PlayHammerSound()
    {
        SoundManager.Instance.PlaySFX("sfx_tank_repair");   
    }
}
