using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

public class ShootController : MonoBehaviour
{
	#region Fields
	[Header("Settings")]
	[SerializeField] private float shootInterval;
	[SerializeField] private bool shootWithKey;
	[SerializeField] private bool canHoldShootKey;
	[Header("References")]
	[SerializeField] private Transform spawnPosition;
	[SerializeField] private GameObject bulletPrefab;

	private float shootCooldown = 0;
	private bool canShoot = true;

	#endregion

	#region Properties
	#endregion

	#region Unity Message
	private void Awake()
	{
		shootCooldown = shootInterval;
	}

	private void Update()
	{
		// only count cooldown if you cant shoot
		if (!canShoot)
		{
			shootCooldown -= Time.deltaTime;
			if (shootCooldown <= 0)
			{
				canShoot = true;
				shootCooldown = shootInterval;
			}
		}

		if (shootWithKey)
		{
			// esse Fire1 fica definido lá no Input Manager da unity,
			// igual os quando usa Horizontal e Vertical
			if (canHoldShootKey && Input.GetButton("Fire1"))
				Shoot();
			else if (Input.GetButtonDown("Fire1"))
				Shoot();
		}
		else
			Shoot();
	}
	#endregion

	#region Public Methods
	public void Shoot()
	{
		// do not shoot if you cant
		if (!canShoot) return;

		// find and spawn bullet
		Instantiate(bulletPrefab, spawnPosition.position, spawnPosition.rotation);
		canShoot = false;
	}


	#endregion

	#region Private Methods
	#endregion
}
