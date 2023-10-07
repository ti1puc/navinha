using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	#region Fields
	[Header("Movement")]
	[SerializeField] private float moveSpeed;
	[SerializeField] private float rotationSpeed;
	[SerializeField] private Vector2 minMaxRangeHorizontal;
	[SerializeField] private Vector2 minMaxRangeVertical;
	[Header("Collision")]
	[SerializeField] private int invencibilityFrames = 10;
	[Header("References")]
	[SerializeField] private HealthController healthController;

	private Vector3 initialPosition;
	private bool hasTookDamage;
	#endregion

	#region Unity Messages
	private void Awake()
	{
		initialPosition = transform.position;
	}

	private void Update()
	{
		// horizontal calculations
		float horizontal = Input.GetAxis("Horizontal");
		float yRot = horizontal * Time.deltaTime * rotationSpeed;

		// vertical calculations
		float vertical = Input.GetAxis("Vertical");
		float zPos = vertical * Time.deltaTime * moveSpeed;

		// go forward
		transform.position += transform.forward * zPos;

		// rotate according to horizontal axis
		transform.Rotate(0, yRot, 0);

		// clamp position
		float clampedX = Mathf.Clamp(transform.position.x, minMaxRangeHorizontal.x, minMaxRangeHorizontal.y);
		float clampedZ = Mathf.Clamp(transform.position.z, minMaxRangeVertical.x, minMaxRangeVertical.y);
		transform.position = new Vector3(clampedX, initialPosition.y, clampedZ);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (hasTookDamage)
		{
			StartCoroutine(DoAfterXFrames(invencibilityFrames, ResetTookDamage));
			return;
		}

		if (other.CompareTag("Bullet") || other.CompareTag("Enemy"))
		{
			healthController.TakeDamage(1, GameManager.Instance.Defeat);
			hasTookDamage = true;
		}

		if (other.CompareTag("Kamikaze"))
		{
			healthController.TakeDamage(30, GameManager.Instance.Defeat);
			hasTookDamage = true;
		}
	}
	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	private void ResetTookDamage()
	{
		hasTookDamage = false;
	}

	private IEnumerator DoAfterXFrames(int frames, Action action = null)
	{
		for (int i = 0; i < frames; i++)
		{
			yield return null;
		}
		action?.Invoke();
	}
	#endregion
}
