using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKamikaze : MonoBehaviour
{
    #region Fields
    public float speed = 5;
    public float minDistanceToFollow = 30;
	public float smoothFollow = 30;
	[Header("References")]
	[SerializeField] private HealthController healthController;
	[SerializeField] private PowerupDrop powerupDrop;

	private GameObject jogador;
	#endregion

	#region Properties
	#endregion

	#region Unity Messages
	private void Awake()
	{
		// Encontre o jogador pelo nome da tag no Awake
        // ficar procurando todo frame no update é custoso pra CPU
		jogador = GameObject.FindWithTag("Player");
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("PlayerBullet"))
		{
			healthController.TakeDamage(1, DestroyEnemy);
		}

		if (other.CompareTag("Enemy"))
		{
			healthController.TakeDamage(30, DestroyEnemy);
		}
	}
	#endregion

	#region Public Methods
	void Update()
    {
        lookAtPlayer();
    }
	#endregion

	#region Private Methods
	private void DestroyEnemy()
	{
		GameManager.IncreaseScore();
		powerupDrop.TrySpawnPowerup();
		Destroy(gameObject);
	}

	private void lookAtPlayer()
    {
        if (jogador != null && Vector3.Distance(transform.position, jogador.transform.position) <= minDistanceToFollow)
        {
			// Faça o objeto olhar para o jogador
			Vector3 targetDirection = (jogador.transform.position - transform.position).normalized;
			Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * smoothFollow);

			// Mantenha a rotação X e Z em 0
			transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        else
        {
            Debug.LogWarning("Jogador não encontrado com a tag " + "Player");
        }
    }
    #endregion
}
