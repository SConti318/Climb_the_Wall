using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class RayCast : MonoBehaviour
{
    public LineRenderer lineR;

    InputDevice leftHand;
    InputDevice rightHand;

    // Start is called before the first frame update
    void Start()
    {
        lineR.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!leftHand.isValid)
        {
            var leftList = new List<UnityEngine.XR.InputDevice>();
            UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(UnityEngine.XR.InputDeviceCharacteristics.Left, leftList);
            leftHand = leftList[0];
            Debug.Log("Left set to valid");

        }

        if (!rightHand.isValid)
        {
            var rightList = new List<UnityEngine.XR.InputDevice>();
            UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(UnityEngine.XR.InputDeviceCharacteristics.Right, rightList);
            rightHand = rightList[0];
        }
        else
        {
            Ray ray = new Ray(this.transform.position, this.transform.forward);
            RaycastHit hit;
            //Debug.Log("At angle" + Camera.main.transform.localEulerAngles.x);

            lineR.SetPosition(0, this.transform.position);
            lineR.SetPosition(1, this.transform.position + (this.transform.forward * 5));
            lineR.enabled = true;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                bool triggerValueL;
                bool triggerValueR;
                if ((rightHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValueR) && triggerValueR) || leftHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValueL) && triggerValueL)
                {
                    if (hit.transform.tag == "UIButton")
                    {
                        hit.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                    }
                }
            }

        }
    }
}
