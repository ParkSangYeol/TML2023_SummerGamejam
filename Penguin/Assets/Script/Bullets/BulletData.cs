using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "ScriptableObject/Bullet Data")]
public class BulletData : ScriptableObject
{
    public GameObject Bullet;
    public float prob;
}
