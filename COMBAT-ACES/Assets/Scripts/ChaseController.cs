using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseController : MonoBehaviour
{
    Transform _target;
    [SerializeField] float speed = 5f; 
    private SpawnManager spawnManager;

    void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
    }

    void Update()
    {
        if (_target != null)
        {
            
            Vector3 direction = (_target.position - transform.position).normalized;

           
            transform.position += direction * speed * Time.deltaTime;

            
            transform.LookAt(_target);
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    void OnDestroy()
    {
        if (spawnManager != null)
        {
            spawnManager.EnemyDefeated(gameObject);
        }
    }
}
