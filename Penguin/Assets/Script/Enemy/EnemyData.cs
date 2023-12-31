using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObject/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public GameObject Enemy;
    public float prob;
    public int point;
    public AudioClip _spawnSFX;
}
