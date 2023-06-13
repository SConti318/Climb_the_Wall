using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAnchor : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float dist = -1.5f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.localPosition.z < dist) {
            player.transform.localPosition += new Vector3(0.0f, 0.0f, 0.01f);
        }
    }
}
