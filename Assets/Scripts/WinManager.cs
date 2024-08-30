using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class WinManager : MonoBehaviour
{
   public bool tieneLlave = false;

   public GameObject[] players;

   private HashSet<GameObject> playersInsideTrigger = new HashSet<GameObject>();


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

    void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra en el Trigger es uno de los jugadores
        if (IsPlayer(other.gameObject))
        {
            playersInsideTrigger.Add(other.gameObject); // A�adir el jugador a la lista de jugadores dentro del trigger

            // Si todos los jugadores est�n dentro del Trigger, activar los escudos
            if (playersInsideTrigger.Count == players.Length && tieneLlave)
            {
                VictoriaMagistral();
            }

          
        }
    }

    void VictoriaMagistral()
    {
        Debug.Log("Victoria");
       // SceneManager.LoadScene("Finish");
    }

}
