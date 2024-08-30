using UnityEngine;

public class LaserLeft : MonoBehaviour
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

    public GameObject impactParticlesPrefab; // Prefab de part�culas de impacto
    private GameObject currentImpactParticles; // Instancia actual de part�culas

    private LineRenderer lineRenderer;

    public Transform player1Spawn;  // Transform para la posici�n de reinicio del jugador 1
    public Transform player2Spawn;  // Transform para la posici�n de reinicio del jugador 2
    public Transform player3Spawn;  // Transform para la posici�n de reinicio del jugador 3
    public Transform player4Spawn;  // Transform para la posici�n de reinicio del jugador 4

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, laserStart.position); // Posici�n inicial del l�ser

        // Crear una instancia �nica de las part�culas, pero desactivada inicialmente
        currentImpactParticles = Instantiate(impactParticlesPrefab);
        currentImpactParticles.SetActive(false);
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 direction = Vector3.left; // Direcci�n del l�ser hacia la derecha

        // Inicia el rayo desde laserStart hacia la derecha
        if (Physics.Raycast(laserStart.position, direction, out hit))
        {
            lineRenderer.SetPosition(1, hit.point);

            // Si el rayo detecta el escudo o el jugador
            if (hit.transform != null && (hit.transform.CompareTag("Shield") ||
                                          hit.transform == player1.transform ||
                                          hit.transform == player2.transform ||
                                          hit.transform == player3.transform ||
                                          hit.transform == player4.transform || hit.transform.CompareTag("Untagged")))
            {
                // Reposicionar y activar las part�culas
                currentImpactParticles.transform.position = hit.point;

                // Asignar una rotaci�n espec�fica a las part�culas
                Quaternion desiredRotation = Quaternion.Euler(0, -270, 0);
                currentImpactParticles.transform.rotation = desiredRotation;

                if (!currentImpactParticles.activeInHierarchy)
                {
                    currentImpactParticles.SetActive(true);
                }

                // Si el rayo detecta al jugador, moverlo a su posici�n de reinicio
                if (hit.transform == player1.transform)
                {
                    player1.transform.position = player1Spawn.position; // Mover al jugador 1 a su punto de reinicio
                }
                else if (hit.transform == player2.transform)
                {
                    player2.transform.position = player2Spawn.position; // Mover al jugador 2 a su punto de reinicio
                }
                else if (hit.transform == player3.transform)
                {
                    player3.transform.position = player3Spawn.position; // Mover al jugador 3 a su punto de reinicio
                }
                else if (hit.transform == player4.transform)
                {
                    player4.transform.position = player4Spawn.position; // Mover al jugador 4 a su punto de reinicio
                }
            }
            else
            {
                // Si no hay impacto, extender el l�ser a la distancia m�xima y desactivar las part�culas
                lineRenderer.SetPosition(1, laserStart.position + direction * 100);
                if (currentImpactParticles.activeInHierarchy)
                {
                    currentImpactParticles.SetActive(false);
                }
            }
        }
        else
        {
            // Si no se detecta nada, extender el l�ser hacia la derecha y desactivar las part�culas
            lineRenderer.SetPosition(1, laserStart.position + direction * 100);
            if (currentImpactParticles.activeInHierarchy)
            {
                currentImpactParticles.SetActive(false);
            }
        }
    }
}
