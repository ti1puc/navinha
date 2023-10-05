using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStationary : MonoBehaviour
{
    #region Fields
    //[Header("Settings")]
    //[Header("References")]

    //[Header("Debug")]
    #endregion

    #region Properties
    #endregion

    #region Unity Messages
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

        if (jogador != null)
        {
            // Faça o objeto olhar para o jogador
            transform.LookAt(jogador.transform);

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
