using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController3D : MonoBehaviour
{
    public Material carBodyMaterial;
    public Material projectorMaterial;

    public GameObject color3DUI;

    // initially 3d UI is disabled
    bool UIStatus = false;

    public void RedColor()
    {
        carBodyMaterial.color = new Color(0.4716981f, 0, 0, 1);
        projectorMaterial.color = new Color(0.4716981f, 0, 0, 1);
    }
    
    public void BlueColor()
    {
        carBodyMaterial.color = new Color(0.1775543f, 0.4132704f, 0.6603774f, 1);
        projectorMaterial.color = new Color(0.1775543f, 0.4132704f, 0.6603774f, 1);
    }
    
    public void WhiteColor()
    {
        carBodyMaterial.color = new Color(0.6981132f, 0.6553044f, 0.6553044f, 1);
        projectorMaterial.color = new Color(0.6981132f, 0.6553044f, 0.6553044f, 1);
    }

    public void Toggle3DUI()
    {
        if(UIStatus==false)
        {
            color3DUI.SetActive(true);
            UIStatus = true;
        }
        else
        {
            color3DUI.SetActive(false);
            UIStatus = false;
        }
    }
}
