using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movimiento : MonoBehaviour
{
    private PlayerMove player;
    [Header("Movimiento jugaror")]
    public Vector2 direccion;
    public Rigidbody rb;

    [SerializeField]
    public float velMov;

    private void Awake()
    {
        player = new PlayerMove();

    }

    private void OnEnable()
    {
        player.Enable();
        player.Player.Move.started += StartMove;
        //player.Player.Move.performed
        player.Player.Move.canceled += CancelMove;
    }

    private void OnDisable()
    {
        player.Disable();
        player.Player.Move.started -= StartMove;
        //player.Player.Move.performed
        player.Player.Move.canceled -= CancelMove;
    }

    private void StartMove(InputAction.CallbackContext context)
    {
        direccion = player.Player.Move.ReadValue<Vector2>();
    }

    private void CancelMove(InputAction.CallbackContext context)
    {
        direccion = player.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(velMov * direccion.x, 0f, velMov * direccion.y);
    }
}
