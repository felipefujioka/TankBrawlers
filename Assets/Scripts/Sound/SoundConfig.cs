using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SoundConfig")]
public class SoundConfig : ScriptableObject
{
    public List<SoundItem> SoundItems;

    public AudioClip GetSoundByName(string soundName)
    {
        return SoundItems.FirstOrDefault(s => s.SoundName == soundName)?.SoundClip;
    }
}
