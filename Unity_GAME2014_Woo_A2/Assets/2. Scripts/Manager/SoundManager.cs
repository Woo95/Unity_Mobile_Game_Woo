using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SFX
{
	public string name;
	public AudioClip clip;
}

[System.Serializable]
public struct BGM
{
	public string name;
	public AudioSource source;
}

public class SoundManager : MonoBehaviour
{
	#region singletone
	public static SoundManager instance;
	public void Awake()
	{
		if (instance == null)
			instance = this;
		else
		{
			Destroy(gameObject);
			return;
		}
	}
	#endregion

	public SFX[] m_SFX;
	//public BGM m_OverWorldBGM, m_BattleBGM;
	public BGM[] m_BGM;
	public List<BGM> m_ActiveBGMList = new List<BGM>();

	public void Init()
	{
		m_ActiveBGMList.Clear();

		PlayBGM("GamePlayBGM");
	}

	#region BGM
	public void PlayBGM(string name, float volume = 0.10f, bool isLoop = true)
	{
		// find the BGM object with the matching name
		BGM bgm = System.Array.Find(m_BGM, b => b.name == name);

		if (bgm.source != null)
		{
			bgm.source.Play();
			bgm.source.volume = volume;
			bgm.source.loop = isLoop;
		}
		m_ActiveBGMList.Add(bgm);
	}
	public void StopBGM(string name)
	{
		// find the BGM object with the matching name
		BGM bgm = System.Array.Find(m_BGM, b => b.name == name);

		if (bgm.source != null)
		{
			bgm.source.Stop();
		}
		m_ActiveBGMList.Remove(bgm);
	}
	public void PauseBGM()
	{
		for (int i = 0; i < m_ActiveBGMList.Count; i++)
		{
			BGM bgm = m_ActiveBGMList[i];
			if (bgm.source != null)
			{
				bgm.source.Pause();
			}
		}
	}
	public void UnPauseBGM()
	{
		for (int i = 0; i < m_ActiveBGMList.Count; i++)
		{
			BGM bgm = m_ActiveBGMList[i];
			if (bgm.source != null)
			{
				bgm.source.UnPause();
			}
		}
	}
	#endregion

	#region SFX
	public void PlaySFX(string name, float volume = 1.0f)
	{
		// Find the Sound object with the matching name
		SFX sfxToPlay = System.Array.Find(m_SFX, sound => sound.name == name);

		if (sfxToPlay.clip != null)
		{
			AudioSource.PlayClipAtPoint(sfxToPlay.clip, Vector3.zero, volume);
		}
		else
		{
			Debug.LogWarning("SFX not found: " + name);
		}
	}
	#endregion
}