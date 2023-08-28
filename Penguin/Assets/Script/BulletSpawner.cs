using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject Bullet;
    public Transform bulletSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void BulletSpawn()
    {
        var bulletGo = Instantiate(Bullet);
        bulletGo.transform.position = this.bulletSpawnPoint.position;
    }
}
