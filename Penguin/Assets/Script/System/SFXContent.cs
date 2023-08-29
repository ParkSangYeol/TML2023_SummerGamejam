using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXContent : MonoBehaviour
{
    private AudioSource _audioSource;

    public void PlayAndDestroy()
    {
        if (_audioSource == null)
        {
            _audioSource = this.GetComponent<AudioSource>();
        }
        StartCoroutine(LastPlay());
    }

    IEnumerator LastPlay()
    {
        _audioSource.Play();
        Debug.Log(_audioSource.clip.length);
        yield return new WaitForSeconds(_audioSource.clip.length);
        Destroy(this.gameObject);
    }
}
