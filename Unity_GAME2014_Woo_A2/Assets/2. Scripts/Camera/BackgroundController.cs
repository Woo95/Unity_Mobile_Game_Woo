using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BackgroundInfo
{
    public Transform m_Background;
	[HideInInspector] public float m_Percent;
}

public class BackgroundController : MonoBehaviour
{
	#region singleton
	public static BackgroundController instance;
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

	Transform m_CameraTrans;
	float m_DepthZ;

	public List<BackgroundInfo> m_BackgroundList = new List<BackgroundInfo>();

	public void Init()
	{
		m_CameraTrans = CameraControl.instance.transform;
		m_DepthZ = transform.position.z;

		for (int i=0; i< m_BackgroundList.Count; i++)
		{
			m_BackgroundList[i].m_Percent = i * 0.03f;
		}
	}

	public void TrackBackGround()
	{
		if (m_CameraTrans == null)
			return;

		// update background positions based on camera and percentage values
		foreach (BackgroundInfo backgroundInfo in m_BackgroundList)
		{
			Vector3 newPosition = m_CameraTrans.position + new Vector3(-m_CameraTrans.position.x * backgroundInfo.m_Percent, 0, m_DepthZ);

			backgroundInfo.m_Background.position = newPosition;
		}
	}
}
