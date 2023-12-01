using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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

	Rigidbody2D m_Rb;
	public float m_MoveSpeed = 2.5f;
	public float m_JumpForce = 5.0f;

	private bool m_IsMoveLeft = false;
	private bool m_IsMoveRight = false;
	private bool m_IsJump = false;
	public enum eJumpState { OnPlatform, Jump };
	public eJumpState m_JumpState;
	public void Init()
	{
		m_Rb = GetComponent<Rigidbody2D>();
		m_JumpState = eJumpState.OnPlatform;
}
	public void Move()
	{
		if (m_IsMoveLeft)
		{
			MoveLeft();
		}
		if (m_IsMoveRight)
		{
			MoveRight();
		}

		if (m_IsJump)
		{
			Jump();
		}
	}
	public void MoveWithKeyboard()	// For test purposes
	{
		if (Input.GetKey(KeyCode.A))
		{
			MoveLeft();
		}
		if (Input.GetKey(KeyCode.D))
		{
			MoveRight();
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
		transform.position += Vector3.left * m_MoveSpeed * Time.deltaTime;
	}
    public void MoveRight()
    {
		transform.position += Vector3.right * m_MoveSpeed * Time.deltaTime;
	}
    public void Jump()
    {
		if (m_JumpState == eJumpState.OnPlatform)
		{
			m_Rb.AddForce(Vector3.up * m_JumpForce, ForceMode2D.Impulse);
			m_JumpState = eJumpState.Jump;
		}
	}
	#endregion

	public void OnPlatform(bool OnPlatform, Transform parent = null)
    {
		transform.SetParent(OnPlatform ? parent : null);
    }

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.collider.CompareTag("Platform"))
		{
			m_JumpState = eJumpState.OnPlatform;
		}
	}
}
