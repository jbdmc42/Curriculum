using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;

namespace Unity.PlayerMovement
{
    public class PlayerMovement : MonoBehaviour
    {
        public CharacterController controller;

        public float walkSpeed = 4f;
        public float sprintSpeed = 6f;
        public float crouchSpeed = 2f;
        public float walkRadius = 4f;
        public float sprintRadius = 7f;
        public float crouchRadius = 1f;
        public float gravity = -19.62f;
        public float jumpHeight = 1f;
        public Transform CameraHolder;
        public Transform groundCheck;
        public Animator animatorArm;
        public Animator animatorBody;
        public SphereCollider sphereCollider;
        public AudioManager audioManager;
        public PlayerHealth playerHealth;
        public float footstepDelay = 0.5f;
        public float groundDistance = 0.4f;
        public LayerMask groundMask;
        public PopUpSystem popUpSystem;

        private Vector3 velocity;
        private float footstepTime;
        private float speed;
        private bool isGrounded;
        private bool isWalking = false;
        private bool isSprinting = false;
        private bool isCrouching = false;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (playerHealth.IsDead == true || popUpSystem.PlayerWon == true)
            {
                return;
            }

            //Ground and Physics

            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            //Movement and Gravity

            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;

            controller.Move(move * speed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);

            Walk();
            Sprint();
            Crouch();
            CrouchWalk();
            Jump();
            BlendTree();
            SphereRadius();
        }
        private void Walk()
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                if (footstepTime <= 0 && isSprinting == false && isCrouching == false && isGrounded)
                {
                    audioManager.Play("walk");
                    footstepTime = footstepDelay;
                }
                footstepTime -= Time.deltaTime;
                speed = walkSpeed;
                animatorArm.SetBool("isWalking", true);
                isWalking = true;
            }
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            {
                animatorArm.SetBool("isWalking", false);
                isWalking = false;
            }
        }
        private void Sprint()
        {
            if (Input.GetKey(KeyCode.LeftShift) && isWalking == true && isCrouching == false && isGrounded)
            {
                if (footstepTime <= 0)
                {
                    audioManager.Play("run");
                    footstepTime = footstepDelay;
                }
                footstepTime -= Time.deltaTime;
                speed = sprintSpeed;
                animatorArm.SetBool("isSprinting", true);
                isSprinting = true;
            }

            if (!Input.GetKey(KeyCode.LeftShift) || isWalking == false || isCrouching == true)
            {
                animatorArm.SetBool("isSprinting", false);
                isSprinting = false;
            }
        }
        private void Crouch()
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                speed = crouchSpeed;
                animatorArm.SetBool("isCrouching", true);
                animatorBody.SetBool("isCrouching", true);
                isCrouching = true;
                CameraHolder.localPosition = Vector3.Lerp(CameraHolder.localPosition, new Vector3(0, 0f, 0.55f), 5f * Time.deltaTime);
                controller.height = Mathf.Lerp(controller.height, 1, 5f * Time.deltaTime);
                controller.center = Vector3.Lerp(controller.center, new Vector3(0, -0.4f, 0), 5f * Time.deltaTime);
            }

            if (!Input.GetKey(KeyCode.LeftControl))
            {
                // Check if there's space above before standing up
                if (!Physics.Raycast(transform.position, Vector3.up, 1.8f - controller.height, groundMask))
                {
                    animatorArm.SetBool("isCrouching", false);
                    animatorBody.SetBool("isCrouching", false);
                    isCrouching = false;
                    CameraHolder.localPosition = Vector3.Lerp(CameraHolder.localPosition, new Vector3(0f, 0.4f, 0.4f), 5f * Time.deltaTime);
                    controller.height = Mathf.Lerp(controller.height, 1.8f, 5f * Time.deltaTime);
                    controller.center = Vector3.Lerp(controller.center, new Vector3(0, 0, 0), 5f * Time.deltaTime);
                }
            }
        }
        private void CrouchWalk()
        {
            if (isCrouching == true && isWalking == true)
            {
                if (footstepTime <= 0)
                {
                    audioManager.Play("crouch");
                    footstepTime = footstepDelay + 1f;
                }
                footstepTime -= Time.deltaTime;
                CameraHolder.localPosition = Vector3.Lerp(CameraHolder.localPosition, new Vector3(0, 0.1f, 0.55f), 5f * Time.deltaTime);
            }

            if (isCrouching == true && isWalking == false)
            {
                CameraHolder.localPosition = Vector3.Lerp(CameraHolder.localPosition, new Vector3(0, 0f, 0.55f), 5f * Time.deltaTime);
            }
        }
        private void Jump()
        {
            if (Input.GetButtonDown("Jump") && isCrouching == false && isGrounded)
            {
                audioManager.Play("jump");
                animatorArm.SetTrigger("Jump");
                animatorBody.SetTrigger("Jump");
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
        private void BlendTree()
        {
            if (isWalking == false)
            {
                animatorBody.SetFloat("VelocityX", 0f, 0.2f, Time.deltaTime);
                animatorBody.SetFloat("VelocityY", 0f, 0.2f, Time.deltaTime);
            }
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                animatorBody.SetFloat("VelocityX", 0f, 0.2f, Time.deltaTime);
                if (Input.GetKey(KeyCode.W) && (!Input.GetKey(KeyCode.LeftShift) || isCrouching == true))
                {
                    animatorBody.SetFloat("VelocityY", 1f, 0.2f, Time.deltaTime);
                }
                else if (Input.GetKey(KeyCode.S) && (!Input.GetKey(KeyCode.LeftShift) || isCrouching == true))
                {
                    animatorBody.SetFloat("VelocityY", -1f, 0.2f, Time.deltaTime);
                }
                else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && isCrouching == false)
                {
                    animatorBody.SetFloat("VelocityY", 2f, 0.2f, Time.deltaTime);
                }
                else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift) && isCrouching == false)
                {
                    animatorBody.SetFloat("VelocityY", -2f, 0.2f, Time.deltaTime);
                }
            }
            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && (!Input.GetKey(KeyCode.LeftShift) || isCrouching == true))
            {
                animatorBody.SetFloat("VelocityX", -1f, 0.2f, Time.deltaTime);
                if (Input.GetKey(KeyCode.W))
                {
                    animatorBody.SetFloat("VelocityY", 1f, 0.2f, Time.deltaTime);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    animatorBody.SetFloat("VelocityY", -1f, 0.2f, Time.deltaTime);
                }
                else
                {
                    animatorBody.SetFloat("VelocityY", 0f, 0.2f, Time.deltaTime);
                }
            }
            if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && (!Input.GetKey(KeyCode.LeftShift) || isCrouching == true))
            {
                animatorBody.SetFloat("VelocityX", 1f, 0.2f, Time.deltaTime);
                if (Input.GetKey(KeyCode.W))
                {
                    animatorBody.SetFloat("VelocityY", 1f, 0.2f, Time.deltaTime);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    animatorBody.SetFloat("VelocityY", -1f, 0.2f, Time.deltaTime);
                }
                else
                {
                    animatorBody.SetFloat("VelocityY", 0f, 0.2f, Time.deltaTime);
                }
            }
            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift) && isCrouching == false)
            {
                animatorBody.SetFloat("VelocityX", -2f, 0.2f, Time.deltaTime);
                if (Input.GetKey(KeyCode.W))
                {
                    animatorBody.SetFloat("VelocityY", 2f, 0.2f, Time.deltaTime);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    animatorBody.SetFloat("VelocityY", -2f, 0.2f, Time.deltaTime);
                }
                else
                {
                    animatorBody.SetFloat("VelocityY", 0f, 0.2f, Time.deltaTime);
                }
            }
            if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift) && isCrouching == false)
            {
                animatorBody.SetFloat("VelocityX", 2f, 0.2f, Time.deltaTime);
                if (Input.GetKey(KeyCode.W))
                {
                    animatorBody.SetFloat("VelocityY", 2f, 0.2f, Time.deltaTime);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    animatorBody.SetFloat("VelocityY", -2f, 0.2f, Time.deltaTime);
                }
                else
                {
                    animatorBody.SetFloat("VelocityY", 0f, 0.2f, Time.deltaTime);
                }
            }
        }
        private void SphereRadius()
        {
            if (isSprinting == true)
            {
                sphereCollider.radius = sprintRadius;
            }
            else if (isCrouching == true && isWalking == true)
            {
                sphereCollider.radius = crouchRadius;
            }
            else if (isWalking == true)
            {
                sphereCollider.radius = walkRadius;
            }
            else
            {
                sphereCollider.radius = controller.radius;
            }
        }
        private void OnTriggerEnter(Collider gameObject)
        {
            if (gameObject.CompareTag("Zombie"))
            {
                gameObject.GetComponent<ZombieMovement>().OnAware();
            }
        }
    }
}