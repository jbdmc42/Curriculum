using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponShooting : MonoBehaviour
{
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;
    public GameObject knifeHolder;
    public Animator animatorArm;
    public Camera mainCamera;
    public GameObject weaponCamera;
    public GameObject UICamera;
    public GameObject scopeUI;
    public float scopedFOV = 10f;
    public AudioManager audioManager;
    public PlayerHealth playerHealth;
    public CameraShake cameraShake;
    public float cameraShakeMagnitude = 0.01f;
    public float cameraShakeDuration = 0.01f;
    public GameObject bloodEffect;
    public GameObject dustEffect;
    public GameObject impactEffect;
    public float impactForce = 60f;
    public float soundIntensity = 20f;
    public LayerMask zombieMask;
    public GameObject ammoUI;
    public TextMeshProUGUI ammoText;
    public PopUpSystem popUpSystem;
    public UIManager uimanager;

    private Weapon knife;
    private Weapon weapon1;
    private Weapon weapon2;
    private Weapon weapon3;
    private ParticleSystem muzzleFlash;
    private ParticleSystem muzzleflash1;
    private ParticleSystem muzzleflash2;
    private ParticleSystem muzzleflash3;

    private float reloadTime;
    private float spread;
    private float damage;
    private float range;
    private float fireRate;
    private float attackTime;
    private float fireTime = 0f;
    private float attackDelay = 0.025f;
    private float normalFOV = 60f;
    private int attackCount = 0;
    private int bulletsPerShot;
    private int storedAmmo;
    private int currentAmmo;
    private int random;
    private string shootClip;
    private string weaponClip;
    private bool automaticFire;
    private bool scoped;
    private bool isReloading;
    private bool isMagazineEmpty;
    private bool isMagazineFull;
    private bool isStoredAmmoEmpty;

    private bool isSlot1Empty => PickUpSystem.isSlot1Empty;
    private bool isSlot2Empty => PickUpSystem.isSlot2Empty;
    private bool isSlot3Empty => PickUpSystem.isSlot3Empty;
    private bool isKnifeHolderEmpty => PickUpSystem.isKnifeHolderEmpty;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.IsDead == true || popUpSystem.PlayerWon == true || uimanager.IsPaused == true)
        {
            return;
        }

        GetReferences();
        WeaponVariables();
        WeaponReload();
        WeaponShoot();
        WeaponAim();
    }
    private void GetReferences()
    {
        if (isSlot1Empty == false)
        {
            weapon1 = slot1.GetComponentInChildren<WeaponObject>().weapon;
            muzzleflash1 = slot1.GetComponentInChildren<ParticleSystem>();
        }
        if (isSlot2Empty == false)
        {
            weapon2 = slot2.GetComponentInChildren<WeaponObject>().weapon;
            muzzleflash2 = slot2.GetComponentInChildren<ParticleSystem>();
        }
        if (isSlot3Empty == false)
        {
            weapon3 = slot3.GetComponentInChildren<WeaponObject>().weapon;
            muzzleflash3 = slot3.GetComponentInChildren<ParticleSystem>();
        }
        if (isKnifeHolderEmpty == false)
        {
            knife = knifeHolder.GetComponentInChildren<WeaponObject>().weapon;
        }
    }
    private void WeaponVariables()
    {
        if (slot1.activeSelf)
        {
            if (weapon1.weaponType == WeaponType.M4A1 || weapon1.weaponType == WeaponType.AK47)
            {
                automaticFire = true;
                scoped = false;
            }
            else if (weapon1.weaponType == WeaponType.Sniper)
            {
                automaticFire = false;
                scoped = true;
            }
            else
            {
                automaticFire = false;
                scoped = false;
            }

            if (weapon1.currentAmmo <= 0)
            {
                isMagazineEmpty = true;
            }
            else
            {
                isMagazineEmpty = false;
            }

            if (weapon1.currentAmmo == weapon1.magazineAmmo)
            {
                isMagazineFull = true;
            }
            else
            {
                isMagazineFull = false;
            }

            if (weapon1.storedAmmo <= 0)
            {
                weapon1.storedAmmo = 0;
                isStoredAmmoEmpty = true;
            }
            else
            {
                isStoredAmmoEmpty = false;
            }

            damage = weapon1.damage;
            fireRate = weapon1.fireRate;
            range = weapon1.range;
            spread = weapon1.spread;
            bulletsPerShot = weapon1.bulletsPerShot;
            muzzleFlash = muzzleflash1;
            reloadTime = weapon1.reloadTime;
            shootClip = weapon1.shootClip;
            weaponClip = weapon1.weaponClip;
            ammoUI.SetActive(true);
            ammoText.SetText(weapon1.currentAmmo + " / " + weapon1.storedAmmo);
        }
        else if (slot2.activeSelf)
        {
            if (weapon2.weaponType == WeaponType.M4A1 || weapon2.weaponType == WeaponType.AK47)
            {
                automaticFire = true;
                scoped = false;
            }
            else if (weapon2.weaponType == WeaponType.Sniper)
            {
                automaticFire = false;
                scoped = true;
            }
            else
            {
                automaticFire = false;
                scoped = false;
            }

            if (weapon2.currentAmmo <= 0)
            {
                isMagazineEmpty = true;
            }
            else
            {
                isMagazineEmpty = false;
            }

            if (weapon2.currentAmmo == weapon2.magazineAmmo)
            {
                isMagazineFull = true;
            }
            else
            {
                isMagazineFull = false;
            }

            if (weapon2.storedAmmo <= 0)
            {
                weapon2.storedAmmo = 0;
                isStoredAmmoEmpty = true;
            }
            else
            {
                isStoredAmmoEmpty = false;
            }

            damage = weapon2.damage;
            fireRate = weapon2.fireRate;
            range = weapon2.range;
            spread = weapon2.spread;
            bulletsPerShot = weapon2.bulletsPerShot;
            muzzleFlash = muzzleflash2;
            reloadTime = weapon2.reloadTime;
            shootClip = weapon2.shootClip;
            weaponClip = weapon2.weaponClip;
            ammoUI.SetActive(true);
            ammoText.SetText(weapon2.currentAmmo + " / " + weapon2.storedAmmo);
        }
        else if (slot3.activeSelf)
        {
            if (weapon3.weaponType == WeaponType.M4A1 || weapon3.weaponType == WeaponType.AK47)
            {
                automaticFire = true;
                scoped = false;
            }
            else if (weapon3.weaponType == WeaponType.Sniper)
            {
                automaticFire = false;
                scoped = true;
            }
            else
            {
                automaticFire = false;
                scoped = false;
            }

            if (weapon3.currentAmmo <= 0)
            {
                isMagazineEmpty = true;
            }
            else
            {
                isMagazineEmpty = false;
            }

            if (weapon3.currentAmmo == weapon3.magazineAmmo)
            {
                isMagazineFull = true;
            }
            else
            {
                isMagazineFull = false;
            }

            if (weapon3.storedAmmo <= 0)
            {
                weapon3.storedAmmo = 0;
                isStoredAmmoEmpty = true;
            }
            else
            {
                isStoredAmmoEmpty = false;
            }

            damage = weapon3.damage;
            fireRate = weapon3.fireRate;
            range = weapon3.range;
            spread = weapon3.spread;
            bulletsPerShot = weapon3.bulletsPerShot;
            muzzleFlash = muzzleflash3;
            reloadTime = weapon3.reloadTime;
            shootClip = weapon3.shootClip;
            weaponClip = weapon3.weaponClip;
            ammoUI.SetActive(true);
            ammoText.SetText(weapon3.currentAmmo + " / " + weapon3.storedAmmo);
        }
        else if (knifeHolder.activeSelf)
        {
            damage = knife.damage;
            fireRate = knife.fireRate;
            range = knife.range;
            ammoUI.SetActive(false);
            automaticFire = false;
        }
        else
        {
            damage = 10f;
            fireRate = 3f;
            range = 1f;
            ammoUI.SetActive(false);
            automaticFire = false;
        }
    }
    private void WeaponReload()
    {
        if (Input.GetKey(KeyCode.R) && isMagazineFull == false && isReloading == false && isStoredAmmoEmpty == false)
        {
            if (slot1.activeSelf || slot2.activeSelf || slot3.activeSelf)
            {
                StartCoroutine(Reload());
            }
        }
    }
    private void WeaponShoot()
    {
        if (slot1.activeSelf || slot2.activeSelf || slot3.activeSelf)
        {
            if (automaticFire == true && animatorArm.GetInteger("weaponType") != 0)
            {
                if (Input.GetKey(KeyCode.Mouse0) && Time.time >= fireTime && isMagazineEmpty == false && isReloading == false)
                {
                    animatorArm.SetBool("isFiring", true);
                    fireTime = Time.time + (1f / fireRate);
                    Shoot();
                }
                if (!Input.GetKey(KeyCode.Mouse0))
                {
                    animatorArm.SetBool("isFiring", false);
                }
                if (isMagazineEmpty == true || isReloading == true)
                {
                    animatorArm.SetBool("isFiring", false);
                }
            }

            else if (automaticFire == false && animatorArm.GetInteger("weaponType") != 0 && animatorArm.GetInteger("weaponType") != 1)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= fireTime && isMagazineEmpty == false && isReloading == false)
                {
                    animatorArm.SetTrigger("Fire");
                    fireTime = Time.time + (1f / fireRate);
                    Shoot();
                }
                if (!Input.GetKeyDown(KeyCode.Mouse0))
                {
                    animatorArm.SetBool("isFiring", false);
                }
            }
        }
        if (automaticFire == false && animatorArm.GetInteger("weaponType") == 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= fireTime)
            {
                fireTime = Time.time + (1f / fireRate);
                MeleeAttack();
            }
        }
        else if (automaticFire == false && animatorArm.GetInteger("weaponType") == 1)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= fireTime)
            {
                fireTime = Time.time + (1f / fireRate);
                KnifeAttack();
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && isMagazineEmpty == true && Time.time >= fireTime)
        {
            fireTime = Time.time + (1f / fireRate);
            audioManager.Play("magazineEmpty");
        }
    }
    private void WeaponAim()
    {
        if (slot1.activeSelf || slot2.activeSelf || slot3.activeSelf)
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                if (scoped == true)
                {
                    animatorArm.SetBool("isScoped", true);
                    scopeUI.SetActive(true);
                    weaponCamera.SetActive(false);
                    UICamera.SetActive(false);
                    mainCamera.fieldOfView = scopedFOV;
                }
                else
                {
                    animatorArm.SetBool("isAiming", true);
                }
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {

                audioManager.Play("aim");
            }
        }

        if (!Input.GetKey(KeyCode.Mouse1))
        {
            animatorArm.SetBool("isScoped", false);
            animatorArm.SetBool("isAiming", false);
            scopeUI.SetActive(false);
            weaponCamera.SetActive(true);
            UICamera.SetActive(true);
            mainCamera.fieldOfView = normalFOV;
        }
        if (scoped == false || animatorArm.GetInteger("weaponType") == 0 || isReloading == true)
        {
            animatorArm.SetBool("isScoped", false);
            scopeUI.SetActive(false);
            weaponCamera.SetActive(true);
            UICamera.SetActive(true);
            mainCamera.fieldOfView = normalFOV;
        }
    }
    private void CurrentAmmo()
    {
        if (slot1.activeSelf)
        {
            weapon1.currentAmmo--;
        }
        if (slot2.activeSelf)
        {
            weapon2.currentAmmo--;
        }
        if (slot3.activeSelf)
        {
            weapon3.currentAmmo--;
        }
    }
    IEnumerator Reload()
    {
        isReloading = true;

        audioManager.Play("reloadClip");

        animatorArm.SetBool("isReloading", true);

        yield return new WaitForSeconds(reloadTime - 0.2f);

        animatorArm.SetBool("isReloading", false);

        if (animatorArm.GetInteger("weaponType") != 0)
        {
            audioManager.Play(weaponClip);
        }
        else
        {
            audioManager.Stop(weaponClip);
        }

        yield return new WaitForSeconds(0.2f);

        if (slot1.activeSelf)
        {
            currentAmmo = weapon1.currentAmmo;

            if (weapon1.storedAmmo < weapon1.magazineAmmo)
            {
                if (weapon1.storedAmmo + weapon1.currentAmmo > weapon1.magazineAmmo)
                {
                    storedAmmo = (weapon1.storedAmmo + weapon1.currentAmmo) - weapon1.magazineAmmo;

                    weapon1.currentAmmo = weapon1.magazineAmmo;
                    weapon1.storedAmmo = storedAmmo;
                }
                else
                {
                    weapon1.currentAmmo += weapon1.storedAmmo;
                    weapon1.storedAmmo = 0;
                }
            }
            else
            {
                weapon1.currentAmmo = weapon1.magazineAmmo;
                weapon1.storedAmmo -= (weapon1.magazineAmmo - currentAmmo);
            }
        }
        if (slot2.activeSelf)
        {
            currentAmmo = weapon2.currentAmmo;

            if (weapon2.storedAmmo < weapon2.magazineAmmo)
            {
                if (weapon2.storedAmmo + weapon2.currentAmmo > weapon2.magazineAmmo)
                {
                    storedAmmo = (weapon2.storedAmmo + weapon2.currentAmmo) - weapon2.magazineAmmo;

                    weapon2.currentAmmo = weapon2.magazineAmmo;
                    weapon2.storedAmmo = storedAmmo;
                }
                else
                {
                    weapon2.currentAmmo += weapon2.storedAmmo;
                    weapon2.storedAmmo = 0;
                }
            }
            else
            {
                weapon2.currentAmmo = weapon2.magazineAmmo;
                weapon2.storedAmmo -= (weapon2.magazineAmmo - currentAmmo);
            }
        }
        if (slot3.activeSelf)
        {
            currentAmmo = weapon3.currentAmmo;

            if (weapon3.storedAmmo < weapon3.magazineAmmo)
            {
                if (weapon3.storedAmmo + weapon3.currentAmmo > weapon3.magazineAmmo)
                {
                    storedAmmo = (weapon3.storedAmmo + weapon3.currentAmmo) - weapon3.magazineAmmo;

                    weapon3.currentAmmo = weapon3.magazineAmmo;
                    weapon3.storedAmmo = storedAmmo;
                }
                else
                {
                    weapon3.currentAmmo += weapon3.storedAmmo;
                    weapon3.storedAmmo = 0;
                }
            }
            else
            {
                weapon3.currentAmmo = weapon3.magazineAmmo;
                weapon3.storedAmmo -= (weapon3.magazineAmmo - currentAmmo);
            }
        }

        isReloading = false;
    }
    private void Shoot()
    {
        muzzleFlash.Play();
        audioManager.Play(shootClip);
        Collider[] zombies = Physics.OverlapSphere(transform.position, soundIntensity, zombieMask);
        for (int i = 0; i < zombies.Length; i++)
        {
            zombies[i].GetComponent<ZombieMovement>().OnAware();
        }
        CurrentAmmo();

        //ShakeCamera
        StartCoroutine(cameraShake.Shake(cameraShakeDuration, cameraShakeMagnitude));

        for (int i = 0; i < bulletsPerShot; i++)
        {
            //Spread
            float x = Random.Range(-spread, spread);
            float y = Random.Range(-spread, spread);

            Vector3 direction = mainCamera.transform.rotation * new Vector3(x, y, 1f);

            RaycastHit hit;
            if (Physics.Raycast(mainCamera.transform.position, direction, out hit, range))
            {
                //Debug.Log(hit.transform.name);

                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);

                    GameObject bloodGameObject = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(bloodGameObject, 2f);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }

                if (hit.rigidbody == null)
                {
                    GameObject impactGameObject = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(impactGameObject, 2f);
                }
                else if (hit.rigidbody != null && target == null)
                {
                    GameObject dustGameObject = Instantiate(dustEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(dustGameObject, 2f);
                }
            }
        }
    }
    private void MeleeAttack()
    {
        random = Random.Range(1, 3);
        if (random == 1)
        {
            animatorArm.SetTrigger("AttackLeft");
        }
        else
        {
            animatorArm.SetTrigger("AttackRight");
        }
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);

                GameObject bloodGameObject = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(bloodGameObject, 2f);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            if (target != null)
            {
                if (attackTime <= 0)
                {
                    audioManager.Play("punchEnemy");
                    attackTime = attackDelay;
                }
                attackTime -= Time.deltaTime;
                return;
            }
        }

        if (attackTime <= 0)
        {
            audioManager.Play("punch");
            attackTime = attackDelay;
        }
        attackTime -= Time.deltaTime;
    }
    private void KnifeAttack()
    {
        if (attackCount == 0)
        {
            animatorArm.SetTrigger("AttackLeft");
            attackCount++;
        }
        else
        {
            animatorArm.SetTrigger("AttackRight");
            attackCount = 0;
        }
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);

                GameObject bloodGameObject = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(bloodGameObject, 2f);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            if (target != null)
            {
                if (attackTime <= 0)
                {
                    audioManager.Play("stabEnemy");
                    attackTime = attackDelay;
                }
                attackTime -= Time.deltaTime;
                return;
            }
        }

        if (attackTime <= 0)
        {
            audioManager.Play("stab");
            attackTime = attackDelay;
        }
        attackTime -= Time.deltaTime;
    }
}
