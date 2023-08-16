using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;        // player movement speed
    [SerializeField]
    private float jumpForce;        // player jump force
    [SerializeField]
    private GameObject groundCheck; // ground check gameobject
    [SerializeField]
    private LayerMask groundLayer;  // layer mask to use as ground
    [SerializeField]
    private FloatingJoystick _js;      // Joystick
    Vector2 movement;
    public Animator animator;
    private float horizontalAxis;
    private bool isGrounded;

    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();      // get the rigidbody component
    }

    // Update is called once per frame
    void Update()
    {
        // for non physics objects
    }

    // better optimized for physics objects
    void FixedUpdate() {

        movement.x = _js.Horizontal;
        movement.y = _js.Vertical;
        //_rb.velocity = new Vector2(moveSpeed * horizontalAxis, _rb.velocity.y);
        _rb.velocity = new Vector2(_js.Horizontal * moveSpeed, _rb.velocity.y);

        // check if player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, 0.1f, groundLayer);

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

    }

    // movement input
    public void OnMove(InputAction.CallbackContext context) {
        horizontalAxis = context.ReadValue<float>();
    }

    // jump input
    public void OnJump(InputAction.CallbackContext context) {
        if(context.performed && isGrounded) {
            _rb.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);
        }
    }
}