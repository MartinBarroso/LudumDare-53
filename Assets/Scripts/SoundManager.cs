using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource effectSource;
    [SerializeField] public AudioClip button;
    [SerializeField] public AudioClip mainMenu;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    public void PlaySound(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }
    public void PlayButtonSound()
    {
        effectSource.PlayOneShot(button);
    }

    public AudioSource GetMusicSource()
    {
        return musicSource;
    }
    public void ToggleEffects()
    {
        effectSource.mute = !effectSource.mute;
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }
   
}
