using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    private const string PATH = "AudioClips\\";
    

    public AudioSource MusicSource { get; private set; }
    public AudioSource SFXSource { get; private set; }

    public AudioMixer GameMixer { get; private set; }


    public int MusicClipSize { get; private set; } = 0;
    public int FireClipSize { get; private set; } = 0;

    public void Initinalize()
    {
        GameMixer = ResourceManager.Instance.LoadResource<AudioMixer>(nameof(GameMixer), nameof(GameMixer));
        
        if (MusicSource == null)
        {
            GameObject prefab = ResourceManager.Instance.LoadResource<GameObject>(nameof(MusicSource), nameof(MusicSource));
            MusicSource = Instantiate(prefab, this.transform).GetComponent<AudioSource>();
            MusicSource.playOnAwake = false;
            MusicSource.transform.parent = this.transform;
        }

        if (SFXSource == null)
        {
            GameObject prefab = ResourceManager.Instance.LoadResource<GameObject>(nameof(SFXSource), nameof(SFXSource));
            SFXSource = Instantiate(prefab, this.transform).GetComponent<AudioSource>();
            SFXSource.playOnAwake = false;
            SFXSource.transform.parent = this.transform;
        }

        MusicClipSize = ResourceManager.Instance.LoadResourceAll<AudioClip>(Enums.MusicClip, $"{PATH}{Enums.MusicClip}"); // MusicClip1, MusicClip2...
        FireClipSize = ResourceManager.Instance.LoadResourceAll<AudioClip>(Enums.FireClip, $"{PATH}{Enums.FireClip}"); // SFXClip1, SFXClip2...
        ResourceManager.Instance.LoadResource<AudioClip>(Enums.EnemyDie, $"{PATH}{Enums.EnemyDie}");

        MusicSource.loop = true;
        MusicSource.clip = ResourceManager.Instance.LoadResource<AudioClip>($"{Enums.MusicClip}1");
        MusicSource.Play();

        // ResourceLoad
        // TODO::
        /*ResourceManager.Instance.LoadResourceAll<AudioClip>(MusicClip, $"{PATH}{MusicClip}");
        ResourceManager.Instance.LoadResourceAll<AudioClip>(SFXClip, $"{PATH}{SFXClip}");*/
    }

    // 해당씬에서 어떤것을 살리고 해제할것인지 모르기에
    private void ClearResource(Action clearMethod)
    {
        clearMethod?.Invoke();
    }
}
