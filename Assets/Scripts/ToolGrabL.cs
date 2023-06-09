using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ToolGrabL : MonoBehaviour
{
    InputDevice leftHand;

    [SerializeField] private HandManager handManager;

    [SerializeField] private GameObject leftCon;


    [SerializeField] private GameObject leftClimbSphere;

    private bool toolUsedL;

    // Start is called before the first frame update
    void Start()
    {
        toolUsedL = false;
        leftHand = handManager.leftHand;
    }

    // Update is called once per frame
    void Update()
    {
        leftHand = handManager.leftHand;
    }

    private void OnTriggerStay(Collider hit)
    {
        if (hit.gameObject.tag == "tool")
        {

            bool gripValue;
            if (leftHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out gripValue) && gripValue)
            {
                if (toolUsedL)
                {
                    return;
                }
                Debug.Log("grab tool left");
                hit.transform.parent = leftCon.transform;
                //toolUsed = true;
                leftClimbSphere.SetActive(false);
                toolUsedL = true;
                //iggerLeft = true;

            }
            else
            {
                if (hit.transform.parent == leftCon.transform)
                {
                    hit.transform.parent = null;
                }


                toolUsedL = false;
                leftClimbSphere.SetActive(true);

            }

        }

    }
}
