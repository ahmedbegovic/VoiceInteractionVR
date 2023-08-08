using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleOne : MonoBehaviour
{
    // References to GameObject spheres that are located in the world
    public GameObject redSphere;
    public GameObject blueSphere;
    public GameObject greenSphere;

    // Reference to the original material color of the spheres

    private Color redSphereOriginalColor;
    private Color blueSphereOriginalColor;
    private Color greenSphereOriginalColor;
    public bool PuzzleActivated;
    void Start()
    {
        // Store the original colors of the spheres
        PuzzleActivated = false;
        redSphereOriginalColor = redSphere.GetComponent<Renderer>().material.color;
        blueSphereOriginalColor = blueSphere.GetComponent<Renderer>().material.color;
        greenSphereOriginalColor = greenSphere.GetComponent<Renderer>().material.color;
    }

    public void StartColorPuzzle()
    {
        StartCoroutine(ColorPuzzleSequence());
    }
    IEnumerator ColorPuzzleSequence()
    {
        // Simon-style color puzzle
        // Wait for 5 seconds before starting the color puzzle
        // Audio also plays when colors activate
        yield return new WaitForSeconds(5.0f);

        // Set all spheres to white
        redSphere.GetComponent<Renderer>().material.color = Color.white;
        blueSphere.GetComponent<Renderer>().material.color = Color.white;
        greenSphere.GetComponent<Renderer>().material.color = Color.white;

        // Wait for 2 seconds before changing the color of the red sphere to red
        yield return new WaitForSeconds(2.0f);
        redSphere.GetComponent<Renderer>().material.color = Color.red;
        redSphere.GetComponent<AudioSource>().Play();

        // Wait for 2 seconds before changing the color of the red sphere to white
        yield return new WaitForSeconds(2.0f);
        redSphere.GetComponent<Renderer>().material.color = Color.white;

        // Wait for 2 seconds before changing the color of the blue sphere to blue
        yield return new WaitForSeconds(2.0f);
        blueSphere.GetComponent<Renderer>().material.color = Color.blue;
        blueSphere.GetComponent<AudioSource>().Play();
        // Wait for 2 seconds before changing the color of the blue sphere to white
        yield return new WaitForSeconds(2.0f);
        blueSphere.GetComponent<Renderer>().material.color = Color.white;

        // Wait for 2 seconds before changing the color of the green sphere to green
        yield return new WaitForSeconds(2.0f);
        greenSphere.GetComponent<Renderer>().material.color = Color.green;
        greenSphere.GetComponent<AudioSource>().Play();
        // Wait for 2 seconds before changing the color of the green sphere to white
        yield return new WaitForSeconds(2.0f);
        greenSphere.GetComponent<Renderer>().material.color = Color.white;

        // Wait for 2 seconds before resetting all spheres to their original colors
        yield return new WaitForSeconds(2.0f);
        redSphere.GetComponent<Renderer>().material.color = redSphereOriginalColor;
        blueSphere.GetComponent<Renderer>().material.color = blueSphereOriginalColor;
        greenSphere.GetComponent<Renderer>().material.color = greenSphereOriginalColor;
    }
}
