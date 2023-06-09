using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class MenuInteractable : MonoBehaviour
{
    private bool found;

    private InputDevice leftHand;
    private InputDevice rightHand;

    [SerializeField] private HandManager handManager;

    [SerializeField] private MenuLogic menu;

    // Start is called before the first frame update
    void Start()
    {
        leftHand = handManager.leftHand;
        rightHand = handManager.rightHand;
    }

    // Update is called once per frame
    void Update()
    {
        leftHand = handManager.leftHand;
        rightHand = handManager.rightHand;
    }
    void OnTriggerStay(Collider hit)
    {
        bool triggerValue;
        if (hit.gameObject.tag == "handL") {
            if (leftHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
            {
                this.GetComponent<Button>().onClick.Invoke();
            }
        }
        if (hit.gameObject.tag == "handR")
        {
            if (rightHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
            {
                this.GetComponent<Button>().onClick.Invoke();
            }
        }

    }
}
