using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumPadLogic : MonoBehaviour
{
    public int currWheel;

    private WheelRotate wheel;

    [SerializeField] private GameObject highlight;

    [SerializeField] private List<WheelRotate> wheels;

    private List<float> highLightPosition = new List<float>()
    {
        -0.4f,
        -0.2f,
        0.0f,
        0.2f,
        0.4f
    };

    // Start is called before the first frame update
    void Start()
    {
        currWheel = 0;
        wheel = wheels[currWheel];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Num1() 
    {
        wheel.setNum(1);
    }
    public void Num2()
    {
        wheel.setNum(2);
    }
    public void Num3()
    {
        wheel.setNum(3);
    }
    public void Num4()
    {
        wheel.setNum(4);
    }
    public void Num5()
    {
        wheel.setNum(5);
    }
    public void Num6()
    {
        wheel.setNum(6);
    }
    public void Num7()
    {
        wheel.setNum(7);
    }
    public void Num8()
    {
        wheel.setNum(8);
    }
    public void Num9()
    {
        wheel.setNum(9);
    }
    public void Num0()
    {
        wheel.setNum(0);
    }

    public void Left()
    {
        Debug.Log("currWheel: " + currWheel);
        currWheel = (currWheel - 1)%5;
        wheel = wheels[currWheel];
        highlight.transform.localPosition = new Vector3(highLightPosition[currWheel], highlight.transform.localPosition.y, highlight.transform.localPosition.z);
    }
    public void Right()
    {
        Debug.Log("currWheel: " + currWheel);
        currWheel = (currWheel + 1)%5;
        wheel = wheels[currWheel];
        highlight.transform.localPosition = new Vector3 (highLightPosition[currWheel], highlight.transform.localPosition.y, highlight.transform.localPosition.z);
    }
}
