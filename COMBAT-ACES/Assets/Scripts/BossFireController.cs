using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireController : MonoBehaviour
{
    [SerializeField]
    Transform[] firePoints;

    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    float fireDelay;

    [SerializeField]
    float lifeTimeout;

    [SerializeField]
    Transform player; 

    [SerializeField]
    float shootRange = 10.0f; 

    float _currentTime;

    private void Start()
    {
        _currentTime = fireDelay;
    }

    private void Update()
    {
        _currentTime -= Time.deltaTime;

        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= shootRange && _currentTime < 0.0f)
            {
                Fire();
                _currentTime = fireDelay;
            }
        }
    }

    private void Fire()
    {
        foreach (Transform firePoint in firePoints)
        {
            if (bulletPrefab != null)
            {
                Vector3 direction = (player.position - firePoint.position).normalized;
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(direction));
                bullet.tag = "BossBullet"; 
                bullet.layer = LayerMask.NameToLayer("BossBullet"); 
                bullet.GetComponent<Rigidbody>().velocity = direction * bullet.GetComponent<BulletController>().speed;
                Destroy(bullet, lifeTimeout);
            }
        }

    }
}
