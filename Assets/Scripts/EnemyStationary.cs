using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStationary : MonoBehaviour
{
	#region Fields
	public float minDistanceToFollow = 30;
	public float smoothFollow = 30;
	[SerializeField, Range(0f, 1f)] private float powerupChance = .35f;
    [Header("References")]
	[SerializeField] private HealthController healthController;
    public GameObject PowerUp;

	private GameObject jogador;
	private bool hasDestroyed;
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
			Destroy(other.gameObject);
		}

		if (other.CompareTag("Kamikaze"))
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
		if (hasDestroyed) return;

		hasDestroyed = true;
		GameManager.IncreaseScore();
		spawnPUP();
		Destroy(gameObject);
    }

    private void lookAtPlayer()
    {
        // Encontre o jogador pelo nome da tag
        GameObject jogador = GameObject.FindWithTag("Player");

        if (jogador != null && Vector3.Distance(transform.position, jogador.transform.position) <= minDistanceToFollow)
        {
			// Faça o objeto olhar para o jogador
			Vector3 targetDirection = (jogador.transform.position - transform.position).normalized;
			Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * smoothFollow);

			// Mantenha a rotação X e Z em 0
			transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
        else
        {
            Debug.LogWarning("Jogador não encontrado com a tag " + "Player");
        }
    }


    private void spawnPUP()
    {
        // Gere um número aleatório entre 0 e 1
        float randomValue = Random.value;

        // Se o número gerado for menor ou igual a 0.5 (50% de chance), retorne true, caso contrário, retorne false
        if (randomValue <= powerupChance) return;
        else Instantiate(PowerUp, gameObject.transform.position, Quaternion.identity);

    }
    #endregion
}
