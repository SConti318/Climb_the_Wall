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
                hit.transform.localPosition = new Vector3(0, 0, 0);
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
