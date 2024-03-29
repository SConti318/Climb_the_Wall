using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class WallClimbL : MonoBehaviour
{

    public GameObject grabbyL;
    InputDevice leftHand;

    [SerializeField] private HandManager handManager;

    [SerializeField] private MenuLogic menu;

    [SerializeField] private GameObject cameraOff;
    [SerializeField] private GameObject leftCon;
    [SerializeField] private GameObject leftToolSphere;

    [SerializeField] private GameObject rightClimb;

    private Vector3 originalHand;
    private bool firstLeft;

    private bool isCurrent;

    public bool wallCollide;
    public bool floorCollide;

    // Start is called before the first frame update
    void Start()
    {
        leftHand = handManager.leftHand;

        firstLeft = false;
        isCurrent = false;
        wallCollide = false;
        floorCollide = false;
    }

    // Update is called once per frame
    void Update()
    {
        leftHand = handManager.leftHand;

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



                    Vector3 movement = -1 * (leftCon.transform.position - originalHand);
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
        bool triggerValue;
        if (hit.gameObject.tag == "UIButton")
        {
            if (leftHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
            {
                hit.gameObject.GetComponent<Button>().onClick.Invoke();
            }
        }
        /*
        if (hit.gameObject.tag == "Backpack")
        {
            Debug.Log("Backpack Hit");
            if (leftHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
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
