using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using static ZombieMovement;

public class ZombieAttack : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;
    public ZombieMovement zombieMovement;
    public AudioManager audioManager;
    public PlayerHealth playerHealth;
    public Transform target;
    public float damage = 15;
    public PopUpSystem popUpSystem;

    private ZombieType zombieType;
    private float attackDelay = 0.5f;
    private float attackTime = 0;
    private int attackCount = 0;
    private float attackDamage;
    private float health;
    private bool isStopping = false;
    private bool isAttacking = false;
    private bool isRaging = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetReferences();
        if (health == 0)
        {
            return;
        }
        if (zombieMovement.IsScreaming == true && isRaging == false)
        {
            damage *= 2;
            isRaging = true;
        }

        if (playerHealth.IsDead == false || popUpSystem.PlayerWon == false)
        { 
            Attack();
        }
    }
    private void GetReferences()
    {
        health = transform.GetComponent<Target>().health;
        zombieType = transform.GetComponent<ZombieMovement>().zombieType;
    }
    private void Attack()
    {
        float targetDistance = Vector3.Distance(target.position, transform.position);

        if (targetDistance <= agent.stoppingDistance)
        {
            if (isStopping != true && isRaging == false && zombieType != ZombieType.Parasite)
            {
                isStopping = true;
                attackTime = Time.time;
            }

            if (Time.time >= attackTime + attackDelay)
            {
                attackTime = Time.time;
                StartCoroutine(AttackAnimation());
                gameObject.GetComponent<ZombieMovement>().OnAware();
            }
        }
        else
        {
            if (isStopping != false)
            {
                isStopping = false;
            }
        }
    }
    public IEnumerator AttackAnimation()
    {
        if (isAttacking)
        {
            yield break; // exit if already attacking
        }

        isAttacking = true;

        if (zombieType == ZombieType.ZombieBoss)
        {
            int random = Random.Range(1, 6);
            if (random == 1)
            {
                animator.SetTrigger("StrongAttack");
                attackDamage = damage * 2;
                yield return new WaitForSeconds(1.5f);
            }
            else
            {
                animator.SetTrigger("Attack");
                attackDamage = damage;
                yield return new WaitForSeconds(1f);
            }
        }
        else if (zombieType == ZombieType.Zombie)
        {
            animator.SetTrigger("FastAttack");
            attackDamage = damage;
            yield return new WaitForSeconds(0.5f);
        }
        else if (zombieType == ZombieType.Parasite)
        {
            animator.SetTrigger("Attack");
            attackDamage = damage;
            yield return new WaitForSeconds(0.25f);
        }
        else
        {
            animator.SetTrigger("Attack");
            attackDamage = damage;
            yield return new WaitForSeconds(1f);
        }

        isAttacking = false;
    }
    public void ZombieEventFunction()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= agent.stoppingDistance)
        {
            PlayerHealth player = target.GetComponent<PlayerHealth>();
            player.TakeDamagePlayer(attackDamage);
        }

        AttackCount();

        if (zombieType != ZombieType.Zombie)
        {
            audioManager.Play("zombieAttack");
            audioManager.Stop("zombieWalk");
        }
    }
    private void AttackCount()
    {
        if (zombieType == ZombieType.Zombie)
        {
            if (attackCount == 0)
            {
                audioManager.Play("zombieAttack");
                audioManager.Stop("zombieWalk");
                audioManager.Stop("zombieRun");

                attackCount++;
            }
            else
            {
                attackCount = 0;
            }
        }
    }
    public bool IsAttacking
    {
        get { return isAttacking; }
    }
}
