using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFirstMove : MonoBehaviour
{
    public Vector3 jump;
    public float jumpForce = 2.0f;
    public bool isReadyToBegin = false;
    public bool isGrounded;
    Rigidbody rb;
    private bool _hasJump = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    void OnCollisionStay()
    {
        isGrounded = true;
        if (_hasJump)
        {
            isReadyToBegin = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            Invoke("Prepare", 0.3f);
        }
    }

    private void Prepare()
    {
        _hasJump = true;
    }
}
