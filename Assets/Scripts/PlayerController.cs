using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
