using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInteractable : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "handL")
        {
            Debug.Log("Button press");
            this.GetComponent<Button>().onClick.Invoke();
        }
        if (hit.gameObject.tag == "handR")
        {
            this.GetComponent<Button>().onClick.Invoke();
        }

    }
}
