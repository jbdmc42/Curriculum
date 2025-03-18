using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundSounds : MonoBehaviour
{
    public AudioManager audioManager;

    private float clipTime;
    private float clipDelay;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (clipTime <= 0)
        {
            audioManager.Play("owl");

            clipDelay = Random.Range(20f, 40f);
            clipTime = clipDelay;
        }
        clipTime -= Time.deltaTime;
    }
}
