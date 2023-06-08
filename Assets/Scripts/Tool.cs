using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    private Vector3 oriPos;
    private Vector3 oriScale;
    private Quaternion oriRot;
    private Transform oriParent;
    [SerializeField] private float timeTilReset;
    private float timeLeft;
    
        // Start is called before the first frame update
    void Start()
    {
        oriPos = transform.localPosition;
        oriRot = transform.rotation;
        oriParent = transform.parent;
        oriScale = transform.localScale;
        timeLeft = timeTilReset;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<Rigidbody>().isKinematic == false)
        {
            timeLeft -= Time.deltaTime;
        }

        if (timeLeft < 0)
        {
            this.GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = oriParent;
            transform.rotation = oriRot;
            transform.localPosition = oriPos;
            transform.localScale = oriScale;
            timeLeft = timeTilReset;
        }
    }
}
