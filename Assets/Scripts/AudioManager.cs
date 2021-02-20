using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioBank[] _audioBanks;
    public AudioSource[] _channels;

    static AudioManager _instance;
    public static AudioManager Instance => _instance;

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this);
    }

    public void PlayBGM(int bank, int channel, int index)
    {
        _channels[channel].clip = _audioBanks[bank]._audioClips[index];
        _channels[channel].Play();
    }

    public void PlaySFX(int bank, int channel, int index)
    {
        if(!_channels[channel].isPlaying)
            _channels[channel].PlayOneShot(_audioBanks[bank]._audioClips[index]);
    }

    public void ChangeVolume(int channel, float volume)
    {
        _channels[channel].volume = volume;
    }
}
