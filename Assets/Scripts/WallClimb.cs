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
    [SerializeField] private GameObject leftToolSphere;
    [SerializeField] private GameObject rightToolSphere;

    private bool firstLeft;
    private bool firstRight;
    private hand curHand;
    private bool toolUsed;

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
        toolUsed = false;
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


    private void OnTriggerStay(Collider hit)
    {

            bool gripValue;
            if (leftHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out gripValue) && gripValue)
            {
                if (hit.gameObject.tag == "handHold")
                {

                    if (!firstLeft)
                    {
                    originalHand = leftCon.transform.position;
                    firstLeft = true;
                    curHand = hand.Left;
                    }

                    if (curHand == hand.Left) 
                    {
                    Debug.Log("Grip button is pressed in hand hold");
                    //wall.transform.SetParent(grabbyL.transform);
                    cameraOff.transform.position += -1 * (leftCon.transform.position - originalHand);
                    Debug.Log("new " + leftCon.transform.position.ToString());
                    //Debug.Log(camera.transform.position.ToString());
                    //originalHand = transform.localPosition;
                    leftToolSphere.SetActive(false);
                    }

                }
            }
            else
            {
                firstLeft = false;
                leftToolSphere.SetActive(true);
            }
        
            if (rightHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out gripValue) && gripValue)
            {
                if (hit.gameObject.tag == "handHold")
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
                        rightToolSphere.SetActive(false);
                    }
                }
            }
            else
            {
                firstRight = false;
                rightToolSphere.SetActive(true);
            }
        


    }
}
