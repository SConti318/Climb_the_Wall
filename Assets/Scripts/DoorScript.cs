using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField]  private List<WheelRotate> wheelScripts;
    private bool wheel1;
    private bool wheel2;
    private bool wheel3;
    private bool wheel4;
    private bool wheel5;
    private float doorLift;

    // Start is called before the first frame update
    void Start()
    {
        wheel1 = false;
        wheel2 = false;
        wheel3 = false;
        wheel4 = false;
        wheel5 = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (wheelScripts[0].currNum == 8)
        {
            wheel1 = true;
        }
        else {
            wheel1 = false;
        }
        if (wheelScripts[1].currNum == 5)
        {
            wheel2 = true;
        }
        else
        {
            wheel2 = false;
        }
        if (wheelScripts[2].currNum == 7)
        {
            wheel3 = true;
        }
        else
        {
            wheel3 = false;
        }
        if (wheelScripts[3].currNum == 9)
        {
            wheel4 = true;
        }
        else
        {
            wheel4 = false;
        }
        if (wheelScripts[4].currNum == 2)
        {
            wheel5 = true;
        }
        else
        {
            wheel5 = false;
        }
        if (wheel1 && wheel2 && wheel3 && wheel4 && wheel5) {
            if (doorLift < 2.5f) {
                this.transform.localPosition += new Vector3( 0.0f, doorLift, 0.0f);
                doorLift += 0.00005f;
            }
        }
    }
}
