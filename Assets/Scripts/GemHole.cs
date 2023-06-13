using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemHole : MonoBehaviour
{
    [SerializeField] private string gemTag;
    [SerializeField] private GameObject numberBlock;
    private bool numBlockMove;
    private float numBlockPos;

    // Start is called before the first frame update
    void Start()
    {
        numBlockMove = false;
        numBlockPos = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (numBlockMove && numberBlock.transform.localPosition.z > -0.5f) {
            numBlockPos = 0.001f;
            numberBlock.transform.localPosition -= new Vector3(0, 0, numBlockPos);
        }
    }
    void OnTriggerEnter(Collider hit) {
        Debug.Log("Hit: "+ hit.gameObject.tag + " My Tag: "+gemTag);
        if (hit.gameObject.tag == gemTag) {
            hit.gameObject.transform.SetParent(this.transform, true);
            hit.gameObject.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            //hit.gameObject.transform.localRotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
            hit.gameObject.transform.GetComponent<GemInteractabel>().PutInSlot();
            numBlockMove = true;
        }
    }
}
