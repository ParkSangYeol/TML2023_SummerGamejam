using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("PlayerBullet") || other.tag.Equals("EnemyBullet"))
        {
            Destroy(other.gameObject);
        }
    }
}
