using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ActivatePuzzleThree : MonoBehaviour
{
    // References to other GameObjects
    public GameObject Button;
    private PuzzleThree PuzzleThree;
    private bool isPuzzleActivated = false;
    public TMP_Text mytext;
    void Start()
    {
        PuzzleThree = Button.GetComponent<PuzzleThree>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RightHand"))
        {
            if (!isPuzzleActivated)
            {
                // Activates the puzzle
                PuzzleThree.puzzleActivated = true;
                isPuzzleActivated = true;
                // Update the button's position and material color
                Vector3 position = transform.position;
                position.y -= 0.05f;
                transform.position = position;
                GetComponent<Renderer>().material.color = Color.green;
                mytext.text = "Grab the Imposter";

            }
        }
    }
}
