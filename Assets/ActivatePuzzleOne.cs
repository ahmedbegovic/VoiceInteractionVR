using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePuzzleOne : MonoBehaviour
{
    // References to other GameObjects and boolean variable for the puzzle activation
    public GameObject colorPuzzleObject;
    private PuzzleOne PuzzleOne;
    private bool isPuzzleActivated = false;

    void Start()
    {
        PuzzleOne = colorPuzzleObject.GetComponent<PuzzleOne>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RightHand"))
        {
            if (!isPuzzleActivated)
            {
                // Starts the Puzzle in PuzzleOne script 
                PuzzleOne.StartColorPuzzle();
                isPuzzleActivated = true;
                PuzzleOne.PuzzleActivated = true;
                // Adjusts the position and material color of the button that is pressed by the right controller.
                Vector3 position = transform.position;
                position.y -= 0.05f;
                transform.position = position;
                GetComponent<Renderer>().material.color = Color.green;
            }
        }
    }
}
