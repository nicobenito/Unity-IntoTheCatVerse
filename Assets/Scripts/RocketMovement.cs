using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    public Joystick joystick;
    public float RotationSpeed = 3f;
    public float Speed = 3f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();        
    }

    // Update is called once per frame
    void Update()
    {
        float xAxis = joystick.Horizontal;
        float yAxis = joystick.Vertical;

        ThrustForward(yAxis);
        RotateRocket(xAxis);
    }

    private void ThrustForward(float power)
    {
        Vector2 force = transform.up * power * Speed;
        rb.AddForce(force);
    }

    private void RotateRocket(float xAxis)
    {
        // t.Rotate(0, 0, amount);
        rb.MoveRotation(rb.rotation + (-xAxis * RotationSpeed) * Time.fixedDeltaTime);
    }
}
