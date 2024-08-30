using UnityEngine;
using System.Collections.Generic; // Para usar listas

public class ShieldManager : MonoBehaviour
{
    public GameObject[] players;  // Array de jugadores
    public GameObject[] shields;  // Array de escudos correspondientes a cada jugador

    private HashSet<GameObject> playersInsideTrigger = new HashSet<GameObject>(); // Lista de jugadores dentro del trigger

    void Start()
    {
        // Desactivar todos los escudos inicialmente
        foreach (GameObject shield in shields)
        {
            shield.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra en el Trigger es uno de los jugadores
        if (IsPlayer(other.gameObject))
        {
            playersInsideTrigger.Add(other.gameObject); // Añadir el jugador a la lista de jugadores dentro del trigger

            // Si todos los jugadores están dentro del Trigger, activar los escudos
            if (playersInsideTrigger.Count == players.Length)
            {
                ActivateRandomShield();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Verificar si el objeto que sale del Trigger es uno de los jugadores
        if (IsPlayer(other.gameObject))
        {
            playersInsideTrigger.Remove(other.gameObject); // Quitar el jugador de la lista

            // Desactivar todos los escudos cuando cualquier jugador salga del Trigger
            DeactivateAllShields();
        }
    }

    bool IsPlayer(GameObject obj)
    {
        // Verificar si el objeto es uno de los jugadores
        foreach (GameObject player in players)
        {
            if (obj == player)
            {
                return true;
            }
        }
        return false;
    }

    void ActivateRandomShield()
    {
        // Seleccionar aleatoriamente un jugador para darles escudo
        int randomIndex = Random.Range(0, players.Length);
        shields[randomIndex].SetActive(true);
    }

    void DeactivateAllShields()
    {
        // Desactivar todos los escudos
        foreach (GameObject shield in shields)
        {
            shield.SetActive(false);
        }
    }
}
