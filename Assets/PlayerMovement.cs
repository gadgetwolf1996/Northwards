using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterController2D controller;

    public float horizontalMove = 0f;
    public float runSpeed = 400f;
    bool jump = false;
    bool crouch = false;

    // wind direction countdown
    public float currentTime = 0.0f;
    public float triggerTime = 10.0f;
    public float windDirection = 1.0f;

    // player progression
    public Slider slider;
    public Image image;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = (Input.GetAxisRaw("Horizontal") * (runSpeed + windDirection)) * (jump ? 1 : 0);

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

        currentTime -= Time.deltaTime;
        if (currentTime <= 0.0f)
        {
            windDirection = Random.Range(-runSpeed, runSpeed);
            currentTime = triggerTime;
            
        }

        if (!crouch && horizontalMove == 0f && windDirection < 0.0f)
            horizontalMove = windDirection / 10.0f;
    }

    void FixedUpdate()
    {
        slider.value = transform.position.x / 10.0f;
        

        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;

        image.transform.rotation = Quaternion.Euler(new Vector3(0, 0, (windDirection * 0.45f)));
    }
}
