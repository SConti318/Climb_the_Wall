using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ToolGrabL : MonoBehaviour
{
    InputDevice leftHand;

    [SerializeField] private GameObject leftCon;


    [SerializeField] private GameObject leftClimbSphere;

    private bool toolUsedL;

    // Start is called before the first frame update
    void Start()
    {
        var leftList = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(UnityEngine.XR.InputDeviceCharacteristics.Left, leftList);
        leftHand = leftList[0];



        toolUsedL = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!leftHand.isValid)
        {
            var leftList = new List<UnityEngine.XR.InputDevice>();
            UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(UnityEngine.XR.InputDeviceCharacteristics.Left, leftList);
            leftHand = leftList[0];
            //Debug.Log("Left set to valid");

        }


    }

    private void OnTriggerStay(Collider hit)
    {
        if (hit.gameObject.tag == "tool")
        {

            bool gripValue;
            if (leftHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out gripValue) && gripValue)
            {
                if (toolUsedL)
                {
                    return;
                }
                Debug.Log("grab tool left");
                hit.transform.parent = leftCon.transform;
                hit.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                //toolUsed = true;
                leftClimbSphere.SetActive(false);
                toolUsedL = true;
                //iggerLeft = true;

            }
            else
            {
                if (hit.transform.parent == leftCon.transform)
                {
                    hit.transform.parent = null;
                }


                toolUsedL = false;
                hit.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                leftClimbSphere.SetActive(true);

            }

        }

    }
}
