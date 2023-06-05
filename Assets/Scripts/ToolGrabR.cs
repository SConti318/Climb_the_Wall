using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ToolGrabR : MonoBehaviour
{

    InputDevice rightHand;

    [SerializeField] private GameObject rightCon;

    [SerializeField] private GameObject rightClimbSphere;

    private bool toolUsedR;
    // Start is called before the first frame update
    void Start()
    {

        var rightList = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(UnityEngine.XR.InputDeviceCharacteristics.Right, rightList);
        rightHand = rightList[0];

        toolUsedR = false;   
    }

    // Update is called once per frame
    void Update()
    {

        if (!rightHand.isValid)
        {
            var rightList = new List<UnityEngine.XR.InputDevice>();
            UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(UnityEngine.XR.InputDeviceCharacteristics.Right, rightList);
            rightHand = rightList[0];
        }
    }

    private void OnTriggerStay(Collider hit)
    {
        if (hit.gameObject.tag == "tool")
        {
            
            bool gripValue;

            if (rightHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out gripValue) && gripValue)
            {
                if (toolUsedR)
                {
                    return;
                }
                //Debug.Log("grab tool");
                hit.transform.parent = rightCon.transform;
                //toolUsed = true;
                rightClimbSphere.SetActive(false);
                toolUsedR = true;

            }
            else
            {
                if (hit.transform.parent == rightCon.transform)
                {
                    hit.transform.parent = null;
                }
                toolUsedR = false;
                rightClimbSphere.SetActive(true);

            }
        }
    
    }
}
