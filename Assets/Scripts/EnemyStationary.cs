using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStationary : MonoBehaviour
{
	#region Fields
	public float minDistanceToFollow = 30;
	public float smoothFollow = 30;
    public GameObject PowerUp;
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
		// ficar procurando todo frame no update � custoso pra CPU
		jogador = GameObject.FindWithTag("Player");
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("PlayerBullet"))
		{
			healthController.TakeDamage(1, DestroyEnemy);
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
		GameManager.IncreaseScore();
		powerupDrop.TrySpawnPowerup();
		Destroy(gameObject);
		spawnPUP();

    }

    private void lookAtPlayer()
    {
        // Encontre o jogador pelo nome da tag
        GameObject jogador = GameObject.FindWithTag("Player");

        if (jogador != null && Vector3.Distance(transform.position, jogador.transform.position) <= minDistanceToFollow)
        {
			// Fa�a o objeto olhar para o jogador
			Vector3 targetDirection = (jogador.transform.position - transform.position).normalized;
			Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * smoothFollow);

			// Mantenha a rota��o X e Z em 0
			transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
        else
        {
            Debug.LogWarning("Jogador n�o encontrado com a tag " + "Player");
        }
    }


    private void spawnPUP()
    {
        // Gere um n�mero aleat�rio entre 0 e 1
        float randomValue = Random.value;

        // Se o n�mero gerado for menor ou igual a 0.5 (50% de chance), retorne true, caso contr�rio, retorne false
        if (randomValue <= 0.35f) return;
        else Instantiate(PowerUp, gameObject.transform.position, Quaternion.identity);

    }
    #endregion
}
