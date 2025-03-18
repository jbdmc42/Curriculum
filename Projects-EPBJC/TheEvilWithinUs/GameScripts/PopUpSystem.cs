using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TMPro;
using UnityEngine;

public class PopUpSystem : MonoBehaviour
{
    public Transform player;
    public Camera mainCamera;
    public GameObject popUp;
    public PopUpCarPart popUpCarPart;
    public TextMeshProUGUI popUpText;
    public TextMeshProUGUI objectText;
    public float popUpRange = 1.3f;
    public LayerMask pickUpMask;
    public LayerMask carMask;
    public bool playerWon = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PopUp();
    }

    private void PopUp()
    {
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, popUpRange, pickUpMask))
        {
            popUp.SetActive(true);

            WeaponObject weaponObject = hit.transform.GetComponent<WeaponObject>();
            ConsumableObject consumableObject = hit.transform.GetComponent<ConsumableObject>();
            if (weaponObject != null)
            {
                objectText.SetText(weaponObject.weapon.weaponName + "");
            }
            else if (consumableObject != null)
            {
                objectText.SetText(consumableObject.consumable.consumableName + "");
            }
            else
            {
                objectText.SetText(hit.transform.name + "");
            } 
        }
        else if (Physics.Raycast(ray, popUpRange, carMask) && popUpCarPart.TotalCollected == 7)
        {
            popUp.SetActive(true);

            popUpText.SetText("Press E To Enter");
            objectText.SetText("The Car");

            if (Input.GetKey(KeyCode.E))
            {
                playerWon = true;
            }
        }
        else
        {
            popUp.SetActive(false);
        }
    }
    public bool PlayerWon
    {
        get { return playerWon; }
    }
}
