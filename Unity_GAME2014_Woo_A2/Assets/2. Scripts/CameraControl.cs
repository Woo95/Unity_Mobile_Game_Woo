using UnityEngine;

public class CameraControl : MonoBehaviour
{
	#region singleton
	public static CameraControl instance;
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

	Transform m_TargetTrans; // player
	public float m_SmoothSpeed = 5.0f;
	public Vector3 m_Offset;

	Transform m_CameraTrans;
	float m_CameraFixedZPos;
	public void Init()
	{
		m_CameraTrans = transform;
		m_CameraFixedZPos = m_CameraTrans.position.z;

		TargetPlayer();
	}

	public void TargetPlayer()
	{
		GameObject playerObject = GameObject.Find("Player");
		if (playerObject != null)
		{
			m_TargetTrans = playerObject.transform;
			m_CameraTrans.position = m_TargetTrans.position;
		}
	}

	public void TrackPlayer()
	{
		if (m_TargetTrans == null)
		{
			TargetPlayer();
			return;
		}

		// calculate the desired camera position with a fixed Z value
		Vector3 desiredPosition = new Vector3(m_TargetTrans.position.x + m_Offset.x, m_TargetTrans.position.y + m_Offset.y, m_CameraFixedZPos);

		// update camera position while smoothly moving the camera towards the desired position
		m_CameraTrans.position = Vector3.Lerp(m_CameraTrans.position, desiredPosition, m_SmoothSpeed * Time.deltaTime);
	}
}