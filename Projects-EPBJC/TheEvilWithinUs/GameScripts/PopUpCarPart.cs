using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class PopUpCarPart : MonoBehaviour
{
    public Transform player;
    public Camera mainCamera;
    public Image popUp;
    public GameObject popUpCheck;
    public GameObject popUpCross;
    public PickUpCarPart pickUpCarPart;
    public GameObject GateClosed;
    public GameObject GateOpened;
    public UIManager uiManager;

    private GameObject wheel;
    private GameObject box;
    private static int totalCollected;
    private int totalCarParts = 7;
    private bool isCollected;
   

    // Start is called before the first frame update
    void Start()
    {
        GateClosed.SetActive(true);
        GateOpened.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (uiManager.IsRestarted == true)
        {
            totalCollected = 0;
        }
        RotateToPlayer();
        if (isCollected != true)
        {
            PopUpCarParts();
        }
    }

    private void PopUpCarParts()
    {
        if (pickUpCarPart.IsCollected == true)
        {
            popUpCheck.SetActive(true);
            popUpCross.SetActive(false);
            popUp.color = new Color(0f, 150f / 255f, 0f, 200f / 255f);
            isCollected = true;
            totalCollected++;
            
            EnableWheel();
            Opengate();

            if (totalCollected == totalCarParts)
            {
                AllCollected();
            }
        }
    }
    private void RotateToPlayer()
    {
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
    }
    private void AllCollected()
    {
        // Find all PopUpCarPart scripts in the scene
        PopUpCarPart[] carParts = FindObjectsOfType<PopUpCarPart>();

        // Deactivate all the canvases
        foreach (PopUpCarPart carPart in carParts)
        {
            foreach (Transform popUp in carPart.transform)
            {
                popUp.gameObject.SetActive(false);
            }
        }
    }
    private void EnableWheel()
    {
        CarPartWheel carPartWheel = transform.GetComponent<CarPartWheel>();
        if (carPartWheel != null)
        {
            wheel = carPartWheel.wheel;
            box = carPartWheel.box; 
            if (wheel != null)
            {
                wheel.SetActive(true);
                box.SetActive(false);
            }
        }
    }
    private void Opengate()
    {
        if(totalCollected == 6)
        {
            GateClosed.SetActive(false);
            GateOpened.SetActive(true);
        }
    }
    public int TotalCollected
    {
        get { return totalCollected; }
    }
}
