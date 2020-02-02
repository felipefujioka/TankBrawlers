using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SoundConfig")]
public class SoundConfig : ScriptableObject
{
    public List<SoundItem> SoundItems;

    public AudioClip GetSoundByName(string soundName)
    {
        return SoundItems.Find(s => s.SoundName == soundName).SoundClip;
    }
}
