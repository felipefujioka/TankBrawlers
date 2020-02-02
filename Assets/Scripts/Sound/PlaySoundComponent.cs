using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundComponent : MonoBehaviour
{
    [SerializeField] private bool isBGM;

    public void PlaySound(string soundName)
    {
        if (isBGM)
        {
            SoundManager.Instance.PlayBGM(soundName);
        }
        else
        {
            SoundManager.Instance.PlaySFX(soundName);
        }
    }
}
