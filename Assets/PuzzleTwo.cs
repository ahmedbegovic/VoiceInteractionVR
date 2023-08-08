using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTwo : MonoBehaviour
{
    // References to GameObjects
    public GameObject Car;
    public float rotationSpeed = 10f;
    private bool isRotating = false;
    public bool PuzzleActivated;

    private void Start()
    {
        PuzzleActivated = false;
    }
    // Update is called once per frame
    void Update()
    {
        // Rotates the buggy that is located in the world, solution is a voice input in VoiceMovement.cs
        if (isRotating)
        {
            Car.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
    public void StartRotating()
    {
        isRotating = true;
    }

}
