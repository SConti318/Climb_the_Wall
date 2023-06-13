using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class GemInteractabel : MonoBehaviour
{
    private bool found;

    private InputDevice leftHand;
    private InputDevice rightHand;

    [SerializeField] private GameObject rightContoller;
    [SerializeField] private GameObject leftContoller;

    private bool firstGrab;
    private bool isHeldR;
    private bool isHeldL;
    private Vector3 scale;


    [SerializeField] private HandManager handManager;

    [SerializeField] private GameObject menu;

    // Start is called before the first frame update
    void Start()
    {
        leftHand = handManager.leftHand;
        rightHand = handManager.rightHand;
        firstGrab = true;
        isHeldL = false;
        isHeldR = false;
    }

    // Update is called once per frame
    void Update()
    {
        leftHand = handManager.leftHand;
        rightHand = handManager.rightHand;
    }
    void OnTriggerStay(Collider hit)
    {
        
        if (hit.gameObject.tag == "handL")
        {
            bool grabValue1;
            if (leftHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out grabValue1) && grabValue1)
            {
                if (firstGrab) {
                    
                    firstGrab = false;
                   if (!menu.activeSelf)
                    {
                        menu.SetActive(true);
                        this.transform.SetParent(menu.transform, true);
                        menu.SetActive(false);
                    }
                    else
                    {
                        this.transform.SetParent(menu.transform, true);
                    }
                    scale = this.transform.localScale;
                }

                if (!isHeldL)
                {
                    this.transform.SetParent(leftContoller.transform, true);
                    isHeldL = true;
                    isHeldR = false;
                }
            }
            else
            {
                if (isHeldL)
                {
                    isHeldL = false;
                    if (!menu.activeSelf)
                    {
                        menu.SetActive(true);
                        this.transform.SetParent(menu.transform, true);
                        this.transform.localScale = scale;
                        this.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                        this.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                        menu.SetActive(false);
                    }
                    else
                    {
                        this.transform.SetParent(menu.transform, true);
                        this.transform.localScale = scale;
                        this.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                        this.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                    }
                }
            }
        }
        if (hit.gameObject.tag == "handR")
        {
            bool grabValue2;
            if (rightHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out grabValue2) && grabValue2)
            {
                if (firstGrab)
                {
                    firstGrab = false;
                   
                    if (!menu.activeSelf)
                    {
                        menu.SetActive(true);
                        this.transform.SetParent(menu.transform, true);
                        menu.SetActive(false);
                    }
                    else
                    {
                        this.transform.SetParent(menu.transform, true);
                    }
                    scale = this.transform.localScale;
                }

                if (!isHeldR)
                {
                    this.transform.SetParent(rightContoller.transform, true);
                    isHeldR = true;
                    isHeldL = false;
                }
            }
            else {
                if (isHeldR)
                {
                    
                    isHeldR = false;
                    if (!menu.activeSelf)
                    {
                        menu.SetActive(true);
                        this.transform.SetParent(menu.transform, true);
                        this.transform.localScale = scale;
                        this.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                        this.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                        menu.SetActive(false);
                    }
                    else
                    {
                        this.transform.SetParent(menu.transform, true);
                        this.transform.localScale = scale;
                        this.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                        this.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                    }
                }
            }
        }

    }
    public void PutInSlot() {
        this.transform.GetComponent<GemInteractabel>().enabled = false;
    }
}
