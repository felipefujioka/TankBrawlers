using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Instantiate(Resources.Load<SoundManager>("Sound/SoundManager"));
                
                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }
    
    private SoundConfig soundConfig;
    private AudioMixer defaultMixer;
    private List<AudioSource> sfxAudioSources;
    private AudioSource bgmAudioSource;
    public bool IsBGMMuted, IsSFXMuted;

    private void Awake()
    {
        IsBGMMuted = PlayerPrefs.GetInt("IsBGMMuted", 0) == 1 ? IsBGMMuted = true : IsBGMMuted = false;
        IsSFXMuted = PlayerPrefs.GetInt("IsSFXMuted", 0) == 1 ? IsSFXMuted = true : IsSFXMuted = false;
        
        sfxAudioSources = new List<AudioSource>();
        bgmAudioSource = gameObject.AddComponent<AudioSource>();
        soundConfig = Resources.Load<SoundConfig>("Sound/SoundConfig");
        defaultMixer = Resources.Load<AudioMixer>("Sound/DefaultMixer");
    }

    private AudioSource GetAvailableAudioSource()
    {
        AudioSource source = sfxAudioSources.Find(a => !a.isPlaying);
        if (source == null)
        {
            source = gameObject.AddComponent<AudioSource>();
            sfxAudioSources.Add(source);
        }

        return source;
    }

    public void PlaySFX(string soundName, bool isLoop = false)
    {
        if (!IsSFXMuted)
        {
            AudioSource source = GetAvailableAudioSource();
            AudioClip soundClip = soundConfig.GetSoundByName(soundName);
            source.loop = isLoop;
            source.clip = soundClip;
            source.Play();
        }
    }

    public void PlayBGM(string soundName)
    {
        AudioClip soundClip = soundConfig.GetSoundByName(soundName);
        bgmAudioSource.loop = true;
        bgmAudioSource.clip = soundClip;
        if (!IsBGMMuted)
        {
            bgmAudioSource.Play();
        }
    }

    public void SetAudioSnapshot(string snapshotName, float fadeTime)
    {
        defaultMixer.FindSnapshot(snapshotName).TransitionTo(fadeTime);
    }

    public bool ToggleBGMMute()
    {
        IsBGMMuted = !IsBGMMuted;
        PlayerPrefs.SetInt("IsBGMMuted", IsBGMMuted ? 1 : 0);
        if (IsBGMMuted)
        {
            bgmAudioSource.Pause();
        }
        else
        {
            bgmAudioSource.Play();
        }
        return IsBGMMuted;
    }
    
    public bool ToggleSFXMute()
    {
        IsSFXMuted = !IsSFXMuted;
        PlayerPrefs.SetInt("IsSFXMuted", IsSFXMuted ? 1 : 0);
        if (IsSFXMuted)
        {
            for (int i = 0; i < sfxAudioSources.Count; i++)
            {
                sfxAudioSources[i].Stop();
            }
        }
        
        return IsSFXMuted;
    }
}
