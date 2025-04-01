using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    private const string PATH = "AudioClips\\";
    private const string MusicClip = nameof(MusicClip);
    private const string SFXClip = nameof(SFXClip);

    public AudioSource MusicSource { get; private set; }
    public AudioSource SFXSource { get; private set; }

    public AudioMixer GameMixer { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        Initinalize();
    }

    private void Initinalize()
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

        // ResourceLoad
        // TODO::
        /*ResourceManager.Instance.LoadResourceAll<AudioClip>(MusicClip, $"{PATH}{MusicClip}");
        ResourceManager.Instance.LoadResourceAll<AudioClip>(SFXClip, $"{PATH}{SFXClip}");*/
    }
}
