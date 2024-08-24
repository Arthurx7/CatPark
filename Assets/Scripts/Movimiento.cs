using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movimiento : MonoBehaviour
{
    private PlayerMove player;
    private InputActionMap actionMap;
    private PlayerInput playerInput;

    [Header("Movimiento jugador")]
    public Vector2 direccion;
    public Rigidbody rb;

    [SerializeField] private float velMov;
    [SerializeField] private float salto;
    [SerializeField] private Transform checkPiso;
    [SerializeField] private LayerMask capaPiso;

    private bool puedeSaltar = false;

    private void Awake()
    {
        player = new PlayerMove();
        playerInput = GetComponent<PlayerInput>(); // Obtener el componente PlayerInput

        if (playerInput != null)
        {
            actionMap = playerInput.currentActionMap; // Obtener el mapa de acciones activo
        }
    }

    private void OnEnable()
    {
        player.Enable();
        if (actionMap != null)
        {
            actionMap["Move"].started += StartMove;
            actionMap["Move"].canceled += CancelMove;
            actionMap["Salto"].started += StartJump;
        }
    }

    private void OnDisable()
    {
        player.Disable();
        if (actionMap != null)
        {
            actionMap["Move"].started -= StartMove;
            actionMap["Move"].canceled -= CancelMove;
            actionMap["Salto"].started -= StartJump;
        }
    }

    private void StartMove(InputAction.CallbackContext context)
    {
        direccion = context.ReadValue<Vector2>();
    }

    private void CancelMove(InputAction.CallbackContext context)
    {
        direccion = Vector2.zero;
    }

    private void StartJump(InputAction.CallbackContext context)
    {
        // Solo permitimos el salto si el personaje está en el suelo.
        if (enElPiso())
        {
            puedeSaltar = true;
        }
    }

    private void FixedUpdate()
    {
        // Movimiento
        rb.velocity = new Vector3(velMov * direccion.x, rb.velocity.y, velMov * direccion.y);

        // Salto
        if (puedeSaltar)
        {
            rb.AddForce(Vector3.up * salto, ForceMode.VelocityChange);
            puedeSaltar = false;
        }
    }

    private bool enElPiso()
    {
        return Physics.CheckSphere(checkPiso.position, 0.1f, capaPiso);
    }
}
