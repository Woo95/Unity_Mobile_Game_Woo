/*
FileName: SoundManager.cs
Name: Chaewan Woo
Student #101354291
Last Modified: Sept 30

About Script:
This is a sound manager.

Revision History:
- Added Sound Manager to the game.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Sound
{
	public string name;
	public AudioClip clip;
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

	public Sound[] m_BGM, m_SFX;
	public AudioSource m_BGMSource, m_SFXSource;

	public void PlayBGM(string name, float volume = 1.0f)
	{
		// Find the Sound object with the matching name
		Sound bgmToPlay = System.Array.Find(m_BGM, sound => sound.name == name);

		if (bgmToPlay.clip != null)
		{
			// Set the AudioClip of the AudioSource to the matched BGM
			m_BGMSource.clip = bgmToPlay.clip;
			// Adjust volume
			m_BGMSource.volume = volume;
			// Play the BGM
			m_BGMSource.Play();
		}
		else
		{
			Debug.LogWarning("BGM not found: " + name);
		}
	}
	public void StopBGM()
	{
		if (m_BGMSource != null) 
			m_BGMSource.Stop();
	}

	public void PlaySFX(string name, float volume = 1.0f)
	{
		// Find the Sound object with the matching name
		Sound sfxToPlay = System.Array.Find(m_SFX, sound => sound.name == name);

		if (sfxToPlay.clip != null)
		{
			AudioSource.PlayClipAtPoint(sfxToPlay.clip, Vector3.zero, volume);
		}
		else
		{
			Debug.LogWarning("SFX not found: " + name);
		}
	}
}
