using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public LayerMask m_PlatformLayerMask;
	
	Rigidbody2D m_Rb;
	CapsuleCollider2D m_PlayerFootCollider;

	public float m_MoveSpeed = 2.5f;
	public float m_JumpVelocity = 6.5f;

	private bool m_IsMoveLeft = false;
	private bool m_IsMoveRight = false;
	private bool m_IsJump = false;

	public bool m_FaceRight = true;
	public Animator m_Animator;

	public Transform m_HitAreaA, m_HitAreaB;
	public LayerMask m_HitLayerMask;

	public void Init()
	{
		m_Rb = GetComponent<Rigidbody2D>();
		m_PlayerFootCollider = GetComponent<CapsuleCollider2D>();

		m_Animator = GetComponent<Animator>();
	}

	#region Move Input
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
			Jump();


		CheckEnemyToKill();
		UpdateFootColliderState();
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

		CheckEnemyToKill();
		UpdateFootColliderState();
	}
	#endregion

	#region Move Behaviours
	public void MoveLeft()
    {
		m_Rb.velocity = new Vector2(-m_MoveSpeed, m_Rb.velocity.y);
		Flip(true);
		m_Animator.SetBool("Move", true);
	}
    public void MoveRight()
    {
		m_Rb.velocity = new Vector2(+m_MoveSpeed, m_Rb.velocity.y);
		Flip(false);
		m_Animator.SetBool("Move", true);
	}
	public void Stop()
	{
		m_Rb.velocity = new Vector2(0, m_Rb.velocity.y);
		m_Animator.SetBool("Move", false);
	}
	public void Jump()
	{
		m_Animator.SetBool("Jump", true);
		if (IsGrounded())
		{
			m_Rb.velocity = Vector3.up * m_JumpVelocity;
		}
	}

	void UpdateFootColliderState()
	{
		if (!IsGrounded())
		{
			if (m_Rb.velocity.y > 0.0f)
				m_PlayerFootCollider.enabled = false;
			else
				m_PlayerFootCollider.enabled = true;
		}
	}
	void Flip(bool faceRight)
	{
		if (m_FaceRight !=  faceRight)
		{
			m_FaceRight = faceRight;

			Vector3 scale = transform.localScale;
			scale.x *= -1;
			transform.localScale = scale;
		}
	}
	#endregion

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

	#region Position Getter and Setter
	public Vector2 GetPosition()
	{
		return transform.position;	
	}
	public void SetPosition(Vector2 position)
	{
		transform.position = position;
	}
	#endregion

	public void OnPlatform(bool OnPlatform, Transform parent = null)
    {
		transform.SetParent(OnPlatform ? parent : null);
	}
	private bool IsGrounded()
	{
		if (!m_PlayerFootCollider.enabled)
			return false;

		Collider2D groundCollider = Physics2D.OverlapArea(m_HitAreaA.position, m_HitAreaB.position, m_PlatformLayerMask);
		if(groundCollider != null)
		{
			m_Animator.SetBool("Jump", false);
		}
		return groundCollider != null;
	}
	public void CheckEnemyToKill()
	{
		Collider2D enemyToKill = Physics2D.OverlapArea(m_HitAreaA.position, m_HitAreaB.position, m_HitLayerMask);

		if (enemyToKill != null)
		{
			Destroy(enemyToKill.gameObject);
		}
	}

	#region Gizmo Drawing
	private void OnDrawGizmos()
	{
		Vector3 p00 = m_HitAreaA.position;
		Vector3 p11 = m_HitAreaB.position;
		Vector3 p10 = new Vector3(p11.x, p00.y, p00.z);
		Vector3 p01 = new Vector3(p00.x, p11.y, p00.z);

		Gizmos.color = Color.red;
		Gizmos.DrawLine(p00, p10);
		Gizmos.DrawLine(p10, p11);
		Gizmos.DrawLine(p11, p01);
		Gizmos.DrawLine(p01, p00);
	}
	#endregion
}
