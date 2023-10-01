using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	#region Fields
	[Header("Settings")]
	[SerializeField] private float moveSpeed;
	[SerializeField] private float maxDistance = 50;
	//[Header("References")]

	private float distanceFromSpawn;
	private Vector3 initialPosition;
	#endregion

	#region Properties
	#endregion

	#region Unity Messages
	private void OnEnable()
	{
		// hold and update initial position every time bullet obj is enabled
		initialPosition = transform.position;
	}

	private void Update()
	{
		float zPos = moveSpeed * Time.deltaTime;
		transform.Translate(0, 0, zPos);

		// if bullet goes too far from obj destroy it
		distanceFromSpawn = Vector3.Distance(initialPosition, transform.position);
		if (distanceFromSpawn > maxDistance)
			Destroy(gameObject);
	}
	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	#endregion
}
