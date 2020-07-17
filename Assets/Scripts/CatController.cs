using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatController : MonoBehaviour
{
    public Button btn;

    [SerializeField]
    float jumpForce = 500f;

    private Rigidbody2D rb;

    bool doubleJumpAllowed = false;
    bool onGround = true;

    void Start()
    {
        btn.onClick.AddListener(Jump);
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (rb.velocity.y == 0)
            onGround = true;
        else
            onGround = false;

        if (onGround)
            doubleJumpAllowed = true;

    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    public void Jump()
    {
        Debug.Log("JUMP!");
        if (onGround)
            rb.AddForce(Vector2.up * jumpForce);
        else if (doubleJumpAllowed)
        {
            Debug.Log("DOUBLE JUMP!");
            rb.AddForce(Vector2.up * jumpForce);
            doubleJumpAllowed = false;
        }
    }
}
