using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region Fields
    //[Header("Settings")]
    //[Header("References")]
    //[Header("Debug")]

    private List<Transform> enemies = new List<Transform>();
	private Transform closestEnemy;
    #endregion

    #region Properties
	#endregion

	#region Unity Messages
	private void Awake()
	{
		foreach (Transform child in transform)
		{
			enemies.Add(child);
		}
	}
	#endregion

	#region Public Methods
	public Transform GetClosestEnemy(Vector3 comparePosition)
	{
		float closestDistance = int.MaxValue;

		for (int i = 0; i < enemies.Count; i++)
		{
			float distance = Vector3.Distance(comparePosition, enemies[i].transform.position);
			if (distance < closestDistance)
			{
				closestDistance = distance;
				closestEnemy = enemies[i];
			}
		}

		return closestEnemy;
	}
	#endregion

	#region Private Methods
	#endregion
}
