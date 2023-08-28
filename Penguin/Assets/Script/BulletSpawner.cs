using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public BulletContainer container;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void BulletSpawn(Vector3 spawnPos)
    {
        var bulletGo = Instantiate(container.GetRandomBullet());
        bulletGo.transform.position = spawnPos;
    }
}
