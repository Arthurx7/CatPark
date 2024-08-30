using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public List<Transform> players; // Lista de transformaciones de los jugadores
    public float smoothSpeed = 0.125f; // Velocidad para suavizar el movimiento de la cámara
    public float minCameraSize = 5f; // Tamaño mínimo de la cámara para evitar que se acerque demasiado
    public float maxCameraSize = 5f; // Tamaño máximo de la cámara para evitar que se aleje demasiado
    public float horizontalOffset = 0f; // Offset para ajustar la cámara horizontalmente

    public Transform leftColliderTransform;  // Transform del collider izquierdo
    public Transform rightColliderTransform; // Transform del collider derecho
    public float colliderOffset = 2f; // Distancia de los colliders al centro de la cámara

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (players.Count == 0)
            return;

        // Calcular la posición media de todos los jugadores
        Vector3 centerPoint = GetCenterPoint();
        
        // Ajustar la posición de la cámara para centrarla en el punto medio de los jugadores
        Vector3 newPosition = new Vector3(centerPoint.x + horizontalOffset, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed);

        // Ajustar el tamaño de la cámara para incluir a todos los jugadores horizontalmente
        float newSize = Mathf.Lerp(cam.orthographicSize, GetRequiredCameraSize(), smoothSpeed);
        cam.orthographicSize = Mathf.Clamp(newSize, minCameraSize, maxCameraSize);

        // Actualizar las posiciones de los colliders laterales para que sigan a la cámara
        UpdateCollidersPosition();
    }

    private Vector3 GetCenterPoint()
    {
        if (players.Count == 1)
        {
            return players[0].position;
        }

        var bounds = new Bounds(players[0].position, Vector3.zero);
        for (int i = 1; i < players.Count; i++)
        {
            bounds.Encapsulate(players[i].position);
        }

        return bounds.center;
    }

    private float GetRequiredCameraSize()
    {
        if (players.Count == 1)
        {
            return minCameraSize;
        }

        // Encontrar el tamaño requerido en base a la distancia horizontal entre los jugadores más a la izquierda y más a la derecha
        float minX = players[0].position.x;
        float maxX = players[0].position.x;

        for (int i = 1; i < players.Count; i++)
        {
            if (players[i].position.x < minX)
                minX = players[i].position.x;
            if (players[i].position.x > maxX)
                maxX = players[i].position.x;
        }

        float width = maxX - minX;
        float size = width / 2f / cam.aspect; // Ajustar el tamaño basado en el aspecto de la cámara

        return Mathf.Max(size, minCameraSize);
    }

    private void UpdateCollidersPosition()
    {
        // Calcular las nuevas posiciones de los colliders basado en la posición actual de la cámara
        if (leftColliderTransform != null)
        {
            leftColliderTransform.position = new Vector3(transform.position.x - cam.orthographicSize * cam.aspect - colliderOffset, leftColliderTransform.position.y, leftColliderTransform.position.z);
        }

        if (rightColliderTransform != null)
        {
            rightColliderTransform.position = new Vector3(transform.position.x + cam.orthographicSize * cam.aspect + colliderOffset, rightColliderTransform.position.y, rightColliderTransform.position.z);
        }
    }
}
