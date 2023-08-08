using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using TMPro;
using Valve.VR;
public class VoiceMovement : MonoBehaviour
{
    // References for keyword recogniztion and the dictionary holding the keywords
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> keywords = new Dictionary<string, Action>();

    // References to GameObjects and variables for voice input
    public Transform player;
    public Transform cameraHead;
    public float teleportDistance = 5.0f;
    public TMP_Text textProgress;

    private ObjectHighlight objectHighlight;
    private int solvedPuzzles = 0;
    public GameObject firstPuzzleButton;
    public GameObject secondPuzzleButton;
    public GameObject computer;
    public PuzzleCounter puzzleCounter;
    public float lateralMovementDistance = 3.0f;
    public float rotationAngle = 30.0f;
    public Color fadeColor = Color.black;
    public float fadeDuration = 1.0f;

    private void Start()
    {
        // Initialize GameObjects and add the voice input keywords to the dictionary
        objectHighlight = GetComponent<ObjectHighlight>();
        puzzleCounter = textProgress.GetComponent<PuzzleCounter>();
        keywords.Add("move forward", MoveForward);
        keywords.Add("move backward", MoveBackward);
        keywords.Add("move left", MoveLeft);
        keywords.Add("move right", MoveRight);
        keywords.Add("rotate right", RotateRight);
        keywords.Add("rotate left", RotateLeft);
        keywords.Add("teleport to entrance", () => TeleportTo("Entrance"));
        keywords.Add("teleport to lab", () => TeleportTo("LabTeleport"));
        keywords.Add("teleport to server room", () => TeleportTo("ServerTeleport"));
        keywords.Add("grab", GrabHighlightedObject);
        keywords.Add("release", ReleaseHighlightedObject);
        keywords.Add("red blue green", SolveFirstPuzzle);
        keywords.Add("car is spinning", SolveSecondPuzzle);
        // Create a KeywordRecognizer and start it
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        // If keyword is recognized, invoke the action from the keyword
        Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }

    private void MoveForward()
    {
        // Moves the player forward using voice input
        Vector3 forwardDirection = cameraHead.transform.forward;
        forwardDirection.y = 0; // Remove vertical movement
        player.position += forwardDirection.normalized * teleportDistance;
    }

    private void MoveBackward()
    {
        // Moves the player backward using voice input
        Vector3 backDirection = -cameraHead.transform.forward;
        backDirection.y = 0; // Remove vertical movement
        player.position += backDirection.normalized * teleportDistance;
    }
    private void MoveLeft()
    {
        // Moves the player left using voice input
        Vector3 leftDirection = -cameraHead.transform.right;
        leftDirection.y = 0; // Remove vertical movement
        player.position += leftDirection.normalized * lateralMovementDistance;
    }

    private void MoveRight()
    {
        // Moves the player right using voice input
        Vector3 rightDirection = cameraHead.transform.right;
        rightDirection.y = 0; // Remove vertical movement
        player.position += rightDirection.normalized * lateralMovementDistance;
    }

    private void RotateRight()
    {
        // Rotates the player's camera to the right
        RotatePlayer(rotationAngle);
    }

    private void RotateLeft()
    {
        // Rotates the player's camera to the left
        RotatePlayer(-rotationAngle);
    }

    private void RotatePlayer(float angle)
    {
        // Rotate the player
        player.Rotate(0, angle, 0);
    }

    private void TeleportTo(string locationName)
    {
        // Teleports the player to specified locations, uses Fade to reduce sim sickness
        GameObject location = GameObject.Find(locationName);
        SteamVR_Fade.View(Color.black, 0);
        if (location)
        {
            player.position = location.transform.position;
        }
        SteamVR_Fade.View(Color.clear, 2);
    }

    private void GrabHighlightedObject()
    {
        // If the object is highlighted using right controller laser, use voice input to grab it
        if (objectHighlight.currentInteractable != null)
        {
            GameObject interactable = objectHighlight.currentInteractable;
            FixedJoint joint = interactable.AddComponent<FixedJoint>();
            joint.connectedBody = GetComponent<Rigidbody>();
            joint.breakForce = 20000;
            joint.breakTorque = 20000;

            // Set the object's position to the controller's position
            interactable.transform.position = transform.position;
        }
    }

    private void ReleaseHighlightedObject()
    {
        // Releases the object that is being is grabbed by the right controller using voice
        if (objectHighlight.currentInteractable != null)
        {
            GameObject interactable = objectHighlight.currentInteractable;
            FixedJoint joint = interactable.GetComponent<FixedJoint>();
            if (joint)
            {
                joint.connectedBody = null;
                Destroy(joint);
            }
        }
    }
    private void SolveFirstPuzzle()
    {
        // Solution to the first puzzle using voice
        // Plays the audio and increments the progress counter
        PuzzleOne puzzleOne = firstPuzzleButton.GetComponent<PuzzleOne>();
        if (puzzleOne.PuzzleActivated)
        {
            AudioSource computerAudioSource = computer.GetComponent<AudioSource>();
            if (!computerAudioSource.isPlaying)
            {
                computerAudioSource.Play();
            }
            puzzleCounter.IncrementSolvedPuzzles();
        }
    }

    private void SolveSecondPuzzle()
    {
        // Solution to the second puzzle using voice
        // Plays the audio and increments the progress counter
        PuzzleTwo puzzleTwo = secondPuzzleButton.GetComponent<PuzzleTwo>();
        if (puzzleTwo.PuzzleActivated)
        {
            AudioSource computerAudioSource = computer.GetComponent<AudioSource>();
            if (!computerAudioSource.isPlaying)
            {
                computerAudioSource.Play();
            }
            puzzleCounter.IncrementSolvedPuzzles();
        }
    }

    // Method to increment the text in progress TextMeshPro object
    private void UpdateTextProgress()
    {
        textProgress.text = $"{solvedPuzzles}/3";
    }

    // Clean up method for KeywordRecognizer
    private void OnDestroy()
    {
        if (keywordRecognizer != null)
        {
            keywordRecognizer.OnPhraseRecognized -= KeywordRecognizer_OnPhraseRecognized;
            keywordRecognizer.Dispose();
        }
    }
}
