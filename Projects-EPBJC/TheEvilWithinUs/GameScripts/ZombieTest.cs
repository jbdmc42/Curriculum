using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.PlayerMovement;
using UnityEngine.UI;
using TMPro;

public class ZombieTest : MonoBehaviour
{
    public PlayerMovement move;
    public int health = 30;
    public float fov = 120f;
    public float ViewDistance = 5f;
    public float speed = 3f;
    private bool isAware = false;
    private NavMeshAgent agent;
    private Animator animator;

    private Collider[] ragdollColliders;
    private Rigidbody[] ragdollRigidbodies;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        ragdollColliders = GetComponentsInChildren<Collider>();
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Collider col in ragdollColliders)
        {
            if (!col.CompareTag("Zombie"))
            {
                col.enabled = false;
            }
        }
        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Die();
            return;
        }
        if (isAware) 
        {
            agent.SetDestination(move.transform.position);
            animator.SetBool("Aware", true);
        }
        else
        {
            SearchForPlayer();
            animator.SetBool("Aware", false);
        }
    }

    public void Die()
    {
        agent.speed = 0;
        animator.enabled = false;

        foreach (Collider col in ragdollColliders)
        {
            col.enabled = true;
        }
        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = false;
        }
    }
    public void OnHit(int damage)
    {
        health -= damage;
    }
    public void SearchForPlayer()
    {
        //if (Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(move.transform.position)) < fov)
        {
            if (Vector3.Distance(move.transform.position, transform.position) < ViewDistance)
            {
                OnAware();
            }
        }
    }
    public void OnAware()
    {
        isAware = true;
    }
}
