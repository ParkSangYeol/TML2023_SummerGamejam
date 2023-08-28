using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        
        transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(inputHorizontal * speed,0, 0), Time.deltaTime);
    }
}
