using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
	float currentHealth;
	float maxHealth;

	Slider slider;
	LifeCounterManager lifeCounterManager;

	[SerializeField] GameObject followObject;
	[SerializeField] Vector3 offset;

	void Start()
	{
		slider = GetComponentInChildren<Slider>();
		lifeCounterManager = FindObjectOfType<LifeCounterManager>();
		maxHealth = slider.maxValue;
		currentHealth = maxHealth;
	}

	public void TakeDamage(float damage)
	{
		currentHealth -= damage;
		if (currentHealth < 0)
		{
			//lose life
			lifeCounterManager.LoseLife();

			currentHealth = maxHealth;

		}
		UpdateHealthBar();
	}

	private void UpdateHealthBar()
	{
		slider.value = currentHealth;
	}

	public void ResetHealth()
	{
		currentHealth = maxHealth;
		UpdateHealthBar();
	}

	private void Update()
	{
		transform.position = followObject.transform.position + offset;
	}
}
