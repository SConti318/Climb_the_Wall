using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{

    [SerializeField] private GameObject grabby;

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
        Debug.Log("hit");
        if (hit.gameObject.tag == "Breakable")
        {
            Vector3 oriPos = hit.gameObject.transform.position;
            Destroy(hit.gameObject);

            Instantiate(grabby, oriPos, Quaternion.Euler(90, 0, 90));
        } 
    }
}
