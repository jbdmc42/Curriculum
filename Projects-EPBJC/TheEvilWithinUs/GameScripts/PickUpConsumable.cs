using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

public class PickUpConsumable : MonoBehaviour
{
    public Transform player;
    public Transform mainCamera;
    public Animator animatorMedkitBox;
    public Animator animatorAmmoBox;
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;
    public Consumable consumable;
    public AudioManager audioManager;
    public PlayerHealth playerHealth;
    public float pickUpRange = 1.5f;
    public PopUpSystem popUpSystem;

    private Weapon weapon1;
    private Weapon weapon2;
    private Weapon weapon3;
    private bool isMedkitBoxOpen = false;
    private bool isAmmoBoxOpen = false;

    private bool isSlot1Empty => PickUpSystem.isSlot1Empty;
    private bool isSlot2Empty => PickUpSystem.isSlot2Empty;
    private bool isSlot3Empty => PickUpSystem.isSlot3Empty;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.IsDead == true || popUpSystem.PlayerWon == true)
        {
            return;
        }

        GetReferences();
        PickUpConsumables();
    }

    private void GetReferences()
    {
        if (isSlot1Empty == false)
        {
            weapon1 = slot1.GetComponentInChildren<WeaponObject>().weapon;
        }
        if (isSlot2Empty == false)
        {
            weapon2 = slot2.GetComponentInChildren<WeaponObject>().weapon;
        }
        if (isSlot3Empty == false)
        {
            weapon3 = slot3.GetComponentInChildren<WeaponObject>().weapon;
        }
    }

    private void PickUpConsumables()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        if (distanceToPlayer.magnitude <= pickUpRange && Input.GetKey(KeyCode.E))
        {
            if (slot1.activeSelf || slot2.activeSelf || slot3.activeSelf)
            {
                if (consumable.consumableType == ConsumableType.Ammo)
                {
                    if (slot1.activeSelf && weapon1.storedAmmo < weapon1.maxAmmo)
                    {
                        weapon1.storedAmmo += weapon1.magazineAmmo * 2;
                        if (weapon1.storedAmmo >= weapon1.maxAmmo)
                        {
                            weapon1.storedAmmo = weapon1.maxAmmo;
                        }
                        UseConsumable();
                        audioManager.Play("pickUp");
                    }
                    if (slot2.activeSelf && weapon2.storedAmmo < weapon2.maxAmmo)
                    {
                        weapon2.storedAmmo += weapon2.magazineAmmo * 2;
                        if (weapon2.storedAmmo >= weapon2.maxAmmo)
                        {
                            weapon2.storedAmmo = weapon2.maxAmmo;
                        }
                        UseConsumable();
                        audioManager.Play("pickUp");
                    }
                    if (slot3.activeSelf && weapon3.storedAmmo < weapon3.maxAmmo)
                    {
                        weapon3.storedAmmo += weapon3.magazineAmmo * 2;
                        if (weapon3.storedAmmo >= weapon3.maxAmmo)
                        {
                            weapon3.storedAmmo = weapon3.maxAmmo;
                        }
                        UseConsumable();
                        audioManager.Play("pickUp");
                    }
                }
            }
            if (consumable.consumableType == ConsumableType.AmmoBox)
            {
                if (slot1.activeSelf || slot2.activeSelf || slot3.activeSelf)
                {
                    if (isAmmoBoxOpen == true)
                    {
                        if (slot1.activeSelf && weapon1.storedAmmo < weapon1.maxAmmo)
                        {
                            weapon1.storedAmmo = weapon1.maxAmmo;
                            audioManager.Play("pickUp");
                        }
                        if (slot2.activeSelf && weapon2.storedAmmo < weapon2.maxAmmo)
                        {
                            weapon2.storedAmmo = weapon2.maxAmmo;
                            audioManager.Play("pickUp");
                        }
                        if (slot3.activeSelf && weapon3.storedAmmo < weapon3.maxAmmo)
                        {
                            weapon3.storedAmmo = weapon3.maxAmmo;
                            audioManager.Play("pickUp");
                        }
                    }
                }
                if (isAmmoBoxOpen == false)
                {
                    animatorAmmoBox.SetTrigger("Open");
                    audioManager.Play("boxOpen");
                    isAmmoBoxOpen = true;
                }
            }
            if (consumable.consumableType == ConsumableType.Medkit && playerHealth.currentHealth < playerHealth.maxHealth)
            {
                playerHealth.currentHealth += playerHealth.maxHealth / 2;
                if (playerHealth.currentHealth >= playerHealth.maxHealth)
                {
                    playerHealth.CurrentHealth(playerHealth.maxHealth);
                }
                else
                {
                    playerHealth.CurrentHealth(playerHealth.currentHealth);
                }
                UseConsumable();
                audioManager.Play("pickUp");
            }
            if (consumable.consumableType == ConsumableType.MedkitBox)
            {
                if (isMedkitBoxOpen == true && playerHealth.currentHealth < playerHealth.maxHealth)
                {
                    playerHealth.CurrentHealth(playerHealth.maxHealth);
                    audioManager.Play("pickUp");
                }
                else if (isMedkitBoxOpen == false)
                {
                    animatorMedkitBox.SetTrigger("Open");
                    audioManager.Play("boxOpen");
                    isMedkitBoxOpen = true;
                }
            }
        }
        else
        {
            //mensagem a dizer que a vida ou a munição esta cheia
        }
    }
    private void UseConsumable()
    {
        Destroy(gameObject);
    }
}
