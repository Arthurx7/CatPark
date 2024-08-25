using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToObject : MonoBehaviour
{
    public float speed;
    private Transform playerTransform;
    private bool isFollowingPlayer = false;

    void Update()
    {
        if (isFollowingPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger.");
            playerTransform = other.transform.GetChild(1);
            isFollowingPlayer = true;
        }
    }
}
