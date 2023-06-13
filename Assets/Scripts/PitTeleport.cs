using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitTeleport : MonoBehaviour
{
    [SerializeField] private GameObject origin;
    [SerializeField] private GameObject teleport;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider hit)
    {

        origin.transform.position = teleport.transform.position;

    }
}
