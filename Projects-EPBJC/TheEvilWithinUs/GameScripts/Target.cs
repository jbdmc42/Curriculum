using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Target : MonoBehaviour
{
    public float health = 100;
    public float points = 100;
    public ZombieMovement zombieMovement;
    public bool isDead;
    public PointSystem pointSystem;
    public InfoStats infoStats;

    public void TakeDamage(float damage)
    {
        IsAware();
        if (zombieMovement.IsScreaming == true)
        {
            return;
        }

        health -= damage;
        if (health <= 0f)
        {
            isDead = true;
            health = 0f;
        }

        if (isDead == true && points != 0f)
        {
            infoStats.Kills(1);
            pointSystem.ScorePoints(points);
            points = 0f;
        }
    }
    private void IsAware()
    {
        gameObject.GetComponent<ZombieMovement>().OnAware();
    }
}