using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public Transform laserStart; // El punto de inicio del láser
    public GameObject shield1;    // Referencia al escudo del jugador
    public GameObject player1;    // Referencia al jugador
    public GameObject shield2;
    public GameObject player2;
    public GameObject shield3;
    public GameObject player3;
    public GameObject shield4;
    public GameObject player4;

    public GameObject impactParticlesPrefab; // Prefab de partículas de impacto
    private GameObject currentImpactParticles; // Instancia actual de partículas

    private LineRenderer lineRenderer;
    public float laserRange = 100f; // Rango máximo del láser

    public Transform player1Spawn;  // Transform para la posición de reinicio del jugador 1
    public Transform player2Spawn;  // Transform para la posición de reinicio del jugador 2
    public Transform player3Spawn;  // Transform para la posición de reinicio del jugador 3
    public Transform player4Spawn;  // Transform para la posición de reinicio del jugador 4

    public LayerMask raycastLayerMask; // Máscara de capas para el raycast

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, laserStart.position); // Posición inicial del láser

        // Crear una instancia única de las partículas, pero desactivada inicialmente
        currentImpactParticles = Instantiate(impactParticlesPrefab);
        currentImpactParticles.SetActive(false);
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 direction = Vector3.down; // Dirección del láser hacia abajo
        Vector3 startPosition = laserStart.position; // Posición inicial del rayo

        // Realizar el raycast usando la máscara de capas para detectar las capas específicas
        if (Physics.Raycast(startPosition, direction, out hit, laserRange, raycastLayerMask))
        {
            // Si se detecta un objeto con el tag "Check", ignorarlo y continuar
            if (hit.transform.CompareTag("Check"))
            {
                // Mueve la posición de inicio un poco adelante para continuar el rayo
                startPosition = hit.point + direction * 0.01f;
                // Realiza un nuevo raycast para continuar detectando más allá del objeto con tag "Check"
                if (Physics.Raycast(startPosition, direction, out hit, laserRange, raycastLayerMask))
                {
                    lineRenderer.SetPosition(1, hit.point);

                    // Si el rayo detecta el escudo o el jugador
                    if (hit.transform != null && (hit.transform.CompareTag("Shield") ||
                                                  hit.transform == player1.transform ||
                                                  hit.transform == player2.transform ||
                                                  hit.transform == player3.transform ||
                                                  hit.transform == player4.transform || hit.transform.CompareTag("Untagged")))
                    {
                        // Reposicionar y activar las partículas
                        currentImpactParticles.transform.position = hit.point;

                        // Asignar una rotación específica a las partículas
                        Quaternion desiredRotation = Quaternion.Euler(-90, -270, 0);
                        currentImpactParticles.transform.rotation = desiredRotation;

                        if (!currentImpactParticles.activeInHierarchy)
                        {
                            currentImpactParticles.SetActive(true);
                        }

                        // Si el rayo detecta al jugador, moverlo a su posición de reinicio
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
                        // Si no hay impacto relevante, extender el láser a la distancia máxima y desactivar las partículas
                        lineRenderer.SetPosition(1, laserStart.position + direction * laserRange);
                        if (currentImpactParticles.activeInHierarchy)
                        {
                            currentImpactParticles.SetActive(false);
                        }
                    }
                }
            }
            else
            {
                lineRenderer.SetPosition(1, hit.point);

                // Si el rayo detecta el escudo o el jugador
                if (hit.transform != null && (hit.transform.CompareTag("Shield") ||
                                              hit.transform == player1.transform ||
                                              hit.transform == player2.transform ||
                                              hit.transform == player3.transform ||
                                              hit.transform == player4.transform || hit.transform.CompareTag("Untagged")))
                {
                    // Reposicionar y activar las partículas
                    currentImpactParticles.transform.position = hit.point;

                    // Asignar una rotación específica a las partículas
                    Quaternion desiredRotation = Quaternion.Euler(-90, -270, 0);
                    currentImpactParticles.transform.rotation = desiredRotation;

                    if (!currentImpactParticles.activeInHierarchy)
                    {
                        currentImpactParticles.SetActive(true);
                    }

                    // Si el rayo detecta al jugador, moverlo a su posición de reinicio
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
                    // Si no hay impacto relevante, extender el láser a la distancia máxima y desactivar las partículas
                    lineRenderer.SetPosition(1, laserStart.position + direction * laserRange);
                    if (currentImpactParticles.activeInHierarchy)
                    {
                        currentImpactParticles.SetActive(false);
                    }
                }
            }
        }
        else
        {
            // Si no se detecta nada, extender el láser hacia la distancia máxima y desactivar las partículas
            lineRenderer.SetPosition(1, laserStart.position + direction * laserRange);
            if (currentImpactParticles.activeInHierarchy)
            {
                currentImpactParticles.SetActive(false);
            }
        }
    }
}