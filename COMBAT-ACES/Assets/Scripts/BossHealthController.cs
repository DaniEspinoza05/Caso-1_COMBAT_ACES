using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthController : MonoBehaviour
{
    [SerializeField]
    int maxHits = 20; // Número máximo de impactos antes de destruir al boss

    private int currentHits = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            currentHits++;
            Destroy(collision.gameObject); // Destruir la bala del jugador al impactar
            Debug.Log("Boss hit! Current hits: " + currentHits);

            if (currentHits >= maxHits)
            {
                Destroy(gameObject);
                Debug.Log("Boss destroyed!");
            }
        }
    }
}
