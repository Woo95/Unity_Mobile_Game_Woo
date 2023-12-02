using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
	#region singleton
	public static Player instance;
	private void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
			return;
		}
		else
			instance = this;
	}
	#endregion
	
	[SerializeField] private LayerMask m_PlatformLayerMask;
	
	Rigidbody2D m_Rb;
	BoxCollider2D m_BoxCollider2D;

	public float m_MoveSpeed = 2.5f;
	public float m_JumpVelocity = 6.5f;

	private bool m_IsMoveLeft = false;
	private bool m_IsMoveRight = false;
	private bool m_IsJump = false;

	public void Init()
	{
		m_Rb = GetComponent<Rigidbody2D>();
		m_BoxCollider2D = transform.GetComponent<BoxCollider2D>();
}
	public void Move()
	{
		if (m_IsMoveLeft && m_IsMoveRight)
			Stop();
		else if (m_IsMoveLeft)
			MoveLeft();
		else if (m_IsMoveRight)
			MoveRight();
		else
			Stop();

		if (m_IsJump)
		{
			Jump();
		}
	}
	public void MoveWithKeyboard()	// For test purposes
	{
		if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
		{
			Stop();
		}
		else if (Input.GetKey(KeyCode.A))
		{
			MoveLeft();
		}
		else if (Input.GetKey(KeyCode.D))
		{
			MoveRight();
		}
		else
		{
			Stop();
		}
		if (Input.GetKey(KeyCode.Space))
		{
			Jump();
		}
	}

	#region Player Button Handler
	public void HandleButtonAction(eButtonActionType actionType, bool isButtonDown)
	{
		switch (actionType)
		{
			case eButtonActionType.MoveLeft:
				m_IsMoveLeft = isButtonDown;
				break;

			case eButtonActionType.MoveRight:
				m_IsMoveRight = isButtonDown;
				break;

			case eButtonActionType.Jump:
				m_IsJump = isButtonDown;
				break;
		}
	}
	#endregion

	#region Move Behaviours
	public void MoveLeft()
    {
		m_Rb.velocity = new Vector2(-m_MoveSpeed, m_Rb.velocity.y);
	}
    public void MoveRight()
    {
		m_Rb.velocity = new Vector2(+m_MoveSpeed, m_Rb.velocity.y);
	}
	public void Stop()
	{
		m_Rb.velocity = new Vector2(0, m_Rb.velocity.y);
	}
	public void Jump()
    {
		if (IsGrounded())
			m_Rb.velocity = Vector3.up * m_JumpVelocity;
	}
	#endregion

	public void OnPlatform(bool OnPlatform, Transform parent = null)
    {
		transform.SetParent(OnPlatform ? parent : null);
	}
	private bool IsGrounded()
	{
		RaycastHit2D raycastHit = Physics2D.BoxCast(m_BoxCollider2D.bounds.center, m_BoxCollider2D.bounds.size, 0.0f, Vector2.down, 0.1f, m_PlatformLayerMask);
		return raycastHit.collider != null;
	}
}
