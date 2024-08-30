using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movimiento2 : MonoBehaviour
{
    private PlayerMove player;
    private InputActionMap actionMap;
    private PlayerInput playerInput;

    public AudioSource saltoSound;

    [Header("Movimiento jugador")]
    public Vector2 direccion;
    public Rigidbody rb;

    [SerializeField] private float velMov;
    [SerializeField] private float salto;
    [SerializeField] private Transform checkPiso;
    [SerializeField] private LayerMask capaPiso;

    private bool puedeSaltar = false;
    private bool tieneJugadorEncima = false;
    private bool estaSobreJugador = false;

    private Vector3 currentRotation = Vector3.zero;
    private GameObject jugadorDebajo;

    private Animator animator;

    private void Awake()
    {
        player = new PlayerMove();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();

        if (playerInput != null)
        {
            actionMap = playerInput.currentActionMap;
        }

        // Inicializar el personaje mirando hacia la derecha
        currentRotation = new Vector3(0, 90, 0);
        transform.eulerAngles = currentRotation;
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
        // Permitir el salto si el personaje está en el suelo y no tiene otro jugador encima.
        if (enElPiso() && !tieneJugadorEncima)
        {
            puedeSaltar = true;
            animator.SetBool("isJumping", true); // Activar la animación de salto inmediatamente
            saltoSound.Play();
        }
    }

    private void FixedUpdate()
    {
        // Movimiento del personaje
        Vector3 movimiento = new Vector3(velMov * direccion.x, rb.velocity.y, velMov * direccion.y);
        rb.velocity = movimiento;

        // Controlar las animaciones de caminar y salto
        ControlarAnimacion(movimiento);

        // Salto
        if (puedeSaltar)
        {
            rb.AddForce(Vector3.up * salto, ForceMode.VelocityChange);
            puedeSaltar = false;
        }

        // Girar el personaje según la dirección
        if (direccion.x > 0)
        {
            currentRotation = new Vector3(0, 90, 0); // Mirando a la derecha
        }
        else if (direccion.x < 0)
        {
            currentRotation = new Vector3(0, -90, 0); // Mirando a la izquierda
        }
        transform.eulerAngles = currentRotation;

        // Si el personaje está sobre otro y el personaje de abajo se mueve, mover también al personaje de arriba.
        if (estaSobreJugador && jugadorDebajo != null && jugadorDebajo.GetComponent<Movimiento>() != null)
        {
            Vector3 movimientoJugadorDebajo = jugadorDebajo.GetComponent<Movimiento>().direccion;
            transform.position += new Vector3(movimientoJugadorDebajo.x * velMov * Time.fixedDeltaTime, 0, 0);
        }
    }

    private void ControlarAnimacion(Vector3 movimiento)
    {
        bool isWalking = movimiento.x != 0 || movimiento.z != 0;
        animator.SetBool("isWalking", isWalking);
    

        // Desactivar la animación de salto cuando aterriza en el suelo
        if (enElPiso())
        {
            animator.SetBool("isJumping", false);
          
        }
    }

    private bool enElPiso()
    {
        return Physics.CheckSphere(checkPiso.position, 0.1f, capaPiso) || estaSobreJugador;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Si estamos tocando a otro jugador por arriba
            if (collision.contacts[0].point.y > transform.position.y)
            {
                tieneJugadorEncima = true;
            }
            // Si estamos tocando a otro jugador por abajo
            else
            {
                estaSobreJugador = true;
                jugadorDebajo = collision.gameObject;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Restablecer las variables cuando termina la colisión
            tieneJugadorEncima = false;
            estaSobreJugador = false;
            jugadorDebajo = null;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.contacts.Length > 0)
            {
                // Actualizar el estado basado en la posición de los contactos
                if (collision.contacts[0].point.y > transform.position.y)
                {
                    tieneJugadorEncima = true;
                }
                else
                {
                    estaSobreJugador = true;
                    jugadorDebajo = collision.gameObject;
                }
            }
        }
    }
}