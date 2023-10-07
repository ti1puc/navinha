using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyFinder : MonoBehaviour
{
	#region Fields
	[Header("Settings")]
	[SerializeField] private float rotateSpeed = 360f;
	[SerializeField] private float minDistanceToHide = 10;
	[Header("References")]
	[SerializeField] private Transform player;
	[SerializeField] private GameObject arrow;
	[SerializeField] private EnemyManager enemyManager;
	//[Header("Debug")]

	private Transform closestEnemy;
	private Vector3 direction;
	private float angle;
	private Vector3 rotate;
	private float distance;
	#endregion

	#region Properties
	#endregion

	#region Unity Messages
	private void Update()
	{
		// get closest enemy from EnemyManager
		closestEnemy = enemyManager.GetClosestEnemy(player.position);
		if (closestEnemy == null) return;

		distance = Vector3.Distance(player.position, closestEnemy.position);
		bool shouldHide = distance <= minDistanceToHide || closestEnemy == null; // hide if too close or no enemy
		arrow.SetActive(!shouldHide);

		// no need to rotate if is hided
		if (shouldHide == false)
			RotateTowardsEnemy();
	}

	private void OnDrawGizmos()
	{
		if (closestEnemy == null)
			return;

		// desenhando uma linha até o inimig mais proximo pra debugar
		Gizmos.color = Color.red;
		Gizmos.DrawLine(player.position, closestEnemy.position);
	}
	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	private void RotateTowardsEnemy()
	{
		// determine which direction to rotate towards
		direction = closestEnemy.position - player.position;

		// returns the angle between -180 and 180.
		angle = Vector3.SignedAngle(player.forward, direction, Vector3.up);
		if (angle < 0)
			angle = 360 - angle * -1;

		// calculate a rotation vector
		rotate = new Vector3(0, 0, 360 - angle);

		// apply rotation
		transform.localRotation = Quaternion.Euler(rotate);
	}
	#endregion
}
