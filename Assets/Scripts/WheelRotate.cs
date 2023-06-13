using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotate : MonoBehaviour
{
    private List<int> wheelRotations = new List<int>()
    {
        0,
        -36,
        -72,
        -108,
        -144,
        -180,
        -216,
        -252,
        -288,
        -324
    };

    private int currValue;
    private int passedValue;

    public int currNum;

    // Start is called before the first frame update
    void Start()
    {
        currValue = 0;
        passedValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currValue != passedValue) {
            currValue = (currValue - 1)%(-360);
            transform.localRotation = Quaternion.Euler(currValue, 0, 90);
        }
    }

    public void setNum(int passed) {
        passedValue = wheelRotations[passed];
        currNum = passed;
    }
}
