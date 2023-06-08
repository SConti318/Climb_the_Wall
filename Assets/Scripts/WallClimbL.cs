using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class WallClimbL : MonoBehaviour
{

    public GameObject grabbyL;
    InputDevice leftHand;
    [SerializeField] private GameObject cameraOff;
    [SerializeField] private GameObject leftCon;
    [SerializeField] private GameObject leftToolSphere;

    [SerializeField] private GameObject rightClimb;

    private Vector3 originalHand;
    private bool firstLeft;

    private bool isCurrent;


    // Start is called before the first frame update
    void Start()
    {
        var leftList = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(UnityEngine.XR.InputDeviceCharacteristics.Left, leftList);
        leftHand = leftList[0];

        firstLeft = false;
        isCurrent = false;

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

    }



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
                    isCurrent= true;
                    rightClimb.GetComponent<WallClimbR>().setCurrent(false);
                }

                

                if (isCurrent)
                {
                    Debug.Log("Grip button is pressed in hand hold");
                    //wall.transform.SetParent(grabbyL.transform);
                    cameraOff.transform.position += -1 * (leftCon.transform.position - originalHand);
                    Debug.Log("new " + leftCon.transform.position.ToString());
                    //Debug.Log(camera.transform.position.ToString());
                    //originalHand = transform.localPosition;
                    leftToolSphere.SetActive(false);
                    cameraOff.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
            else
            {
                Debug.Log("no hand hold");
                firstLeft = false;
                leftToolSphere.SetActive(true);
                isCurrent = false;

                if (rightClimb.GetComponent<WallClimbR>().isActive())
                {
                    rightClimb.GetComponent<WallClimbR>().setCurrent(true);
                }

                else if (rightClimb.GetComponent<WallClimbR>().shouldFall())
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
            
            Debug.Log("exit");
            firstLeft = false;
            isCurrent = false;
            leftToolSphere.SetActive(true);

            if (rightClimb.GetComponent<WallClimbR>().isActive())
            {
                Debug.Log("called active");
                rightClimb.GetComponent<WallClimbR>().setCurrent(true);
            }
            else if (rightClimb.GetComponent<WallClimbR>().shouldFall())
            {
                Debug.Log("falling");

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
        return firstLeft;
    }
}
