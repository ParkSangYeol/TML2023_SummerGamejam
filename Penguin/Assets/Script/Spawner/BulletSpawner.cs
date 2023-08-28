using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public BulletContainer container;
    public float shootDelay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BulletSpawn(Vector3 spawnPos, List<Quaternion> rotations, int iterTime)
    {
        StartCoroutine(StartBulletSpawn(spawnPos, rotations, iterTime));
    }

    IEnumerator StartBulletSpawn(Vector3 spawnPos, List<Quaternion> rotations, int iterTime)
    {
        for (int i = 0; i < iterTime; ++i)
        {
            foreach (var rotation in rotations)
            {
                var spawnObj = Instantiate(container.GetRandomBullet(), spawnPos, rotation);
            }

            yield return new WaitForSeconds(shootDelay);
        }
    }
}
