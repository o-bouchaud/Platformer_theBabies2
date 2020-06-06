using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
[SerializeField] private float speed;
[SerializeField] private float maxSpeed;
[SerializeField] private float jumpForce;
[SerializeField] private LayerMask ground;
//in order to select one or more of our ground's layers;

private Rigidbody2D myRigidbody2D;
private Vector2 stickDirection;
private Animator myAnimator;
private bool isOnGround = false;
//to verify if the player is on the ground;
 private bool isFacingLeft = true;
private SpriteRenderer myRenderer;


    private void OnEnable()
    {
        var playerController = new PlayerController();
        playerController.Enable();
        playerController.Main.Move.performed += MoveOnPerformed;
        playerController.Main.Move.canceled += MoveOnCanceled;
        playerController.Main.Jump.performed += JumpOnPerformed;

    }

    private void JumpOnPerformed(InputAction.CallbackContext obj)
    {

        if (isOnGround)
        {
            myRigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isOnGround = false;
        }

    }

    private void MoveOnPerformed(InputAction.CallbackContext obj)
    {
        stickDirection = obj.ReadValue<Vector2>();
    }

    private void MoveOnCanceled(InputAction.CallbackContext obj)
    {
        stickDirection = Vector2.zero;
    }




    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var direction = new Vector2
        {
            x = stickDirection.x, y = 0
        };

        if (myRigidbody2D.velocity.sqrMagnitude < maxSpeed)
        {
            myRigidbody2D.AddForce(direction * speed);
        }


        var isRunning = isOnGround && Mathf.Abs(myRigidbody2D.velocity.x) > 0.1f;
        myAnimator.SetBool("IsRunning", isRunning);
        var isAscending = !isOnGround && myRigidbody2D.velocity.y > 0;
        myAnimator.SetBool("IsAscending", isAscending);
        var isDescending = !isOnGround && myRigidbody2D.velocity.y < 0;
        myAnimator.SetBool("IsDescending", isDescending);
        myAnimator.SetBool("IsGrounded", isOnGround);
        Flip();

        /*if (direction.x < 0)
        {
            myRenderer.flipX = true;
        }
        else if (direction.x > 0)
        {
            myRenderer.flipX = false;
        }*/
    }

    private void Flip()
    {
         if (stickDirection.x < -0.1f)
        {
            isFacingLeft = true;
        }

        if (stickDirection.x > 0.1f)
        {
            isFacingLeft = false;
        }
        myRenderer.flipX = isFacingLeft;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var touchingGround = ground == (ground | (1 << other.gameObject.layer));
        var touchFromAbove = other.contacts[0].normal == Vector2.up;
        if (touchingGround && touchFromAbove)
        {
            isOnGround = true;
        }
    }
}
