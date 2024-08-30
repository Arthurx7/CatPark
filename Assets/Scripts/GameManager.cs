using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
   
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Player"))
        {
        
            RestartScene();
        }
    }

  
    private void RestartScene()
    {

        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        }
    }
}
