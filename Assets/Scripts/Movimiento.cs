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

      [SerializeField]
    public float salto;

    [SerializeField] private Transform checkPiso;
    [SerializeField] private LayerMask capaPiso;

    private bool puedeSaltar = false;
    
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

        player.Player.Salto.started += StartJump;
        //player.Player.Move.performed
        player.Player.Salto.canceled += CancelJump;
    }

    private void OnDisable()
    {
        player.Disable();
        player.Player.Move.started -= StartMove;
        //player.Player.Move.performed
        player.Player.Move.canceled -= CancelMove;

        player.Player.Salto.started -= StartJump;
        //player.Player.Move.performed
        player.Player.Salto.canceled -= CancelJump;
    }

    private void StartMove(InputAction.CallbackContext context)
    {
        direccion = player.Player.Move.ReadValue<Vector2>();
    }

    private void CancelMove(InputAction.CallbackContext context)
    {
        direccion = Vector2.zero;
    }

    private void StartJump(InputAction.CallbackContext context)
    {
        puedeSaltar = true;
        
    }

    private void CancelJump(InputAction.CallbackContext context)
    {
        puedeSaltar = false;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(velMov * direccion.x, 0f, velMov * direccion.y);
    }

    void Update()
    {
        if(puedeSaltar && enElPiso())
        {
            Debug.Log("Salta");
            rb.AddForce(Vector3.up * salto, ForceMode.Impulse);
            puedeSaltar = false;
        }
    }

      private bool enElPiso()
    {
        return Physics.CheckSphere(checkPiso.position, 0.2f, capaPiso);
    }
}
