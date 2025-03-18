using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    public int attackDamage = 30;
    private Animator animator;
    public Transform spherecastSpawn;
    public LayerMask zombieLayer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            attack();
        }
    }

    public void attack()
    {
        animator.SetTrigger("Attack");
        RaycastHit hit;
        if (Physics.SphereCast(spherecastSpawn.transform.position, 0.1f, spherecastSpawn.TransformDirection(Vector3.forward), out hit, zombieLayer))
        {
            hit.transform.GetComponent<ZombieTest>().OnHit(attackDamage);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Zombie"))
        {
            other.GetComponent<ZombieTest>().OnAware();
        }
    }
}
