using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShot : MonoBehaviour
{
    [SerializeField] private HandManager handManager;
    [SerializeField] private float cooldownTime;

    [SerializeField] private GameObject leftTool;
    [SerializeField] private GameObject rightTool;

    [SerializeField] private GameObject cylinder;

    private Transform parent;

    private float currCooldown;

    private GameObject currCylinder;

    private LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        currCooldown = 0;
        parent = transform.parent;
        currCylinder = null;
        line = gameObject.AddComponent<LineRenderer>();
        line.enabled = false;
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        Gradient gradient = new Gradient();
        line.startColor = Color.blue;
        line.endColor = Color.blue;
    }

    // Update is called once per frame
    void Update()
    {

        

        parent = transform.parent;
        currCooldown -= Time.deltaTime;
        if (currCooldown > 0)
        {
            return;
        }
        Debug.DrawRay(transform.position, transform.forward, Color.green);
        if (parent == leftTool.transform)
        {
            line.enabled = true;
            line.SetPosition(0, this.transform.position);
            line.SetPosition(1, this.transform.position + (this.transform.forward * 10));



            bool trigValue;
            if (handManager.leftHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out trigValue) && trigValue)
            {
                RaycastHit hit;
                Ray ray = new Ray(this.transform.position + this.transform.forward, this.transform.forward);
                Debug.DrawRay(transform.position, transform.forward, Color.green);
                if (Physics.Raycast(ray, out hit, 10f))
                {
                    if (hit.transform.tag == "target")
                    {
                        if (currCylinder != null)
                        {
                            Destroy(currCylinder);
                        }
                        makeRope(this.transform.position, hit.transform.position, 0.1f);
                        currCooldown = cooldownTime;
                    }
                    
                }
            }
        }
        else if (parent == rightTool.transform)
        {
            line.enabled = true;
            line.SetPosition(0, this.transform.position);
            line.SetPosition(1, this.transform.position + (this.transform.forward * 10));
            bool trigValue;

            Debug.Log("Grabbed");
            if (handManager.rightHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out trigValue) && trigValue)
            {
                RaycastHit hit;
                Ray ray = new Ray(this.transform.position + this.transform.forward, this.transform.forward);
                //Debug.DrawRay(transform.position, transform.forward, Color.green);
                Debug.Log("trigger");
                if (Physics.Raycast(ray, out hit, 10f))
                {
                    Debug.Log(hit.transform.name);
                    Debug.Log(hit.transform.tag);
                    if (hit.transform.tag == "target")
                    {

                        if (currCylinder != null)
                        {
                            Destroy(currCylinder);
                        }

                        makeRope(this.transform.position, hit.transform.position, 0.1f);
                        currCooldown = cooldownTime;
                    }

                }
            }
        }
        else
        {
            line.enabled = false;
        }
    }

    private void makeRope(Vector3 start, Vector3 end, float width)
    {
        Vector3 offset = end - start;
        Vector3 scale = new Vector3(width, offset.magnitude / 2.0f, width);
        Vector3 position = start + (offset / 2.0f);

        GameObject c = Instantiate(cylinder, position, Quaternion.identity);
        c.transform.up = offset;
        c.transform.localScale = scale;
        currCylinder = c;
    }

}
