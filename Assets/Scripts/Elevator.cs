using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform elevator; // Reference to the elevator object
    public float elevatorSpeed = 2f; // Speed at which the elevator moves
    private int totalPlayers = 0; // Number of players currently on the elevator
    private int playersOnElevator = 0; // Counter for players on the elevator

    public float targetHeight = 10f; // The height at which the elevator stops
    private bool isMovingUp = false; // Flag to check if the elevator is moving up
    private bool isMovingDown = false; // Flag to check if the elevator is moving down

    private Vector3 originalPosition; // Store the original position of the elevator

    public GameObject numberCube; // Reference to the cube inside the elevator
    public List<Material> numberMaterials; // List of materials/textures for the numbers

    void Start()
    {
        // Count total players tagged as "Player" in the scene
        totalPlayers = GameObject.FindGameObjectsWithTag("Player").Length;

        // Store the original position of the elevator
        originalPosition = elevator.position;

        // Set the initial texture of the cube based on the number of players
        UpdateCubeTexture(playersOnElevator);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playersOnElevator++;
            UpdateCubeTexture(playersOnElevator);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playersOnElevator--;
            UpdateCubeTexture(playersOnElevator);
        }
    }

    void Update()
    {
        // Start moving up if all players are on the elevator
        if (playersOnElevator == totalPlayers)
        {
            isMovingUp = true;
            isMovingDown = false; // Stop moving down if it's going up
        }
        else if (playersOnElevator < totalPlayers)
        {
            // Start moving down immediately if any player exits
            isMovingUp = false;
            isMovingDown = true;
        }

        if (isMovingUp)
        {
            MoveElevatorUp();
        }

        if (isMovingDown)
        {
            MoveElevatorDown();
        }
    }

    void MoveElevatorUp()
    {
        // Continue moving up until it reaches the target height
        if (elevator.position.y < targetHeight)
        {
            elevator.transform.Translate(Vector3.up * elevatorSpeed * Time.deltaTime);
        }
    }

    void MoveElevatorDown()
    {
        // Move the elevator back to the original position
        elevator.position = Vector3.MoveTowards(elevator.position, originalPosition, elevatorSpeed * Time.deltaTime);

        // If the elevator has returned to the original position, stop moving it down
        if (elevator.position == originalPosition)
        {
            isMovingDown = false; // Stop the downward movement
        }
    }

    void UpdateCubeTexture(int playerCount)
    {
        // Ensure playerCount is within the bounds of the materials list
        if (playerCount >= 0 && playerCount < numberMaterials.Count)
        {
            // Get the Renderer component of the cube and set the material
            Renderer cubeRenderer = numberCube.GetComponent<Renderer>();
            cubeRenderer.material = numberMaterials[playerCount];
        }
    }
}
