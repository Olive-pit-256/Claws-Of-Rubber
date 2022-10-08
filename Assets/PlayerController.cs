using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController: MonoBehaviour
{
    [SerializeField] float turningAcceleration = 0.5f;
    [SerializeField] float maxTurnSpeed = 90.0f;

    private float accelerationCoefficient = 1.0f;
    private bool accelerating = false;
    private float speed = 0.0f;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        UpdateKeyboardLook();
        UpdateSpeed();
    }

    [ExecuteInEditMode]
    void OnValidate()
    {
        turningAcceleration = ClampPositive(turningAcceleration);
        maxTurnSpeed = ClampPositive(maxTurnSpeed);
    }

    float ClampPositive(float value)
    {
        if (value < 0)
        {
            return 0;
        }
        else
        {
            return value;
        }
    }

    void ClampSpeed()
    {
        if (speed > maxTurnSpeed)
        {
            speed = maxTurnSpeed;
        }
        if (speed < -maxTurnSpeed)
        {
            speed = -maxTurnSpeed;
        }
        if (speed < turningAcceleration / 2 && speed > -turningAcceleration / 2)
        {
            speed = 0.0f;
        }
    }

    void UpdateSpeed()
    {
        if (accelerating)
        {
            speed += accelerationCoefficient * turningAcceleration;
        }
        else
        {
            if (speed > 0)
            {
                speed -= turningAcceleration;
            }
            else if (speed < 0)
            {
                speed += turningAcceleration;
            }
        }
        
        ClampSpeed();
        accelerating = false;
        cam.transform.RotateAround(transform.position, transform.up, speed * Time.deltaTime);
    }

    void UpdateKeyboardLook()
    {
        if (Input.GetKey("a"))
        {
            accelerationCoefficient = 1.0f;
            accelerating = true;
            System.Console.Out.WriteLine("reading input from the" + "button");
        }
        else if (Input.GetKey("d"))
        {
            accelerationCoefficient = -1.0f;
            accelerating = true;
            System.Console.Out.WriteLine("reading input from the D button");
        }
    }
}