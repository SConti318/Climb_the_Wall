using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class WallClimb : MonoBehaviour
{
    enum hand
    {
        Left,
        Right
    }
    

    InputDevice leftHand;
    InputDevice rightHand;
    public GameObject wall;
    public GameObject grabbyL;
    public GameObject grabbyR;
    private bool leftHold;
    private bool rightHold;

    [SerializeField] private GameObject cameraOff;

    [SerializeField] private GameObject leftCon;
    [SerializeField] private GameObject rightCon;

    private bool firstLeft;
    private bool firstRight;
    private hand curHand;

    private Vector3 originalHand;
    // Start is called before the first frame update
    void Start()
    {
        var leftList = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(UnityEngine.XR.InputDeviceCharacteristics.Left, leftList);
        leftHand = leftList[0];

        var rightList = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(UnityEngine.XR.InputDeviceCharacteristics.Right, rightList);
        rightHand = rightList[0];

        leftHold = false;
        rightHold = false;
        firstLeft = false;
        firstRight = false;
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
        //Debug.Log("hand " + transform.localPosition.ToString());
    }

    /*void FixedUpdate()
    {
        bool gripValue;
        if (leftHold)
        {
            if (leftHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out gripValue) && gripValue)
            {
                Debug.Log("Grip button is pressed in hand hold");
                //wall.transform.SetParent(grabbyL.transform);

                cameraOff.transform.position += -1 * (transform.localPosition - originalHand);
                Debug.Log((transform.localPosition - originalHand).ToString());
                //Debug.Log(camera.transform.position.ToString());
                //originalHand = transform.localPosition;
            }
        }
    }*/

    private void OnTriggerStay(Collider hit)
    {

        if (hit.gameObject.tag == "handHold")
        {
            Debug.Log("colliding with hand hold");
            bool gripValue;
            if (leftHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out gripValue) && gripValue)
            {

                if (!firstLeft)
                {
                    originalHand = leftCon.transform.position;
                    firstLeft = true;
                    curHand = hand.Left;
                }

                if (curHand == hand.Left) {
                    Debug.Log("Grip button is pressed in hand hold");
                    //wall.transform.SetParent(grabbyL.transform);
                    cameraOff.transform.position += -1 * (leftCon.transform.position - originalHand);
                    Debug.Log("new " + leftCon.transform.position.ToString());
                    //Debug.Log(camera.transform.position.ToString());
                    //originalHand = transform.localPosition;
                }
            }
            else
            {
                firstLeft = false;
            }
        
            if (rightHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out gripValue) && gripValue)
            {
                if (!firstRight) 
                {
                    originalHand = rightCon.transform.position;
                    firstRight = true;
                    curHand = hand.Right;
                }

                if (curHand == hand.Right)
                {
                    Debug.Log("Grip button is pressed in hand hold");
                    //wall.transform.SetParent(grabbyL.transform);
                    cameraOff.transform.position += -1 * (rightCon.transform.position - originalHand);
                    Debug.Log("new " + leftCon.transform.position.ToString());
                    //Debug.Log(camera.transform.position.ToString());
                    //originalHand = transform.localPosition;
                }
            }
            else
            {
                firstRight = false;
            }
            /*
            if (leftHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out gripValue) && gripValue)
            {
                Debug.Log("Grip button is pressed in hand hold");
                wall.transform.SetParent(grabbyL.transform);
                leftHold = true;
            }
            else
            {
                //Debug.Log("No parent");
                //wall.transform.SetParent(null);
            }
            */

            /*wall.transform.rotation = Quaternion.Euler(270.0f, 0.0f, 0.0f);
            if (!(leftHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out gripValue) && gripValue))
            {
                Debug.Log("Grip release");
                leftHold = false;
            }*/


            /*
            if (!rightHold)
            {
                if (rightHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out gripValue) && gripValue)
                {
                    Debug.Log("Grip button is pressed in hand hold");
                    wall.transform.SetParent(grabbyR.transform);
                    rightHold = true;
                }
                else
                {
                    Debug.Log("No parent");
                    wall.transform.SetParent(null);
                }
            }
            else {
                wall.transform.rotation = Quaternion.Euler(270.0f, 0.0f, 0.0f);
                if (!(rightHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out gripValue) && gripValue))
                {
                    Debug.Log("Grip release");
                    rightHold = false;
                }
            }
            */
        }

    }
}
