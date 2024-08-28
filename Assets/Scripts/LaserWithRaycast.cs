using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public Transform laserStart; // El punto de inicio del l�ser
    public GameObject shield1;    // Referencia al escudo del jugador
    public GameObject player1;    // Referencia al jugador
    public GameObject shield2;
    public GameObject player2;
    public GameObject shield3;
    public GameObject player3;
    public GameObject shield4;
    public GameObject player4;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, laserStart.position); // Posici�n inicial del l�ser
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 direction = Vector3.down; // Direcci�n del l�ser hacia abajo

        // Inicia el rayo desde laserStart hacia abajo
        if (Physics.Raycast(laserStart.position, direction, out hit))
        {
            // Si el rayo detecta el escudo
            if (hit.transform != null && hit.transform.CompareTag("Shield"))
            {
                lineRenderer.SetPosition(1, hit.point);
            }
            // Si el rayo detecta al jugador y el objeto no es nulo
            else if (hit.transform != null && hit.transform == player1.transform)
            {
                // Destruye el objeto del jugador
                Destroy(player1);

                // Evita intentar acceder al jugador despu�s de destruirlo
                player1 = null;
            }
            else if (hit.transform != null && hit.transform == player2.transform)
            {
                // Destruye el objeto del jugador
                Destroy(player2);

                // Evita intentar acceder al jugador despu�s de destruirlo
                player2 = null;
            }
            else if (hit.transform != null && hit.transform == player3.transform)
            {
                // Destruye el objeto del jugador
                Destroy(player3);

                // Evita intentar acceder al jugador despu�s de destruirlo
                player3 = null;
            }
            else if (hit.transform != null && hit.transform == player4.transform)
            {
                // Destruye el objeto del jugador
                Destroy(player4);

                // Evita intentar acceder al jugador despu�s de destruirlo
                player4 = null;
            }
            else
            {
                // Si el rayo no detecta el escudo ni al jugador, extiende el l�ser a una distancia m�xima hacia abajo
                lineRenderer.SetPosition(1, laserStart.position + direction * 100); // 100 es una distancia arbitraria
            }
        }
        else
        {
            // Si no detecta nada, extiende el l�ser hacia abajo a la distancia m�xima
            lineRenderer.SetPosition(1, laserStart.position + direction * 100);
        }
    }
}
