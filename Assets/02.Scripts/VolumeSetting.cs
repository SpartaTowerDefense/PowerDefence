using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSetting : MonoBehaviour
{
    private const string masterMixer = nameof(masterMixer);
    private const string musicMixer = nameof(musicMixer);
    private const string sfxMixer = nameof(musicMixer);


    private AudioMixer mixer;
    [SerializeField] private Scrollbar masterSlider, musicSlider, sfxSlider;

    private bool isMaterMute;
    private bool isMusicMute;
    private bool isSFXMute;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.Instance;
        mixer = audioManager.GameMixer;

        masterSlider.value = audioManager.MasterVolume;
        /*musicSlider.value = audioManager.MusicVolume;
        sfxSlider.value = audioManager.SFXVolume;*/
    }

    public void SetMasterVolume()
    {
        isMaterMute = false;

        masterSlider.value = Mathf.Max(masterSlider.value, 0.0001f);
        float volume = masterSlider.value;
        mixer.SetFloat(masterMixer, Mathf.Log10(volume) * 20);

        if (audioManager)
            PlayerPrefs.SetFloat(nameof(audioManager.MasterVolume), volume);
    }

    public void SetMusicVolume()
    {
        isMusicMute = false;

        musicSlider.value = Mathf.Max(musicSlider.value, 0.0001f);
        float volume = musicSlider.value;
        mixer.SetFloat(musicMixer, Mathf.Log10(volume) * 20);

        if (audioManager)
            PlayerPrefs.SetFloat(nameof(audioManager.MusicVolume), volume);
    }

    public void SetSFXVolume()
    {
        isSFXMute = false;

        musicSlider.value = Mathf.Max(musicSlider.value, 0.0001f);
        float volume = musicSlider.value;
        mixer.SetFloat(sfxMixer, Mathf.Log10(volume) * 20);

        if (audioManager)
            PlayerPrefs.SetFloat(nameof(audioManager.SFXVolume), volume);
    }

    public void ToggleMaster()
    {
        if(audioManager)
        {
            if(isMaterMute)
            {
                mixer.SetFloat(masterMixer, 0f);
                isMaterMute = true;
            }
            else
            {
                if (PlayerPrefs.HasKey(nameof(audioManager.MasterVolume)))
                    mixer.SetFloat(masterMixer, PlayerPrefs.GetFloat(nameof(audioManager.MasterVolume)));
                else
                    mixer.SetFloat(masterMixer, 1f);

                isMaterMute = false;
            }
        }
    }

    public void ToggleMusic()
    {
        if (audioManager)
        {
            if (isMusicMute)
            {
                mixer.SetFloat(musicMixer, 0f);
                isMusicMute = true;
            }
            else
            {
                if (PlayerPrefs.HasKey(nameof(audioManager.MusicVolume)))
                    mixer.SetFloat(musicMixer, PlayerPrefs.GetFloat(nameof(audioManager.MusicVolume)));
                else
                    mixer.SetFloat(musicMixer, 1f);

                isMusicMute = false;
            }
        }
    }

    public void ToggleSFX()
    {
        if (audioManager)
        {
            if (isSFXMute)
            {
                mixer.SetFloat(sfxMixer, 0f);
                isSFXMute = true;
            }
            else
            {
                if (PlayerPrefs.HasKey(nameof(audioManager.SFXVolume)))
                    mixer.SetFloat(sfxMixer, PlayerPrefs.GetFloat(nameof(audioManager.SFXVolume)));
                else
                    mixer.SetFloat(sfxMixer, 1f);

                isSFXMute = false;
            }
        }
    }
}
