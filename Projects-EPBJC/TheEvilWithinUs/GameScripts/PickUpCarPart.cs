using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PickUpCarPart : MonoBehaviour
{
    public Transform player;
    public Transform mainCamera;
    public AudioManager audioManager;
    public PlayerHealth playerHealth;
    public PointSystem pointSystem;
    public float points = 500f;
    public float pickUpRange = 2f;
    public bool isCollected = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.IsDead == true)
        {
            return;
        }

        PickUpCarParts();
    }

    private void PickUpCarParts()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        if (distanceToPlayer.magnitude <= pickUpRange && Input.GetKey(KeyCode.E))
        {
            UseCarPart();
            audioManager.Play("pickUp");
            pointSystem.ScorePoints(points);
            isCollected = true;
        }
    }
    private void UseCarPart()
    {
        Destroy(gameObject);
    }
    public bool IsCollected
    {
        get { return isCollected; }
    }
}
