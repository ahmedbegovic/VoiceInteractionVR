using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ActivatePuzzleTwo : MonoBehaviour
{
    // References to GameObjects and text objects that are located on the computer in the world
    public GameObject Button;
    private PuzzleTwo PuzzleTwo;
    private bool isPuzzleActivated = false;
    public TMP_Text mytext;
    void Start()
    {
        PuzzleTwo = Button.GetComponent<PuzzleTwo>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RightHand"))
        {
            if (!isPuzzleActivated)
            {   
                // Activates the puzzle when right controller presses the button
                PuzzleTwo.StartRotating();
                isPuzzleActivated = true;
                PuzzleTwo.PuzzleActivated = true;
                // Updates the button's position and color
                Vector3 position = transform.position;
                position.y -= 0.05f;
                transform.position = position;
                GetComponent<Renderer>().material.color = Color.green;
                // Text prompt for puzzle
                mytext.text = "What is Wrong?";
            }
        }
    }
}
