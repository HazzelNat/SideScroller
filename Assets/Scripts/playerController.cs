using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] float horizontalSpeed;
    float horizontalInput;
    bool jump = false;
    public bool crouch = false;

	[SerializeField] private float JumpForce = 500f;
	[Range(0, 1)] [SerializeField] private float CrouchSpeed = .36f;
	[Range(1, 2)] [SerializeField] private float CrouchJumpForce = 1f;
	[Range(0, .3f)] [SerializeField] private float MovementSmoothing = .05f;
	[SerializeField] private bool AirControl = false;
	

	[Header("Checks")]
	[SerializeField] private LayerMask m_WhatIsGround;
	[SerializeField] private Transform m_GroundCheck;
	[SerializeField] private Transform m_CeilingCheck;
	[SerializeField] private Collider2D m_CrouchDisableCollider;
	const float k_GroundedRadius = .2f;
	private bool Grounded;
	const float k_CeilingRadius = .00001f;
	private Rigidbody2D rb2d;
	private bool FacingRight = true;
	private Vector3 Velocity = Vector3.zero;
	public bool m_wasCrouching = false;
	private bool isDead = false;
	public bool forcedCrouch = false;


	[Header("Animation")]
	private Animator animator;
	string currentState;
	const string playerIdle = "Player_Idle";
	const string playerRun = "Player_Run";
	const string playerCrouch = "Player_Crouch";
	const string playerCrouchWalk = "Player_CrouchWalk";
	const string playerJump = "Player_Jump";

	private void Awake()
	{
		rb2d = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		animator.Rebind();
	}

	void Update()
    {
		if (!isDead){
			horizontalInput = Input.GetAxisRaw("Horizontal") * horizontalSpeed;

			if (Input.GetButtonDown("Crouch"))
			{
				crouch = true;
			} 

			if (Input.GetButtonUp("Crouch"))
			{
				crouch = false;
			}

			if (Input.GetButtonDown("Jump"))
			{
				jump = true;
			}

			if (!crouch && Grounded)
			{
				if (horizontalInput == 0)
				{
					ChangeAnimationState(playerIdle);
				}
				else 
				{
					ChangeAnimationState(playerRun);
				}
			} 
			else if (Grounded && crouch)
			{
				if (horizontalInput == 0)
				{
					ChangeAnimationState(playerCrouch);
				}
				else 
				{
					ChangeAnimationState(playerCrouchWalk);
				}
			}
		}		
    }

	private void FixedUpdate()
	{
		if (m_wasCrouching && !crouch)
		{
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				Debug.Log("KEJEDOT");

				crouch = true;
				forcedCrouch = true;
			} 
		}

		if (forcedCrouch && !Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround)){
			forcedCrouch = false;
			crouch = false;
		}

		Move(horizontalInput * Time.deltaTime, crouch, jump);

        jump = false;

		bool wasGrounded = Grounded;
		Grounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i=0 ; i<colliders.Length ; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				Grounded = true;
			}
		}
	}

	public void Move(float move, bool crouch, bool jump)
	{
		if (Grounded || AirControl)
		{
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
				}

				move *= CrouchSpeed;

				if (m_CrouchDisableCollider != null)
				{
					m_CrouchDisableCollider.enabled = false;
				}
				
			} 
			else
			{
				if (m_CrouchDisableCollider != null)
				{
					m_CrouchDisableCollider.enabled = true;
				}
				
				if (m_wasCrouching && !forcedCrouch)
				{
					m_wasCrouching = false;
				}
			}

			Vector3 targetVelocity = new Vector2(move * 10f, rb2d.velocity.y);
            
			rb2d.velocity = Vector3.SmoothDamp(rb2d.velocity, targetVelocity, ref Velocity, MovementSmoothing);

			if (move > 0 && !FacingRight)
			{
				Flip();
			}
			else if (move < 0 && FacingRight)
			{
				Flip();
			}
		}
        
		if (Grounded && jump)
		{
			if (m_wasCrouching){
				Grounded = false;
				rb2d.AddForce(new Vector2(0f, JumpForce * CrouchJumpForce));
				ChangeAnimationState(playerJump);
			}

			else if (!m_wasCrouching){
				Grounded = false;
				rb2d.AddForce(new Vector2(0f, JumpForce));
				ChangeAnimationState(playerJump);
			}
				
		}
	}


	private void Flip()
	{
		FacingRight = !FacingRight;

		transform.Rotate(0, 180, 0);
	}


	public void ChangeAnimationState(string newState)
    {
        if(currentState == newState)
        {
            return;
        }

        animator.Play(newState);
        
        currentState = newState;
    }
}
