using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SFXManager : Singleton<SFXManager>
{
    [SerializeField] 
    private GameObject SFXContents;

    private AudioSource _audioSource;
    private void Start()
    {
        _audioSource = this.GetComponent<AudioSource>();
    }

    public void PlayOneShot(AudioClip audioClip)
    {
        var spawnObj = Instantiate(SFXContents, this.transform);
        spawnObj.GetComponent<AudioSource>().clip = audioClip;
        spawnObj.GetComponent<SFXContent>().PlayAndDestroy();
    }
    public void PlayLoop(AudioClip audioClip)
    {
        _audioSource.Stop();
        _audioSource.clip = audioClip;
        _audioSource.Play();
    }
    
    public void PlayBGMOneShot(AudioClip audioClip)
    {
        _audioSource.Stop();
        _audioSource.loop = false;
        _audioSource.clip = audioClip;
        _audioSource.Play();
    }
}
