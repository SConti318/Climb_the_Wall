using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class WallClimbR : MonoBehaviour
{

    InputDevice rightHand;
    public GameObject grabbyR;

    [SerializeField] private GameObject cameraOff;
    [SerializeField] private GameObject rightCon;
    [SerializeField] private GameObject rightToolSphere;

    [SerializeField] private GameObject leftClimb;

    private bool firstRight;

    private Vector3 originalHand;

    private bool isCurrent;

    // Start is called before the first frame update
    void Start()
    {
        var rightList = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(UnityEngine.XR.InputDeviceCharacteristics.Right, rightList);
        rightHand = rightList[0];

        firstRight = false;
        isCurrent = false;
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

        if (hit.gameObject.tag == "handHold")
        {

            bool gripValue;
            if (rightHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out gripValue) && gripValue)
            {
                if (!firstRight)
                {
                    originalHand = rightCon.transform.position;
                    firstRight = true;
                    isCurrent = true;

                    leftClimb.GetComponent<WallClimbL>().setCurrent(false);
                }

                if (isCurrent)
                {
                    Debug.Log("Grip button is pressed in hand hold");
                    //wall.transform.SetParent(grabbyL.transform);
                    cameraOff.transform.position += -1 * (rightCon.transform.position - originalHand);
                    //Debug.Log("new " + leftCon.transform.position.ToString());
                    //Debug.Log(camera.transform.position.ToString());
                    //originalHand = transform.localPosition;
                    rightToolSphere.SetActive(false);
                    cameraOff.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
            else
            {
                firstRight = false;
                rightToolSphere.SetActive(true);
                isCurrent = false;

                if (leftClimb.GetComponent<WallClimbL>().isActive())
                {
                    leftClimb.GetComponent<WallClimbL>().setCurrent(true);
                }
                else if (leftClimb.GetComponent<WallClimbL>().shouldFall())
                {
                    cameraOff.GetComponent<Rigidbody>().isKinematic = false;
                }
            }
        }
    }

    public void OnTriggerExit(Collider hit)
    {

        if (hit.gameObject.tag == "handHold")
        {
            firstRight = false;
            isCurrent = false;
            rightToolSphere.SetActive(true);

            if (leftClimb.GetComponent<WallClimbL>().isActive())
            {
                leftClimb.GetComponent<WallClimbL>().setCurrent(true);
            }
            else if (leftClimb.GetComponent<WallClimbL>().shouldFall())
            {
                cameraOff.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
        
    }

    public void setCurrent(bool cur)
    {
        isCurrent = cur;
    }

    public bool shouldFall()
    {
        return !(isCurrent);
    }

    public bool isActive()
    {
        return firstRight;
    }
}
