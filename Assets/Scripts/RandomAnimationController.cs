using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimationController : MonoBehaviour
{
    // Lista de nombres de animaciones
    public List<string> animationNames;
    private Animator animator;
    private bool playing = true;
    public float jumpForce = 3f; // Fuerza del salto
    private Rigidbody rb;

    void Start()
    {
        // Obtener el componente Animator y Rigidbody
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        StartCoroutine(PlayRandomAnimations());
    }

    void Update()
    {
        // Verificar si se ha hecho clic en la pantalla
        if (Input.GetMouseButtonDown(0))
        {
            playing = false;
        }

        // Verificar si la animación de caminar está activa
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walking02") || animator.GetCurrentAnimatorStateInfo(0).IsName("WiggingTail"))
        {
            Jump();
        }
    }

    IEnumerator PlayRandomAnimations()
    {
        while (playing)
        {
            // Escoger un nombre de animación al azar de la lista
            string randomAnimationName = animationNames[Random.Range(0, animationNames.Count)];
            
            // Reproducir la animación por su nombre
            animator.Play(randomAnimationName);

            // Esperar hasta que la animación termine o hasta que se detenga manualmente
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        }
    }

    void Jump()
    {
        // Verificar si el personaje está en el suelo para no saltar en el aire
        if (IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        // Verificar si el personaje está tocando el suelo
        return Physics.Raycast(transform.position, Vector3.down, 0.1f);
    }
}
