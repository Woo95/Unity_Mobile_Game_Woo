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

	public Transform m_BottomLeft, m_TopRight;
	Vector2 p00, p11;

	public void Init()
	{
		m_CameraTrans = transform;
		m_CameraFixedZPos = m_CameraTrans.position.z;

		SetCameraBoundary();

		TargetPlayer();

		BackgroundController.instance.Init();
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

		UpdateCameraBoundary();

		BackgroundController.instance.TrackBackGround();
	}

	#region Camera Boundaries
	public void SetCameraBoundary()
	{
		Camera camera = Camera.main;

		float halfWidth = camera.orthographicSize * camera.aspect;
		float halfHeight = camera.orthographicSize;

		p00 = new Vector2(m_BottomLeft.position.x + halfWidth,	m_BottomLeft.position.y + halfHeight);
		p11 = new Vector2(m_TopRight.position.x - halfWidth,	m_TopRight.position.y - halfHeight);
	}
	public void UpdateCameraBoundary()
	{
		Vector3 pos = m_CameraTrans.position;

		if (pos.x < p00.x) pos.x = p00.x;
		if (pos.x > p11.x) pos.x = p11.x;
		if (pos.y < p00.y) pos.y = p00.y;
		if (pos.y > p11.y) pos.y = p11.y;

		m_CameraTrans.position = pos;
	}

	#region Gizmo Drawing
	private void OnDrawGizmos()
	{
		Vector2 p00 = new Vector2(m_BottomLeft.position.x, m_BottomLeft.position.y);
		Vector2 p01 = new Vector2(m_BottomLeft.position.x, m_TopRight.position.y);
		Vector2 p11 = new Vector2(m_TopRight.position.x, m_TopRight.position.y);
		Vector2 p10 = new Vector2(m_TopRight.position.x, m_BottomLeft.position.y);

		Gizmos.DrawLine(p00, p10);
		Gizmos.DrawLine(p10, p11);
		Gizmos.DrawLine(p11, p01);
		Gizmos.DrawLine(p01, p00);
	}
	#endregion
	#endregion
}