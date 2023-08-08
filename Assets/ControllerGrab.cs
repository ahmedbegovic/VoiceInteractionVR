using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class ControllerGrab : MonoBehaviour
{
    // References to SteamVR and GameObjects
    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean grabAction;
    private GameObject collidingObject;
    private GameObject objectInHand; 

    // Sets the object if the hand has collided with the object
    private void SetCollidingObject(Collider obj)
    {
        if (collidingObject || !obj.GetComponent<Rigidbody>())
        {
            return;
        }

        collidingObject = obj.gameObject;

    }

    // Fetch the colliding object (PuzzleThree)
    public GameObject GetCollidingObject()
    {
        return collidingObject;
    }

    // Collider events
    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }
    // Update is called once per frame
    void Update()
    {
        // Grab the object using SteamVR action by pressing down on the button
        if (grabAction.GetLastStateDown(handType))
        {
            if (collidingObject)
            {
                GrabObject();
            }
        }

        // Release the object from the hand if the button is lifted
        if (grabAction.GetLastStateUp(handType))
        {
            if (objectInHand)
            {
                ReleaseObject();
            }
        }
    }

    // Grab the object and attach it to the joint
    private void GrabObject()
    {
        objectInHand = collidingObject;
        collidingObject = null;
        
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    // Create a FixedJoint that will attach the collding object
    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    // Releases the object and gives the object velocity from the controller
    private void ReleaseObject()
    {
        if (GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            objectInHand.GetComponent<Rigidbody>().velocity = controllerPose.GetVelocity();
            objectInHand.GetComponent<Rigidbody>().angularVelocity = controllerPose.GetAngularVelocity();
        }
        objectInHand = null;
    }
    // Method for PuzzleThree, checks if the object has the tag "puzzle"
    public bool IsGrabbedObjectWithTag(string tag)
    {
        return objectInHand != null && objectInHand.CompareTag(tag);
    }
}
