using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeCounterManager : MonoBehaviour
{
	int currentLifeNumber;
	int maxLifeNumber;

	Image lifeCounterUI;

	void Start()
	{
		lifeCounterUI = GetComponent<Image>();

		maxLifeNumber = 4;
		currentLifeNumber = maxLifeNumber;
	}

	public void LoseLife()
	{
		currentLifeNumber--;

		if (currentLifeNumber <= 0)
		{
			//game over
			SceneManager.LoadScene(1);
		}
		lifeCounterUI.sprite = Resources.Load<Sprite>($"Sprites/LifeCounter/hud-{currentLifeNumber}");
	}
}
