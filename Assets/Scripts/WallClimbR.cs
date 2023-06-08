using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class WallClimbR : MonoBehaviour
{

    InputDevice rightHand;
    public GameObject grabbyR;

    [SerializeField] private MenuLogic menu;

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
                    cameraOff.GetComponent<Rigidbody>().isKinematic = true;
                    //Debug.Log("new " + leftCon.transform.position.ToString());
                    //Debug.Log(camera.transform.position.ToString());
                    //originalHand = transform.localPosition;
                    rightToolSphere.SetActive(false);
                }
            }
            else
            {
                firstRight = false;
                rightToolSphere.SetActive(true);
                isCurrent = false;
                cameraOff.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
        bool triggerValueR;
        if (hit.gameObject.tag == "UIButton")
        {
            if (rightHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValueR) && triggerValueR)
            {
                hit.gameObject.GetComponent<Button>().onClick.Invoke();
            }
        }
        /*
        if (hit.gameObject.tag == "Backpack")
        {
            Debug.Log("Backpack Hit");
            if (rightHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValueR) && triggerValueR)
            {
                menu.ToggleMenu();
            }
        }*/
    }
    private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Backpack")
        {
            menu.ToggleMenu();
        }
    }

    public void setCurrent(bool cur)
    {
        isCurrent = cur;
    }   
}
