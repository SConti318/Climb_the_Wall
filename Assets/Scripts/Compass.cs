using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    [SerializeField] private List<GameObject> checkPoints;
    private GameObject currCheckpoint;
    // Start is called before the first frame update
    void Start()
    {
        currCheckpoint = checkPoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = currCheckpoint.transform.position;
        Vector3 relative = transform.parent.InverseTransformPoint(direction);
        
        float point = Mathf.Atan2(relative.y, -relative.x) * Mathf.Rad2Deg;

        transform.localRotation = Quaternion.Euler(0, 0, -point);
        //Debug.Log(rotation.ToString());
        Debug.Log(transform.eulerAngles.ToString());
    }
    public void SetCheckpoint(int num) {
        Debug.Log("Setting checkpoint "+ num);
        currCheckpoint = checkPoints[num];
    }
}
