using UnityEngine;
using System.Collections;
using TMPro;
public class DoorOpen : MonoBehaviour
{
    // Reference to other GameObjects
    public TMP_Text textProgress;
    private PuzzleCounter puzzleCounter;
    public GameObject roomDoorA;
    public GameObject roomDoorB;
    private bool doorsOpened = false;

    private void Start()
    {
        puzzleCounter = textProgress.GetComponent<PuzzleCounter>();
    }
    private void Update()
    {
        // Open the lab doors when puzzles are completed
        if (!doorsOpened && puzzleCounter.solvedPuzzles == puzzleCounter.totalPuzzles)
        {
            doorsOpened = true;
            OpenDoors();
        }
    }

    private void OpenDoors()
    {
        // Hides the doors and plays the door opening sound
        GameObject doorLeft = roomDoorA.transform.Find("Door").gameObject;
        GameObject doorRight = roomDoorB.transform.Find("Door").gameObject;

        roomDoorA.GetComponent<AudioSource>().Play();
        roomDoorB.GetComponent<AudioSource>().Play();

        doorLeft.SetActive(false);
        doorRight.SetActive(false);
    }
}
