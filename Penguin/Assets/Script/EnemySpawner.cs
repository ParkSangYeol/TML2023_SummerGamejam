using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool stopTrigger = true;
    IEnumerator CreateEnemyRoutine()
    {
        while (stopTrigger)
        {
            EnemySpawn();
            yield return new WaitForSeconds(3.0f);
        }
    }
    public void EnemySpawn()
    {
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(UnityEngine.Random.Range(0.0f, 1.0f), 1.1f, 0));
        pos.z = 0.0f;
        Instantiate(Enemy, pos, Quaternion.identity);
        count++;
    }
}
