using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSystem : MonoBehaviour
{
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;
    public GameObject knifeHolder;
    public Animator animatorArm;
    public AudioManager audioManager;
    public PlayerHealth playerHealth;
    public PopUpSystem popUpSystem;

    private Weapon knife;
    private Weapon weapon1;
    private Weapon weapon2;
    private Weapon weapon3;
    private int currentWeaponId = 0;
    private int currentSlot;
    private string weaponClip;
    private bool isSlot1Empty => PickUpSystem.isSlot1Empty;
    private bool isSlot2Empty => PickUpSystem.isSlot2Empty;
    private bool isSlot3Empty => PickUpSystem.isSlot3Empty;
    private bool isKnifeHolderEmpty => PickUpSystem.isKnifeHolderEmpty;

    // Start is called before the first frame update
    void Start()
    {
        slot1.SetActive(false);
        slot2.SetActive(false);
        slot3.SetActive(false);
        knifeHolder.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.IsDead == true || popUpSystem.PlayerWon == true)
        {
            return;
        }

        GetReferences();
        WeaponSwitch();
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
        if (isKnifeHolderEmpty == false)
        {
            knife = knifeHolder.GetComponentInChildren<WeaponObject>().weapon;
        }
    }

    private void WeaponSwitch()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            if (isSlot1Empty == false)
            {
                currentSlot = 1;
                if (weapon1.weaponId != currentWeaponId || animatorArm.GetInteger("weaponType") == 0)
                {
                    audioManager.Play("takeOut");

                    if (weapon1.weaponType == WeaponType.M4A1)
                    {
                        currentWeaponId = weapon1.weaponId;
                        animatorArm.SetTrigger("unEquip");
                        animatorArm.SetInteger("weaponType", 3);
                    }
                    else if (weapon1.weaponType == WeaponType.AK47)
                    {
                        currentWeaponId = weapon1.weaponId;
                        animatorArm.SetTrigger("unEquip");
                        animatorArm.SetInteger("weaponType", 3);
                    }
                    else if (weapon1.weaponType == WeaponType.Shotgun)
                    {
                        currentWeaponId = weapon1.weaponId;
                        animatorArm.SetTrigger("unEquip");
                        animatorArm.SetInteger("weaponType", 3);
                    }
                    else if (weapon1.weaponType == WeaponType.Sniper)
                    {
                        currentWeaponId = weapon1.weaponId;
                        animatorArm.SetTrigger("unEquip");
                        animatorArm.SetInteger("weaponType", 3);
                    }
                    else if (weapon1.weaponType == WeaponType.Pistol)
                    {
                        currentWeaponId = weapon1.weaponId;
                        animatorArm.SetTrigger("unEquip");
                        animatorArm.SetInteger("weaponType", 2);
                    }
                }
            }
            else if (isSlot1Empty == true && animatorArm.GetInteger("weaponType") != 0)
            {
                currentWeaponId = 0;
                animatorArm.SetTrigger("unEquip");
                animatorArm.SetInteger("weaponType", 0);
                audioManager.Play("takeOut");
            }
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            if (isSlot2Empty == false)
            {
                currentSlot = 2;
                if (weapon2.weaponId != currentWeaponId || animatorArm.GetInteger("weaponType") == 0)
                {
                    audioManager.Play("takeOut");

                    if (weapon2.weaponType == WeaponType.M4A1)
                    {
                        currentWeaponId = weapon2.weaponId;
                        animatorArm.SetTrigger("unEquip");
                        animatorArm.SetInteger("weaponType", 3);
                    }
                    else if (weapon2.weaponType == WeaponType.AK47)
                    {
                        currentWeaponId = weapon2.weaponId;
                        animatorArm.SetTrigger("unEquip");
                        animatorArm.SetInteger("weaponType", 3);
                    }
                    else if (weapon2.weaponType == WeaponType.Shotgun)
                    {
                        currentWeaponId = weapon2.weaponId;
                        animatorArm.SetTrigger("unEquip");
                        animatorArm.SetInteger("weaponType", 3);
                    }
                    else if (weapon2.weaponType == WeaponType.Sniper)
                    {
                        currentWeaponId = weapon2.weaponId;
                        animatorArm.SetTrigger("unEquip");
                        animatorArm.SetInteger("weaponType", 3);
                    }
                    else if (weapon2.weaponType == WeaponType.Pistol)
                    {
                        currentWeaponId = weapon2.weaponId;
                        animatorArm.SetTrigger("unEquip");
                        animatorArm.SetInteger("weaponType", 2);
                    }
                }
            }
            else if (isSlot2Empty == true && animatorArm.GetInteger("weaponType") != 0)
            {
                currentWeaponId = 0;
                animatorArm.SetTrigger("unEquip");
                animatorArm.SetInteger("weaponType", 0);
                audioManager.Play("takeOut");
            }
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            if (isSlot3Empty == false)
            {
                currentSlot = 3;
                if (weapon3.weaponId != currentWeaponId || animatorArm.GetInteger("weaponType") == 0)
                {
                    audioManager.Play("takeOut");

                    if (weapon3.weaponType == WeaponType.M4A1)
                    {
                        currentWeaponId = weapon3.weaponId;
                        animatorArm.SetTrigger("unEquip");
                        animatorArm.SetInteger("weaponType", 3);
                    }
                    else if (weapon3.weaponType == WeaponType.AK47)
                    {
                        currentWeaponId = weapon3.weaponId;
                        animatorArm.SetTrigger("unEquip");
                        animatorArm.SetInteger("weaponType", 3);
                    }
                    else if (weapon3.weaponType == WeaponType.Shotgun)
                    {
                        currentWeaponId = weapon3.weaponId;
                        animatorArm.SetTrigger("unEquip");
                        animatorArm.SetInteger("weaponType", 3);
                    }
                    else if (weapon3.weaponType == WeaponType.Sniper)
                    {
                        currentWeaponId = weapon3.weaponId;
                        animatorArm.SetTrigger("unEquip");
                        animatorArm.SetInteger("weaponType", 3);
                    }
                    else if (weapon3.weaponType == WeaponType.Pistol)
                    {
                        currentWeaponId = weapon3.weaponId;
                        animatorArm.SetTrigger("unEquip");
                        animatorArm.SetInteger("weaponType", 2);
                    }
                }
            }
            else if (isSlot3Empty == true && animatorArm.GetInteger("weaponType") != 0)
            {
                currentWeaponId = 0;
                animatorArm.SetTrigger("unEquip");
                animatorArm.SetInteger("weaponType", 0);
                audioManager.Play("takeOut");
            }
        }
        if (Input.GetKey(KeyCode.C))
        {
            if (isKnifeHolderEmpty == false)
            {
                currentSlot = 4;
                if (knife.weaponId != currentWeaponId || animatorArm.GetInteger("weaponType") == 0)
                {
                    currentWeaponId = knife.weaponId;
                    animatorArm.SetTrigger("unEquip");
                    animatorArm.SetInteger("weaponType", 1);
                    audioManager.Play("takeOut");
                }
            }
            else if (isKnifeHolderEmpty == true && animatorArm.GetInteger("weaponType") != 0)
            {
                currentWeaponId = 0;
                animatorArm.SetTrigger("unEquip");
                animatorArm.SetInteger("weaponType", 0);
                audioManager.Play("takeOut");
            }
        }
    }
    public void EventFunction()
    {
        if (currentSlot == 1 && animatorArm.GetInteger("weaponType") != 0)
        {
            slot1.SetActive(true);
            slot2.SetActive(false);
            slot3.SetActive(false);
            knifeHolder.SetActive(false);

            weaponClip = weapon1.weaponClip;
            audioManager.Play(weaponClip);
        }
        if (currentSlot == 2 && animatorArm.GetInteger("weaponType") != 0)
        {
            slot1.SetActive(false);
            slot2.SetActive(true);
            slot3.SetActive(false);
            knifeHolder.SetActive(false);

            weaponClip = weapon2.weaponClip;
            audioManager.Play(weaponClip);
        }
        if (currentSlot == 3 && animatorArm.GetInteger("weaponType") != 0)
        {
            slot1.SetActive(false);
            slot2.SetActive(false);
            slot3.SetActive(true);
            knifeHolder.SetActive(false);

            weaponClip = weapon3.weaponClip;
            audioManager.Play(weaponClip);
        }
        if (currentSlot == 4 && animatorArm.GetInteger("weaponType") != 0)
        {
            slot1.SetActive(false);
            slot2.SetActive(false);
            slot3.SetActive(false);
            knifeHolder.SetActive(true);
            audioManager.Play("KnifeClip");
        }
        if (animatorArm.GetInteger("weaponType") == 0)
        {
            slot1.SetActive(false);
            slot2.SetActive(false);
            slot3.SetActive(false);
            knifeHolder.SetActive(false);
        }
    }
}
