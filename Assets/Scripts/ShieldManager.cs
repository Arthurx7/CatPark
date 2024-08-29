using UnityEngine;

public class ShieldManager : MonoBehaviour
{
    public GameObject[] players;  // Array de jugadores
    public GameObject[] shields;  // Array de escudos correspondientes a cada jugador

    void Start()
    {
        // Desactivar todos los escudos inicialmente
        foreach (GameObject shield in shields)
        {
            shield.SetActive(false);
        }

        // Seleccionar aleatoriamente dos jugadores para darles escudos
        int[] selectedPlayers = SelectRandomPlayers();

        // Activar los escudos de los jugadores seleccionados
        foreach (int index in selectedPlayers)
        {
            shields[index].SetActive(true);
        }
    }

    int[] SelectRandomPlayers()
    {
        int[] selectedPlayers = new int[2];
        int firstPlayer = Random.Range(0, players.Length);
        int secondPlayer;

        // Asegurarse de que el segundo jugador no sea el mismo que el primero
        do
        {
            secondPlayer = Random.Range(0, players.Length);
        }
        while (secondPlayer == firstPlayer);

        selectedPlayers[0] = firstPlayer;
        selectedPlayers[1] = secondPlayer;

        return selectedPlayers;
    }
}
