using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet Container", menuName = "ScriptableObject/Bullet Container")]
public class BulletContainer : SerializedScriptableObject
{
    public List<BulletData> bullets;

    public GameObject GetRandomBullet()
    {
        float probRange = 0;
        foreach (var bullet in bullets)
        {
            probRange += bullet.prob;
        }

        float randomNum = UnityEngine.Random.Range(0, probRange);
        
        foreach (var bullet in bullets)
        {
            if (randomNum > bullet.prob)
            {
                randomNum -= bullet.prob;
            }
            else
            {
                return bullet.Bullet;
            }
        }

        Debug.LogError("Random Error out of range");
        return null;
    }
}