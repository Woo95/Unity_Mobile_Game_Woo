using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeCounterManager : MonoBehaviour
{
	int currentLifeNumber;
	int maxLifeNumber;

	Image lifeCounterUI;

	public List<Sprite> images = new List<Sprite>();

	void Start()
	{
		lifeCounterUI = GetComponent<Image>();

		maxLifeNumber = 4;
		currentLifeNumber = maxLifeNumber;

		lifeCounterUI.sprite = images[currentLifeNumber - 1];
	}

	public void LoseLife()
	{
		currentLifeNumber--;

		if (currentLifeNumber <= 0)
		{
			//game over
			SceneManager.LoadScene("GameOver");
		}
		if (currentLifeNumber-1 >= 0)
			lifeCounterUI.sprite = images[currentLifeNumber - 1];
	}
}
