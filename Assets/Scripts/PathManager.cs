using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    [SerializeField] private List<GemHole> triggers;
    private bool lift;
    // Start is called before the first frame update
    void Start()
    {
        lift = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!lift) {
            foreach (GemHole trigger in triggers) {
                lift = trigger.isActivated();
                if (!lift) {
                    break;
                }
            }
        }
        if (this.transform.localPosition.y < -5.0f) {
            this.transform.localPosition += new Vector3(0.0f,0.1f,0.0f);
        }
    }
}
