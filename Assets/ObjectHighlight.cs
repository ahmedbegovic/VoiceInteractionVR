using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ObjectHighlight : MonoBehaviour
{
    // References to GameObjects and public variables for SteamVR and prefabs
    public GameObject laserPrefab;
    public string interactableTag = "Interactable";
    private string puzzleTag = "puzzle";
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean laserToggleAction;

    private GameObject laser;
    // Dictionary to keep track of object materials
    private Dictionary<GameObject, (Material, Color)> originalMaterials = new Dictionary<GameObject, (Material, Color)>();
    public GameObject currentInteractable;
    private bool laserActive;

    void Start()
    {
        laser = Instantiate(laserPrefab);
        laser.SetActive(false);
    }
    void Update()
    {
        if (laserToggleAction.GetStateDown(handType))
        {
            // Instantiate the laser prefab and set it to inactive
            laserActive = !laserActive;
            laser.SetActive(laserActive);
        }
        // Check if the laser is active

        if (laserActive)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                // Update the laser's position, rotation, and scale to point towards the hit point
                laser.transform.position = Vector3.Lerp(transform.position, hit.point, 0.5f);
                laser.transform.LookAt(hit.point);
                laser.transform.localScale = new Vector3(laser.transform.localScale.x, laser.transform.localScale.y, hit.distance);

                // Check if the hit object has the interactable or puzzle tag
                if (hit.collider.CompareTag(interactableTag) || hit.collider.CompareTag(puzzleTag))
                {
                    GameObject interactable = hit.collider.gameObject;
                    if (interactable != currentInteractable)
                    {
                        if (currentInteractable != null)
                        {
                            // If a different interactable is highlighted, restore the original materials of the previous interactable and set the current interactable
                            currentInteractable.GetComponent<Renderer>().material = originalMaterials[currentInteractable].Item1;
                            currentInteractable.GetComponent<Renderer>().material.color = originalMaterials[currentInteractable].Item2;
                        }
                        currentInteractable = interactable;
                        if (!originalMaterials.ContainsKey(currentInteractable))
                        {
                            // If the current interactable doesn't have original materials stored yet, store them
                            originalMaterials[currentInteractable] = (currentInteractable.GetComponent<Renderer>().material, currentInteractable.GetComponent<Renderer>().material.color);
                        }
                        // Highlight the current interactable by setting its material color to match the laser color
                        currentInteractable.GetComponent<Renderer>().material.color = laser.GetComponent<Renderer>().material.color;
                    }
                }
                else
                {
                    // If the hit object doesn't have the interactable or puzzle tag, restore the original materials of the currently highlighted interactable and set the current interactable to null
                    if (currentInteractable != null)
                    {
                        currentInteractable.GetComponent<Renderer>().material = originalMaterials[currentInteractable].Item1;
                        currentInteractable.GetComponent<Renderer>().material.color = originalMaterials[currentInteractable].Item2;
                        currentInteractable = null;
                    }
                }
            }
            else
            {
                // If no object is hit by the raycast, restore the original materials of the currently highlighted interactable and set the current interactable to null
                if (currentInteractable != null)
                    if (currentInteractable != null)
                {
                    currentInteractable.GetComponent<Renderer>().material = originalMaterials[currentInteractable].Item1;
                    currentInteractable.GetComponent<Renderer>().material.color = originalMaterials[currentInteractable].Item2;
                    currentInteractable = null;
                }
                // Restore the laser
                laser.transform.position = transform.position + transform.forward * 50f;
                laser.transform.LookAt(transform.position + transform.forward * 100f);
                laser.transform.localScale = new Vector3(laser.transform.localScale.x, laser.transform.localScale.y, 100f);
            }
        }
        else
        {
            if (currentInteractable != null)
            {
                // If the laser is not active, restore the original materials of the currently highlighted interactable and set the current interactable to null
                currentInteractable.GetComponent<Renderer>().material = originalMaterials[currentInteractable].Item1;
                currentInteractable.GetComponent<Renderer>().material.color = originalMaterials[currentInteractable].Item2;
                currentInteractable = null;
            }
        }
    }
}
