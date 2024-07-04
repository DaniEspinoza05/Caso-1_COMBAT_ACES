using UnityEngine;

public class BossChaseController : MonoBehaviour
{
    public Transform player;

    [SerializeField]
    Transform targetTransform;

    [SerializeField]
    float speed = 50.0f;

    [SerializeField]
    float stopDistance = 30.0f;

    [SerializeField]
    float retreatDistance = 20.0f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (targetTransform != null)
        {
            // Direccion hacia el jugador
            Vector3 direction = (targetTransform.position - transform.position).normalized;

            // Distancia entre el Boss y el jugador
            float distance = Vector3.Distance(targetTransform.position, transform.position);

            //Debug.Log($"Distance to player: {distance}");

            if (distance > stopDistance)
            {
                // Moverse hacia el jugador
                rb.MovePosition(Vector3.MoveTowards(rb.position, targetTransform.position, speed * Time.fixedDeltaTime));
                //Debug.Log("Moving towards player");
            }
            else if (distance < retreatDistance)
            {
                // Retroceder del jugador
                rb.MovePosition(Vector3.MoveTowards(rb.position, transform.position - direction, speed * Time.fixedDeltaTime));
                //Debug.Log("Retreating from player");
            }
            else
            {
                // Detener el movimiento
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                //Debug.Log("Stopping");
            }

            // Mirar al jugador
            transform.LookAt(targetTransform);
        }
    }

    //Este gizmos está hecho para que se pueda ver el rango en el que el boss se detiene (circulo verde)
    //y el circulo rojo es el rango del retreat
    void OnDrawGizmos()
    {
        if (targetTransform != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, stopDistance);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, retreatDistance);
        }
    }
}



