using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using System;
using UnityEngine.EventSystems;

public class ARFocusCircle : MonoBehaviour
{
    public GameObject virtual_object;
    public GameObject button;

    public GameObject placementIndicator;

    private ARSessionOrigin arOrigin;
    private Pose placementPose;
    private bool placementPoseIsValid = false;

    private bool placementIndicatorEnabled = true;

    bool isUIHidden = false;

    void Start()
    {
        arOrigin = FindObjectOfType<ARSessionOrigin>();
    }

    void Update()
    {

        if (placementIndicatorEnabled == true)
        {
            UpdatePlacementPose();
            UpdatePlacementIndicator();
        }

        /*if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlaceObject();
        }*/
    }

    public void HideUI() {

        // this runs the first time since isUIHidden = false initially
        if (isUIHidden == false)
        {
            button.SetActive(false);
            
            placementIndicatorEnabled = false;
            placementIndicator.SetActive(false);

            isUIHidden = true;
        }

        else if (isUIHidden == true) {

            button.SetActive(true);
            
            placementIndicatorEnabled = true;
            placementIndicator.SetActive(true);

            isUIHidden = false;

        }
    }

    public void PlaceObject() {

        virtual_object.SetActive(true);
        virtual_object.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        //virtual_object.transform.position = placementPose.position;
        //virtual_object.transform.rotation = placementPose.rotation;
    }

    

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);

            button.SetActive(true);    

            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);

            button.SetActive(false);
            
        }
    }

    private void UpdatePlacementPose()
    {
        // current is used to refer the camera we are currently rendering with
        // not used main since it refers the first enabled camera tagged "MainCamera"
        // A viewport space point is relative to the camera. The bottom-left of the camera is (0,0); the top-right is (1,1). Therefore the center taken is (0.5f, 0.5f)
        // A screen space point is defined in pixels. The bottom-left of the screen is (0,0); the right-top is (pixelWidth,pixelHeight).
        // ViewportToScreenPoint is done because the Raycast method below takes a 2D pixel position on the screen.
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();

        // ray casting allows to determine where a ray intersects with a trackable.
        // screenCenter is the point from which to cast.
        // raycast results, if successful, are added to the list, hits
        // The third parameter specifies the type(s) of trackable(s) to hit test against
        arOrigin.GetComponent<ARRaycastManager>().Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneEstimated);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            // Pose is representation of a Position, and a Rotation in 3D Space. Used primarily in XR applications.
            placementPose = hits[0].pose;


            // If vertical plane (ex- wall) is tracked, line 113 assigns its pose to the placementIndicator, making it vertical.
            // To avoid this, rotation of placementIndicator should be changed which is done using these 3 lines.
            var cameraForward = Camera.current.transform.forward;
            // making y component 0 for proper orientation 
            // comment it to see the effect without it.
            cameraForward.y = 0;
            //var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraForward);
            
        }
    }
}
