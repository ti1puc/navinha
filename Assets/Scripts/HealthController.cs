using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
	#region Fields
	[SerializeField] private int maxHealth;
	public HealthBar healthBar;

	private int currentHealth;
	#endregion

	#region Properties
	public int CurrentHealth => currentHealth;
	#endregion

	#region Unity Messages
	private void Awake()
	{
		currentHealth = maxHealth;
		healthBar.ChangeMaxLife(maxHealth);
	}
	#endregion

	#region Public Methods
	public void Heal(int heal)
	{
		if (currentHealth < maxHealth)
		{
			currentHealth += heal;
			healthBar.ChangeLife(currentHealth);
		}

		// um fix pra caso a vida passar do m�ximo
		if (currentHealth > maxHealth)
			currentHealth = maxHealth;
	}

	public void TakeDamage(int damage, Action onDeath = null)
	{
		currentHealth -= damage;
		healthBar.ChangeLife(currentHealth);

		if (currentHealth <= 0)
			onDeath?.Invoke();
	}
	#endregion

	#region Private Methods
	#endregion
}
