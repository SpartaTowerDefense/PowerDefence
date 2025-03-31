using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
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
            GameObject music = new GameObject(nameof(MusicSource));
            music.transform.parent = transform;

            MusicSource = music.AddComponent<AudioSource>();
            MusicSource.playOnAwake = false;
            //MusicSource.outputAudioMixerGroup = GameMixer.
        }

        if (SFXSource == null)
        {
            GameObject sfx = new GameObject(nameof(SFXSource));
            sfx.transform.parent = transform;

            SFXSource = sfx.AddComponent<AudioSource>();
            SFXSource.playOnAwake = false;
        }

    }
}
