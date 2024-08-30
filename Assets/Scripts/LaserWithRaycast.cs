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

        // Inicia el rayo desde laserStart hacia abajo
        if (Physics.Raycast(laserStart.position, direction, out hit))
        {
            lineRenderer.SetPosition(1, hit.point);

            // Si el rayo detecta el escudo o el jugador
            if (hit.transform != null && (hit.transform.CompareTag("Shield") ||
                                          hit.transform == player1.transform ||
                                          hit.transform == player2.transform ||
                                          hit.transform == player3.transform ||
                                          hit.transform == player4.transform))
            {
                // Reposicionar y activar las partículas
                currentImpactParticles.transform.position = hit.point;

                // Asignar una rotación específica a las partículas
                // Por ejemplo, si quieres que la rotación sea fija en el eje Y en 90 grados:
                Quaternion desiredRotation = Quaternion.Euler(-90, -270, 0);
                currentImpactParticles.transform.rotation = desiredRotation;

                if (!currentImpactParticles.activeInHierarchy)
                {
                    currentImpactParticles.SetActive(true);
                }

                // Si el rayo detecta al jugador, destruirlo
                if (hit.transform == player1.transform)
                {
                    Destroy(player1);
                    player1 = null;
                }
                else if (hit.transform == player2.transform)
                {
                    Destroy(player2);
                    player2 = null;
                }
                else if (hit.transform == player3.transform)
                {
                    Destroy(player3);
                    player3 = null;
                }
                else if (hit.transform == player4.transform)
                {
                    Destroy(player4);
                    player4 = null;
                }
            }
            else
            {
                // Si no hay impacto, extender el láser a la distancia máxima y desactivar las partículas
                lineRenderer.SetPosition(1, laserStart.position + direction * 100);
                if (currentImpactParticles.activeInHierarchy)
                {
                    currentImpactParticles.SetActive(false);
                }
            }
        }
        else
        {
            // Si no se detecta nada, extender el láser hacia abajo y desactivar las partículas
            lineRenderer.SetPosition(1, laserStart.position + direction * 100);
            if (currentImpactParticles.activeInHierarchy)
            {
                currentImpactParticles.SetActive(false);
            }
        }
    }
}
