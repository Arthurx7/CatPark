using UnityEngine;

public class ShieldFollow : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public Vector3 offset;   // Offset de la posici�n del escudo respecto al jugador

    void Update()
    {
        // Actualiza la posici�n del escudo para seguir al jugador con un offset
        transform.position = player.position + offset;
    }
}