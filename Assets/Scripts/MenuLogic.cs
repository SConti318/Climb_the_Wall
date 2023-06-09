using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class MenuLogic : MonoBehaviour
{
    InputDevice leftHand;
    InputDevice rightHand;
    private bool MenuVis;
    [SerializeField] private GameObject menu;
    [SerializeField] private Compass compass;
    [SerializeField] private HandManager handManager;

    [SerializeField] private List<GameObject> menuItems;

    private Image selected;
    private int currCheckpoint;
    // Start is called before the first frame update
    void Start()
    {
        leftHand = handManager.leftHand;
        rightHand = handManager.rightHand;

        MenuVis = false;
        //menu.enabled = false;
        menu.SetActive(false);
        selected = menuItems[0].GetComponent<Image>();
        currCheckpoint = 0;
    }

    // Update is called once per frame
    void Update()
    {

        leftHand = handManager.leftHand;
        rightHand = handManager.rightHand;
    }

    public void ToggleMenu() {
        Debug.Log("Menu Toggled");
        MenuVis = !MenuVis;
        menu.SetActive(MenuVis);
    }

    public void CP1Select() 
    {
        currCheckpoint = 0;
        selected.color = new Color32(255, 255, 225, 100);
        menuItems[0].GetComponent<Image>().color = new Color32(10, 241, 245, 100);
        selected = menuItems[0].GetComponent<Image>();
    }
    public void CP2Select() 
    {
        currCheckpoint = 1;
        selected.color = new Color32(255, 255, 225, 100);
        menuItems[1].GetComponent<Image>().color = new Color32(10, 241, 245, 100);
        selected = menuItems[1].GetComponent<Image>();
    }
    public void CP3Select()
    {
        currCheckpoint = 2;
        selected.color = new Color32(255, 255, 225, 100);
        menuItems[2].GetComponent<Image>().color = new Color32(10, 241, 245, 100);
        selected = menuItems[2].GetComponent<Image>();
    }
    public void CP4Select()
    {
        currCheckpoint = 3;
        selected.color = new Color32(255, 255, 225, 100);
        menuItems[3].GetComponent<Image>().color = new Color32(10, 241, 245, 100);
        selected = menuItems[3].GetComponent<Image>();
    }
    public void CP5Select()
    {
        currCheckpoint = 4;
        selected.color = new Color32(255, 255, 225, 100);
        menuItems[4].GetComponent<Image>().color = new Color32(10, 241, 245, 100);
        selected = menuItems[4].GetComponent<Image>();
    }
    public void CP6Select()
    {
        currCheckpoint = 5;
        selected.color = new Color32(255, 255, 225, 100);
        menuItems[5].GetComponent<Image>().color = new Color32(10, 241, 245, 100);
        selected = menuItems[5].GetComponent<Image>();
    }
    public void onTrackPress() {
        compass.SetCheckpoint(currCheckpoint);
    }
}
