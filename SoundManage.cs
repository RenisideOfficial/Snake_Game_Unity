using UnityEngine;
using UnityEngine.Audio;
using System;

public class SoundManage : MonoBehaviour
{
    public static SoundManage instance;
    public Sound[] musicSound, sfxSound;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("SnakeBeat");
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSound, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found!");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }
    public void PlaySfx(string name)
    {
        Sound s = Array.Find(sfxSound, y => y.name == name);

        if(s == null)
        {
            Debug.Log("Sound not found!");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }
    public void ToggleSfx()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    public void SfxVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
