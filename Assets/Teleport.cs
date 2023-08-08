using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class Teleport : MonoBehaviour
{
    // References to SteamVR and GameObjects
    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean teleportAction;
    public GameObject laserPrefab;
    private GameObject laser; 
    private Transform laserTransform; 
    private Vector3 hitPoint;
    public Transform cameraRigTransform;
    public GameObject teleportReticlePrefab;
    private GameObject reticle;
    private Transform teleportReticleTransform;
    public Transform headTransform;
    public Vector3 teleportReticleOffset;
    private bool shouldTeleport;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate the laser and the reticle
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;
        reticle = Instantiate(teleportReticlePrefab);
        teleportReticleTransform = reticle.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Activates when teleport button is pressed
        if (teleportAction.GetState(handType))
        {
            RaycastHit hit;

            if (Physics.Raycast(controllerPose.transform.position, transform.forward, out hit, 100))
            {
                // Show the laser and start the teleport
                hitPoint = hit.point;
                ShowLaser(hit);
                reticle.SetActive(true);
                teleportReticleTransform.position = hitPoint + teleportReticleOffset;
                shouldTeleport = true;
            }
        }
        else
        {
            // Turn off the laser and reticle when completed
            laser.SetActive(false);
            reticle.SetActive(false);
        }
        if (teleportAction.GetStateUp(handType) && shouldTeleport)
        {
            SteamVR_Fade.View(Color.black, 0);
            TeleportPlayer();
            SteamVR_Fade.View(Color.clear, 1);
        }
    }
    private void ShowLaser(RaycastHit hit)
    {
        // Smooth motion of laser
        laser.SetActive(true);
        laserTransform.position = Vector3.Lerp(controllerPose.transform.position, hitPoint, .5f);
        laserTransform.LookAt(hitPoint);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x,
                                                laserTransform.localScale.y,
                                                hit.distance);
    }
    private void TeleportPlayer()
    {
        // Teleport the person
        shouldTeleport = false;
        reticle.SetActive(false);
        // Adjust the position difference between cameraRig and player's head position
        Vector3 difference = cameraRigTransform.position - headTransform.position;
        difference.y = 0;
        cameraRigTransform.position = hitPoint + difference;
    }
}
