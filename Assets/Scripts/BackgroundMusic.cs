using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic instance;

    void Awake()
    {
        // Verificar si ya existe una instancia de este objeto
        if (instance == null)
        {
            // Si no existe, asignarlo a esta instancia y no destruirlo al cargar una nueva escena
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Si ya existe una instancia, destruir este objeto duplicado
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Asegurarse de que el audio se está reproduciendo
        AudioSource audioSource = GetComponent<AudioSource>();
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}