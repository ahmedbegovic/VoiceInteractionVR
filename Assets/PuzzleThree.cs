using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuzzleThree : MonoBehaviour
{
    // Reference to GameObjects
    public GameObject Computer;
    public bool puzzleActivated;
    public TMP_Text mytext;
    public TMP_Text progress;
    public GameObject LeftHand;
    private ControllerGrab Controller;
    public PuzzleCounter puzzleCounter;
    // Start is called before the first frame update
    void Start()
    {
        puzzleActivated = false;
        Controller = LeftHand.GetComponent<ControllerGrab>();
        puzzleCounter = progress.GetComponent<PuzzleCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        // This puzzle requires the user to find an object that does not belong in the room, it's the radio
        // Code checks if the user has grabbed the radio with their left hand and triggers the completion
        if (puzzleActivated && (Controller.IsGrabbedObjectWithTag("puzzle")))
        {
            mytext.text = "Imposter Found.";
            if (!Computer.GetComponent<AudioSource>().isPlaying)
            {
                Computer.GetComponent<AudioSource>().Play();
            }
            puzzleCounter.IncrementSolvedPuzzles();
        }
    }
}
