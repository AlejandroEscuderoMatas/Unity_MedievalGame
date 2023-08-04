using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : PersistentSingleton<MusicManager>
{
    public float                          m_musicVolume = 0.5f;
    public float                          m_sfxVolume = 0.5f;

    private Dictionary<string, AudioClip> m_musicSoundDictionary = null;
    private Dictionary<string, AudioClip> m_sfxSoundDictionary = null;

    private AudioSource                   m_backgroundMusic;
    private AudioSource                   m_sfxMusic;

    public override void Awake()
    {
        base.Awake();

        m_backgroundMusic = CreteAudioSource("Music", true);
        m_sfxMusic        = CreteAudioSource("Sfx", false);

        m_musicSoundDictionary = new Dictionary<string, AudioClip>();
        m_sfxSoundDictionary = new Dictionary<string, AudioClip>();

        MusicVolume = PlayerPrefs.GetFloat(AppPlayerPrefKeys.MUSIC_VOLUME);
        SfxVolume = PlayerPrefs.GetFloat(AppPlayerPrefKeys.SFX_VOLUME);

        AudioClip[] audioSfxVector = Resources.LoadAll<AudioClip>(AppPaths.PATH_RESOURCE_SFX);
        

        for (int i = 0; i < audioSfxVector.Length; i++)
        {
            m_sfxSoundDictionary.Add(audioSfxVector[i].name, audioSfxVector[i]);
        }
        
        
        

        audioSfxVector = Resources.LoadAll<AudioClip>(AppPaths.PATH_RESOURCE_MUSIC);

        for (int i = 0; i < audioSfxVector.Length; i++)
        {
            m_musicSoundDictionary.Add(audioSfxVector[i].name, audioSfxVector[i]);
        }

    }

    public AudioSource CreteAudioSource(string name, bool isLoop)
    {
        GameObject temporalAudioHost = new GameObject(name);
        AudioSource audioSource      = temporalAudioHost.AddComponent<AudioSource>() as AudioSource;
        audioSource.playOnAwake      = false;
        audioSource.loop             = isLoop;
        audioSource.spatialBlend     = 0.0f;
        temporalAudioHost.transform.SetParent(this.transform);
        return audioSource;
    }

    public void PlayBackgroundMusic(string audioName)
    {
        if (m_musicSoundDictionary.ContainsKey(audioName))
        {
            m_backgroundMusic.clip = m_musicSoundDictionary[audioName];
            m_backgroundMusic.volume = m_musicVolume;
            m_backgroundMusic.Play();
        }
    }
    
    public void PlaySfxMusic(string audioName)
    {
        if (m_sfxSoundDictionary.ContainsKey(audioName))
        {
            m_sfxMusic.clip = m_sfxSoundDictionary[audioName];
            m_sfxMusic.volume = m_sfxVolume;
            m_sfxMusic.Play();
        }
    }

    public void StopBackgroundMusic()
    {
        if (m_backgroundMusic != null)
        {
            m_backgroundMusic.Stop();
        }
    }
    
    public void PauseBackgroundMusic()
    {
        if (m_backgroundMusic != null)
        {
            m_backgroundMusic.Pause();
        }
    }

    public float MusicVolume
    {
        get
        {
            return m_musicVolume;
        }
        set
        {
            value = Mathf.Clamp(value, 0, 1);
            m_backgroundMusic.volume = m_musicVolume;
            m_musicVolume = value;
        }
    }
    public float MusicVolumeSave
    {
        get
        {
            return m_musicVolume;
        }
        set
        {
            value = Mathf.Clamp(value, 0, 1);
            m_backgroundMusic.volume = m_musicVolume;
            PlayerPrefs.SetFloat(AppPlayerPrefKeys.MUSIC_VOLUME, value);
            m_musicVolume = value;
        }
    }
    
    public float SfxVolume
    {
        get
        {
            return m_sfxVolume;
        }
        set
        {
            value = Mathf.Clamp(value, 0, 1);
            m_sfxMusic.volume = m_sfxVolume;
            m_sfxVolume = value;
        }
    }
    public float SfxVolumeSave
    {
        get
        {
            return m_sfxVolume;
        }
        set
        {
            value = Mathf.Clamp(value, 0, 1);
            m_sfxMusic.volume = m_sfxVolume;
            PlayerPrefs.SetFloat(AppPlayerPrefKeys.SFX_VOLUME, value);
            m_musicVolume = value;
        }
    }
}