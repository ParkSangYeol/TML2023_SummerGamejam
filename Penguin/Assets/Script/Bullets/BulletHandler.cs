using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    public float speed;
    public int damage;
    public bool isPoison;

    private void Update()
    {
        this.transform.position += new Vector3(0, Mathf.Lerp(0, speed, Time.deltaTime), 0);
    }
}
