using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private bool isFalling = false;
    private float downSpeed = 0;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player")
        {
            StartCoroutine(StartFallingAfterDelay(1)); 
        }
    }

    private IEnumerator StartFallingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 
        isFalling = true;
        Destroy(gameObject, 10);
    }

    void Update()
    {
        if (isFalling)
        {
            downSpeed += Time.deltaTime/20;
            transform.position = new Vector3(transform.position.x, transform.position.y - downSpeed, transform.position.z);
        }
    }
}
