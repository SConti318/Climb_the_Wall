using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollider : MonoBehaviour
{
    [SerializeField] private WallClimbL leftHand;
    [SerializeField] private WallClimbR rightHand;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Wall")
        {
            Debug.Log("hit wall");
            leftHand.wallCollide = true;
            rightHand.wallCollide = true;
        }

        if (hit.tag == "Floor")
        {
            leftHand.floorCollide = true;
            rightHand.floorCollide = true;
        }
    }

    public void OnTriggerExit(Collider hit)
    {
        if (hit.tag == "Wall")
        {
            leftHand.wallCollide = false;
            rightHand.wallCollide = false;
        }

        if (hit.tag == "Floor")
        {
            leftHand.floorCollide = false;
            rightHand.floorCollide = false;
        }

    }
}
