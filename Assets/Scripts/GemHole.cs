using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemHole : MonoBehaviour
{
    [SerializeField] private string gemTag;
    [SerializeField] private List<GameObject> numberBlocks;
    [SerializeField] private GameObject dummy;
    [SerializeField] private GameObject gem;
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
        if (numBlockMove && numberBlocks[0].transform.localPosition.z > -0.5f) {
            foreach(GameObject numberBlock in numberBlocks) {
                numberBlock.transform.localPosition -= new Vector3(0, 0, 0.01f);
            }
        }
    }
    void OnTriggerEnter(Collider hit) {
        Debug.Log("Hit: "+ hit.gameObject.tag + " My Tag: "+gemTag);
        if (hit.gameObject.tag == gemTag) {
            hit.gameObject.SetActive(false);
            Instantiate(gem, dummy.transform.position, Quaternion.Euler(-90, 0, 0));
            numBlockMove = true;
        }
    }
    public bool isActivated() {
        return numBlockMove;
    }
}
