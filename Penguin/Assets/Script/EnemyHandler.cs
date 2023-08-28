using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyEnemy()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Shoot()
    {
        BulletSpawner.Instance.BulletSpawn(this.transform.position);
    }
}
