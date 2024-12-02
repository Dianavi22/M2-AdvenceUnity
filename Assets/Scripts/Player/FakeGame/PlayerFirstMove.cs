using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFirstMove : MonoBehaviour
{
    public Vector3 jump;
    public float jumpForce = 2.0f;
    public bool isReadyToBegin = false;
    public bool isGrounded;
    [SerializeField] Rigidbody _rb;
    private bool _hasJump = false;
    private bool _canJump = true;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
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
        if (Input.GetKeyDown(KeyCode.Space) && _canJump)
        {
            Jump();
        }
    }

    public void Jump()
    {
        _canJump = false;
        _rb.AddForce(jump * jumpForce, ForceMode.Impulse);
        Invoke("Prepare", 0.3f);
    }

    private void Prepare()
    {
        _hasJump = true;
    }
}
