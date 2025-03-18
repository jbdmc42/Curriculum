using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class ZombieMovement : MonoBehaviour
{
    public enum WanderType { Random, Waypoint };
    public enum ZombieType { Zombie, Walker, ZombieGirl, ZombiePolice, ZombieBoss, Parasite };

    public Animator animator;
    public NavMeshAgent agent;
    public ZombieAttack zombieAttack;
    public AudioManager audioManager;
    public PlayerHealth playerHealth;
    public Transform target;
    public Collider animCollider;
    public LayerMask groundMask;
    public ZombieType zombieType;
    public WanderType wanderType;
    public Transform[] waypoints;
    public float walkSpeed = 1f;
    public float sprintSpeed = 4f;
    public float ViewDistance = 10f;
    public float loseDelay = 10f;
    public float fov = 120f;
    public float wanderPointRange = 5;
    public PopUpSystem popUpSystem;

    private Collider ZombieCollider;
    private BoxCollider ZombieBoxCollider;
    private Vector3 wanderPoint;
    private int waypointIndex = 0;
    private float losetime = 0;
    private float health;
    private float zombieWalkClipTime;
    private float zombieRunClipTime;
    private float zombieWalkClipDelay;
    private float zombieRunClipDelay;
    private bool isAware = false;
    private bool isDetecting = false;
    private bool isScreaming = false;
    private bool isRaging = false;

    // Start is called before the first frame update
    void Start()
    {
        ZombieAnimation();
        wanderPoint = RandomWanderPoint();
    }

    // Update is called once per frame
    void Update()
    {
        GetReferences();
        if (health == 0)
        {
            if (agent.enabled != false)
            {
                audioManager.Stop("zombieWalk");
                audioManager.Play("zombieDie");
                animator.SetTrigger("Die");
            }
            if (ZombieBoxCollider != null)
            {
                ZombieBoxCollider.enabled = false;
            }
            else
            {
                ZombieCollider.enabled = false;
            }
            animCollider.enabled = false;
            agent.enabled = false;
            return;
        }
        if (health <= 1500 && zombieType == ZombieType.ZombieBoss && isRaging == false)
        {
            StartCoroutine(ScreamingAnimation());
        }
        if (playerHealth.IsDead == true || popUpSystem.PlayerWon == true)
        {
            isAware = false;
        }

        if (isAware)
        {
            MoveToTarget();
            RotateToTarget();
        }
        else
        {
            Wander();

            audioManager.Stop("zombieRun");
            zombieRunClipTime = 0;

            if (zombieWalkClipTime <= 0)
            {
                audioManager.Play("zombieWalk");

                zombieWalkClipDelay = Random.Range(4.5f, 5.5f);
                zombieWalkClipTime = zombieWalkClipDelay;
            }
            zombieWalkClipTime -= Time.deltaTime;

            animator.SetBool("Aware", false);
            agent.speed = walkSpeed;
        }
        SearchForTarget();
    }
    private void GetReferences()
    {
        ZombieBoxCollider = transform.GetComponent<BoxCollider>();
        ZombieCollider = transform.GetComponent<CapsuleCollider>();
        health = transform.GetComponent<Target>().health;
    }
    private void MoveToTarget()
    {
        if (zombieAttack.IsAttacking == true || isScreaming == true)
        {
            if (zombieType == ZombieType.ZombieBoss)
            {
                zombieRunClipTime = 1.5f;
            }
            else
            {
                zombieRunClipTime = 1;
            }
            agent.speed = 0;
        }
        else
        {
            agent.SetDestination(target.position);

            audioManager.Stop("zombieWalk");
            zombieWalkClipTime = 0;

            float distance = Vector3.Distance(target.position, transform.position);
            if (distance >= agent.stoppingDistance)
            {
                if (zombieRunClipTime <= 0 && isScreaming == false)
                {
                    audioManager.Play("zombieRun");

                    zombieRunClipDelay = Random.Range(2.5f, 3.5f);
                    zombieRunClipTime = zombieRunClipDelay;
                }
                zombieRunClipTime -= Time.deltaTime;
            }
            else
            {
                if (zombieType == ZombieType.ZombieBoss)
                {
                    zombieRunClipTime = 1.5f;
                }
                else
                {
                    zombieRunClipTime = 1;
                }
            }

            animator.SetBool("Aware", true);
            agent.speed = sprintSpeed;
        }

        if (isDetecting == false)
        {
            losetime += Time.deltaTime;
            if (losetime >= loseDelay)
            {
                isAware = false;
                losetime = 0;
            }
        }
    }
    private void RotateToTarget()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= ViewDistance / 2)
        {
            Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
            transform.LookAt(targetPosition);
        }
    }
    private void SearchForTarget()
    {
        if (Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(target.position)) < fov / 2)
        {
            if (Vector3.Distance(target.position, transform.position) < ViewDistance)
            {
                RaycastHit hit;
                if (Physics.Linecast(transform.position, target.position, out hit, -1))
                {
                    if (hit.transform.CompareTag("Player"))
                    {
                        OnAware();
                    }
                    else
                    {
                        isDetecting = false;
                    }
                }
                else
                {
                    isDetecting = false;
                }
            }
            else
            {
                isDetecting = false;
            }
        }
        else
        {
            isDetecting = false;
        }
    }
    public void OnAware()
    {
        isAware = true;
        isDetecting = true;
        losetime = 0;
    }
    private void Wander()
    {
        if (wanderType == WanderType.Random)
        {
            if (Vector3.Distance(transform.position, wanderPoint) < 2.5f)
            {
                wanderPoint = RandomWanderPoint();
            }
            else
            {
                agent.SetDestination(wanderPoint);
            }
        }
        else if (wanderType == WanderType.Waypoint)
        {
            if (waypoints.Length >= 2)
            {
                if (Vector3.Distance(waypoints[waypointIndex].position, transform.position) < 2.5f)
                {

                    if (waypointIndex == (waypoints.Length - 1))
                    {
                        waypointIndex = 0;
                    }
                    else
                    {
                        waypointIndex++;
                    }
                }
                else
                {
                    agent.SetDestination(waypoints[waypointIndex].position);
                }
            }
            else
            {
                Debug.Log("Só tem um Waypoint: " + gameObject.name);
            }
        }
    }
    private Vector3 RandomWanderPoint()
    {
        float randomZ = Random.Range(-wanderPointRange, wanderPointRange);
        float randomX = Random.Range(-wanderPointRange, wanderPointRange);

        wanderPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(wanderPoint, -transform.up, 2f, groundMask))
        {
            return new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        }
        else
        {
            return new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }
    IEnumerator ScreamingAnimation()
    {
        isScreaming = true;
        animator.SetTrigger("Scream");
        audioManager.Play("zombieBossScream");
        audioManager.Stop("zombieWalk");
        audioManager.Stop("zombieRun");
        audioManager.Stop("zombieAttack");

        isRaging = true;
        sprintSpeed += 1;

        yield return new WaitForSeconds(2f);

        isScreaming = false;
    }
    private void ZombieAnimation()
    {
        if (zombieType == ZombieType.Zombie)
        {
            animator.SetInteger("zombieType", 0);
        }
        else if (zombieType == ZombieType.Walker)
        {
            animator.SetInteger("zombieType", 1);
        }
        else if (zombieType == ZombieType.ZombieGirl)
        {
            animator.SetInteger("zombieType", 2);
        }
        else if (zombieType == ZombieType.ZombiePolice)
        {
            animator.SetInteger("zombieType", 3);
        }
        else if (zombieType == ZombieType.ZombieBoss)
        {
            animator.SetInteger("zombieType", 4);
        }
        else if (zombieType == ZombieType.Parasite)
        {
            animator.SetInteger("zombieType", 5);
        }
    }
    public bool IsScreaming
    {
        get { return isScreaming; }
    }
}
