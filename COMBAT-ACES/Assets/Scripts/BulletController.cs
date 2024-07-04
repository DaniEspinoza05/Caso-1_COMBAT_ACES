using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    LayerMask targetMask;

    [SerializeField]
    public float speed;

    Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _rigidbody.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        
        if ((targetMask & (1 << other.gameObject.layer)) != 0)
        {
            Destroy(other.gameObject);
        }

        
        Destroy(gameObject);
    }
}
