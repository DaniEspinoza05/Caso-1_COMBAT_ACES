using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] spawningPrefabs;

    [SerializeField]
    GameObject bossPrefab;

    [SerializeField]
    int maxSpawningAmount;

    [SerializeField]
    float spawningRange;

    [SerializeField]
    float spawningSafeRange;

    [SerializeField]
    Transform target;

    private List<GameObject> activeEnemies = new List<GameObject>();

    private void Start()
    {
        
        if (bossPrefab != null)
        {
            bossPrefab.SetActive(false);
        }

        
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        for (int spawningIndex = 0; spawningIndex < maxSpawningAmount; spawningIndex++)
        {
            Vector3 spawningPoint = Vector3.zero;
            while (Vector3.Distance(spawningPoint, Vector3.zero) < spawningSafeRange)
            {
                spawningPoint = GetSpawningPoint();
            }

            GameObject spawningPrefab = spawningPrefabs[Random.Range(0, spawningPrefabs.Length)];
            GameObject spawningObject = Instantiate(spawningPrefab, spawningPoint, Quaternion.identity);
            ChaseController chaseController = spawningObject.GetComponent<ChaseController>();
            chaseController.SetTarget(target);
            spawningObject.transform.parent = transform;

            activeEnemies.Add(spawningObject);
        }
    }

    private Vector3 GetSpawningPoint()
    {
        float x = Random.Range(-1.0F, 1.0F);
        float y = Random.Range(-1.0F, 1.0F);
        float z = Random.Range(-1.0F, 1.0F);

        Vector3 spawningPoint = new Vector3(x, y, z);
        if (spawningPoint.magnitude > 1.0F)
        {
            spawningPoint.Normalize();
        }
        spawningPoint *= spawningRange;

        return spawningPoint;
    }

    public void EnemyDefeated(GameObject enemy)
    {
        if (enemy != null)
        {
            activeEnemies.Remove(enemy);
            if (activeEnemies.Count == 0)
            {
                ActivateBoss();
            }
        }
    }

    private void ActivateBoss()
    {
        if (bossPrefab != null)
        {
           
            bossPrefab.SetActive(true);
            BossChaseController bossChaseController = bossPrefab.GetComponent<BossChaseController>();
            bossChaseController.player = target; 

        }
    }
}
