using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKamikaze : MonoBehaviour
{
    #region Fields
    [Header("Settings")]
    public int speed = 5;
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
            // Fa�a o objeto olhar para o jogador
            transform.LookAt(jogador.transform);

            // Mantenha a rota��o X e Z em 0
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        else
        {
            Debug.LogWarning("Jogador n�o encontrado com a tag " + "Player");
        }
    }
    #endregion
}
