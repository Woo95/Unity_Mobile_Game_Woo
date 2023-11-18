using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SFX
{
	[SerializeField]
	public string name;
	[SerializeField]
	public AudioClip clip;
}

[System.Serializable]
public struct BGM
{
	[SerializeField]
	public string name;
	[SerializeField]
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

		PlayBGM("OverWorldBGM");
		StopBGM("BattleBGM");
	}

	#region BGM
	public void PlayBGM(string name, float volume = 0.15f, bool isLoop = true)
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
	#region Swap Scene BGM Handler
	public void SwapTrack(string CurrentBgmName, string NewBgmName, float newVloume = 0.15f)
	{
		BGM currentBGM = System.Array.Find(m_BGM, b => b.name == CurrentBgmName);
		BGM newBGM = System.Array.Find(m_BGM, b => b.name == NewBgmName);

		// start fading out the current track
		StartCoroutine(FadeTrack(currentBGM, currentBGM.source.volume, 0.0f));

		// start fade in for the new track
		StartCoroutine(FadeTrack(newBGM, 0.0f, newVloume, newBGM.source.clip));
	}

	IEnumerator FadeTrack(BGM bgm, float startVolume, float endVolume, AudioClip newClip = null)
	{
		float timer = 0f;
		float fadeDuration = 1.0f;

		if (newClip != null)
		{
			bgm.source.clip = newClip;
			bgm.source.volume = startVolume;   // new track sound start from startVolume(which is 0)
			bgm.source.Play();
			m_ActiveBGMList.Add(bgm);
		}

		while (timer < fadeDuration)
		{
			timer += Time.deltaTime;
			bgm.source.volume = Mathf.Lerp(startVolume, endVolume, timer / fadeDuration);
			yield return null;
		}

		bgm.source.volume = endVolume; // to avoid the bug of having volume very close to 0 instead of 0.

		if (endVolume == 0f)
		{
			bgm.source.Stop();
			m_ActiveBGMList.Remove(bgm);
		}
	}
	#endregion
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