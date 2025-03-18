using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    public Rigidbody rb;
    public BoxCollider boxCollider;
    public Transform player;
    public Transform slot1;
    public Transform slot2;
    public Transform slot3;
    public Transform knifeHolder;
    public Transform mainCamera;
    public Animator animatorArm;
    public Weapon weapon;
    public AudioManager audioManager;
    public PlayerHealth playerHealth;
    public PopUpSystem popUpSystem;
    public UIManager uiManager;

    public float pickUpRange = 1.3f;
    public float dropForwardForce = 3;
    public float dropUpwardForce = 1;

    public bool isEquiped = false;
    public static bool isSlot1Empty = true;
    public static bool isSlot2Empty = true;
    public static bool isSlot3Empty = true;
    public static bool isKnifeHolderEmpty = true;

    // Start is called before the first frame update
    void Start()
    {
        WeaponAmmo();
    }

    // Update is called once per frame
    void Update()
    {
        if (uiManager.IsRestarted == true)
        {
            isSlot1Empty = true;
            isSlot2Empty = true;
            isSlot3Empty = true;
            isKnifeHolderEmpty = true;
        }


        if (playerHealth.IsDead == true || popUpSystem.PlayerWon == true)
        {
            return;
        }

        PickUp();
        Drop();
    }
    private void PickUp()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        if (isEquiped == false && distanceToPlayer.magnitude <= pickUpRange && Input.GetKey(KeyCode.E))
        {
            if ((isSlot1Empty == true || isSlot2Empty == true || isSlot3Empty == true) && weapon.weaponType != WeaponType.Knife)
            {
                if (isSlot1Empty == true)
                {
                    audioManager.Play("pickUp");
                    isSlot1Empty = false;
                    transform.SetParent(slot1);
                }
                else if (isSlot2Empty == true)
                {
                    audioManager.Play("pickUp");
                    isSlot2Empty = false;
                    transform.SetParent(slot2);
                }
                else if (isSlot3Empty == true)
                {
                    audioManager.Play("pickUp");
                    isSlot3Empty = false;
                    transform.SetParent(slot3);
                }

                isEquiped = true;
                rb.isKinematic = true;
                boxCollider.isTrigger = true;

                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.Euler(Vector3.zero);
                gameObject.layer = LayerMask.NameToLayer("Weapons");
            }
            else if (isKnifeHolderEmpty == true && weapon.weaponType == WeaponType.Knife)
            {
                audioManager.Play("pickUp");
                isKnifeHolderEmpty = false;
                transform.SetParent(knifeHolder);

                isEquiped = true;
                rb.isKinematic = true;
                boxCollider.isTrigger = true;

                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.Euler(Vector3.zero);
                gameObject.layer = LayerMask.NameToLayer("Weapons");
            }
        }
        else
        {
            //mensagem a dizer que tem de dropar arma ou que os slots estão cheios
        }
    }
    private void Drop()
    {
        if (isEquiped == true && Input.GetKey(KeyCode.Q))
        {
            audioManager.Play("dropWeapon");

            if (transform.parent == slot1)
            {
                animatorArm.SetTrigger("unEquip");
                animatorArm.SetInteger("weaponType", 0);
                isSlot1Empty = true;
            }
            else if (transform.parent == slot2)
            {
                animatorArm.SetTrigger("unEquip");
                animatorArm.SetInteger("weaponType", 0);
                isSlot2Empty = true;
            }
            else if (transform.parent == slot3)
            {
                animatorArm.SetTrigger("unEquip");
                animatorArm.SetInteger("weaponType", 0);
                isSlot3Empty = true;
            }
            else if (transform.parent == knifeHolder)
            {
                animatorArm.SetTrigger("unEquip");
                animatorArm.SetInteger("weaponType", 0);
                isKnifeHolderEmpty = true;
            }

            isEquiped = false;
            transform.SetParent(null);

            rb.isKinematic = false;
            boxCollider.isTrigger = false;

            rb.velocity = transform.GetComponent<Rigidbody>().velocity;
            rb.AddForce(mainCamera.forward * dropForwardForce, ForceMode.Impulse);
            rb.AddForce(mainCamera.up * dropUpwardForce, ForceMode.Impulse);

            float random = Random.Range(-1f, 1f);
            rb.AddTorque(new Vector3(random, random, random) * 10);
            gameObject.layer = LayerMask.NameToLayer("Pickup");
        }
        else
        {
            //mensagem a dizer que nao tens arma para dropar ou que os slots estão vazios
        }
    }
    private void WeaponAmmo()
    {
        if (weapon.weaponType == WeaponType.Pistol)
        {
            weapon.currentAmmo = weapon.magazineAmmo;
            weapon.storedAmmo = weapon.maxAmmo;
        }
        if (weapon.weaponType == WeaponType.Sniper)
        {
            weapon.currentAmmo = weapon.magazineAmmo;
            weapon.storedAmmo = weapon.maxAmmo;
        }
        if (weapon.weaponType == WeaponType.AK47)
        {
            weapon.currentAmmo = weapon.magazineAmmo;
            weapon.storedAmmo = weapon.maxAmmo;
        }
        if (weapon.weaponType == WeaponType.M4A1)
        {
            weapon.currentAmmo = weapon.magazineAmmo;
            weapon.storedAmmo = weapon.maxAmmo;
        }
        if (weapon.weaponType == WeaponType.Shotgun)
        {
            weapon.currentAmmo = weapon.magazineAmmo;
            weapon.storedAmmo = weapon.maxAmmo;
        }
    }
    public static bool IsSlot1Empty
    {
        get { return isSlot1Empty; }
    }
    public static bool IsSlot2Empty
    {
        get { return isSlot2Empty; }
    }
    public static bool IsSlot3Empty
    {
        get { return isSlot3Empty; }
    }
    public static bool IsKnifeHolderEmpty
    {
        get { return isKnifeHolderEmpty; }
    }
}
