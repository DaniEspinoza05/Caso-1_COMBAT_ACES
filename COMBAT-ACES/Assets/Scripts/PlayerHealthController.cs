using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("BossBullet"))
        {
            Destroy(gameObject); 
            Application.Quit();
        }
    }
}
