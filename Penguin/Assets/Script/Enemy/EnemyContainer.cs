using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Container", menuName = "ScriptableObject/Enemy Container")]
public class EnemyContainer : SerializedScriptableObject
{
    public List<EnemyData> enemies;

    public GameObject GetRandomEnemy()
    {
        float probRange = 0;
        foreach (var enemy in enemies)
        {
            probRange += enemy.prob;
        }

        float randomNum = UnityEngine.Random.Range(0, probRange);
        
        foreach (var enemy in enemies)
        {
            if (randomNum > enemy.prob)
            {
                randomNum -= enemy.prob;
            }
            else
            {
                return enemy.Enemy;
            }
        }

        Debug.LogError("Random Error out of range");
        return null;
    }
}