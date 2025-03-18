using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouselook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform player;
    public PlayerHealth playerHealth;
    public float minAngle = -90f;
    public float maxAngle = 90f;
    public PopUpSystem popUpSystem;
    public UIManager uimanager;

    private float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.IsDead == true || popUpSystem.PlayerWon == true || uimanager.IsPaused == true)
        {
            Cursor.lockState = CursorLockMode.None;
            return;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minAngle, maxAngle);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
    }
}