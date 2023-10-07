using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStationary : MonoBehaviour
{
	#region Fields
	public float minDistanceToFollow = 30;
	public float smoothFollow = 30;

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
	#endregion

	#region Public Methods
	void Update()
    {
        lookAtPlayer();
    }
    #endregion

    #region Private Methods
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
    #endregion
}
