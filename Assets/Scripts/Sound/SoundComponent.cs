using UnityEngine;

public class SoundComponent : MonoBehaviour
{
    public void ExecuteSound(string soundName)
    {
        SoundManager.Instance.PlaySFX(soundName, false);
    }
}
