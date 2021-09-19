using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class AmbientLightEstimation : MonoBehaviour
{
    // We need this variable so that we can get each frame of the app, get the lightening values from that frame,
    // and set that to the light component in our scene.
    public ARCameraManager cameraManager;

    // We need this light variable to store the reference of the Light component attached to Directional Light object.
    private Light lightComponent;

    public GameObject warningText;

    // this function is used to subscribe to the frameReceived event and is called when the attached GameObject is toggled.
    private void OnEnable()
    {
        lightComponent = GetComponent<Light>();

        // The function, OnCameraFrameReceived is a listener method and
        // will be called for each frame that the camera receives through frameReceived event.
        // So this function will be called over and over again.
        cameraManager.frameReceived += OnCameraFrameReceived;
    }

    void OnCameraFrameReceived(ARCameraFrameEventArgs camFrameEvent)
    {
        // getting the light estimation data for each frame. We will get the brightness value and the color temperature value from this light estimation data.
        ARLightEstimationData led = camFrameEvent.lightEstimation;


        // It might be possible that for some frame our app is not able to get these brightness and color temperature values.
        // So to prevent our app from behaving unexpectedly, we use if condition to make sure we are assigning the value to our lightComponent only when
        // we have a non-null value from our light estimation data.
        if (led.averageBrightness.HasValue)
        {
            lightComponent.intensity = led.averageBrightness.Value;

            if(led.averageBrightness.Value<0.1f)
                warningText.SetActive(true);
            else
                warningText.SetActive(false);

        }
            
        if(led.averageColorTemperature.HasValue)
            lightComponent.colorTemperature = led.averageColorTemperature.Value;
    }

    // to unsubscribe from the event in case our light is disabled for some reason.
    private void OnDisable()
    {
        cameraManager.frameReceived -= OnCameraFrameReceived;
    }
}
