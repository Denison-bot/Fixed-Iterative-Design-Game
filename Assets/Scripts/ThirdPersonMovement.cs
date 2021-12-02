using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float gravity = -9.81f;
    public float speed = 6f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    new Vector3 playerVelocity;
    bool isGrounded;
    bool canPickUp = false;
    public int pagesCollected = 0;
    public TMP_Text pageCount;
    public TMP_Text pressE;
    public GameObject currentObject;
    public GameObject pressEText;

    // Update is called once per frame
    void Update()
    {
        pressE.text = ("Press E to collect pages");
        pageCount.text = ("Pages Collected: " + pagesCollected);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);

            
        }
        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (canPickUp == true && Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log("Button Pressed");
            currentObject.SetActive(false);
            pagesCollected++;
            //pressEText.SetActive(false);
            canPickUp = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Collectable"))
        {
            currentObject = other.gameObject;

            canPickUp = true;
        }       
    }
}
