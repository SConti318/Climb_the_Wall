using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class WallClimbR : MonoBehaviour
{

    InputDevice rightHand;
    public GameObject grabbyR;

    [SerializeField] private HandManager handManager;

    [SerializeField] private MenuLogic menu;

    [SerializeField] private GameObject cameraOff;
    [SerializeField] private GameObject rightCon;
    [SerializeField] private GameObject rightToolSphere;

    [SerializeField] private GameObject leftClimb;

    private bool firstRight;

    private Vector3 originalHand;

    private bool isCurrent;


    public bool wallCollide;
    public bool floorCollide;

    // Start is called before the first frame update
    void Start()
    {
        rightHand = handManager.rightHand;

        firstRight = false;
        isCurrent = false;
        wallCollide = false;
        floorCollide = false;
    }

    // Update is called once per frame
    void Update()
    {
        rightHand = handManager.rightHand;
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


                    Vector3 movement = -1 * (rightCon.transform.position - originalHand);
                    if (wallCollide)
                    {
                        //if ((originalHand.z - newPos.z) > 0)
                        //{
                        //Debug.Log("Colliding with wall");
                        movement.z = 0;
                        //}
                    }
                    if (floorCollide)
                    {
                        //if ((originalHand.y - newPos.y) < 0)
                        //{
                        movement.y = 0;
                        //}
                    }

                    cameraOff.transform.position += movement;

                    cameraOff.GetComponent<Rigidbody>().isKinematic = true;
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
