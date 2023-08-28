using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Animator _animator;

    private void Start()
    {
        _animator = this.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        _animator.SetFloat("Dir", inputHorizontal);
        
        transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(inputHorizontal * speed,0, 0), Time.deltaTime);
    }
}
