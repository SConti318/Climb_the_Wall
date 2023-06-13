using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ToolGrabR : MonoBehaviour
{

    InputDevice rightHand;

    [SerializeField] private HandManager handManager;

    [SerializeField] private GameObject rightCon;

    [SerializeField] private GameObject rightClimbSphere;

    private bool toolUsedR;
    // Start is called before the first frame update
    void Start()
    {

        rightHand = handManager.rightHand;
        toolUsedR = false;   
    }

    // Update is called once per frame
    void Update()
    {
        rightHand = handManager.rightHand;
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
                    Debug.Log("AlreadyHolding");
                    return;
                }
                //Debug.Log("grab tool");
                hit.transform.parent = rightCon.transform;
                if (hit.gameObject.name == "miniSledge") {
                    hit.transform.localPosition = new Vector3(-0.0252999999f, -0.00530002639f, 0.0676999539f);
                    hit.transform.localRotation = Quaternion.Euler(282.342224f, 0.0f, 90.0f);
                }
                if (hit.gameObject.name == "Compass")
                {
                    hit.transform.localPosition = new Vector3(-0.0478504188f, -0.0165986829f, 0.0522001274f);
                    hit.transform.localRotation = new Quaternion(0.579433799f, -0.57943368f, -0.405285656f, 0.405285805f);
                }
                if (hit.gameObject.name == "hookshot") {
                    hit.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                    hit.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                }

                hit.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                //toolUsed = true;
                rightClimbSphere.SetActive(false);
                toolUsedR = true;

            }
            else
            {
                if (hit.transform.parent == rightCon.transform)
                {
                    hit.transform.parent = null;
                    hit.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                }
                toolUsedR = false;
                
                rightClimbSphere.SetActive(true);

            }
        }
    
    }
}
