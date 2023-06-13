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
                if (hit.gameObject.name == "miniSledge")
                {
                    hit.transform.localPosition = new Vector3(0.0269000009f, -0.00529999984f, 0.0676999986f);
                    hit.transform.localRotation = Quaternion.Euler(282.342224f, 0.0f, 90.0f);
                }
                if (hit.gameObject.name == "Compass")
                {
                    hit.transform.localPosition = new Vector3(0.0392000005f, -0.0165986829f, 0.053199999f);
                    hit.transform.localRotation = new Quaternion(0.489304751f, -0.489304543f, 0.510471344f, -0.510471404f);
                }
                if (hit.gameObject.name == "hookshot")
                {
                    hit.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                    hit.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                }
                leftClimbSphere.SetActive(false);
                toolUsedL = true;
                //iggerLeft = true;

            }
            else
            {
                if (hit.transform.parent == leftCon.transform)
                {
                    hit.transform.parent = null;
                    hit.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                }


                toolUsedL = false;
                leftClimbSphere.SetActive(true);

            }

        }

    }
}
