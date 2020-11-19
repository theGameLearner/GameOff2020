/*
 * Copyright (c) The Game Learner
 * https://connect.unity.com/u/rishabh-jain-1-1-1
 * https://www.linkedin.com/in/rishabh-jain-266081b7/
 * 
 * created on - #CREATIONDATE#
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[RequireComponent(typeof(Collider))]
public class TurretHandler : MonoBehaviour, IGridObject, IDestroyableEnemy
{
	public int bulletIndexInPool;
	public float bulletLife;

	public Transform targetTransform;
	public Transform bulletPrefab;
	public List<Transform> bulletStartPosList;
	public EnumTurret turretType;
	public Transform turretTrans;

	private Collider myCollider;
	private bool canFire = true;
	private float chargeMax = 2;
	private float currCharge;

	private static int bulletCount;

	int index;
	int x;
	int y;

	void Start()
	{
		if (targetTransform == null)
		{
			targetTransform = GameSettings.instance.playerTransform;
		}
		if (bulletPrefab == null)
		{
			bulletPrefab = GameSettings.instance.bulletPrefab;
		}
		currCharge = 0;
		bulletLife = GameSettings.instance.bulletLifeTime;
		bulletIndexInPool = GameSettings.instance.bulletPoolIndex;
		myCollider = GetComponent<Collider>();
	}

	void Update()
	{
		if (GameManager.instance.CurrGameState == GameStates.LevelEditor || targetTransform == null || !canFire) 
		{
			currCharge = 0;
			return;
		}

		turretTrans.LookAt(targetTransform);
		currCharge += Time.deltaTime;

		if (currCharge > chargeMax)
		{
			currCharge = 0;
			switch (turretType)
			{
				case EnumTurret.singleBullet:
					ShootSingleBullet(bulletStartPosList[0]);
					break;
				case EnumTurret.tripleBullet:
					ShootThreeBullets();
					break;
				default:
					Debug.LogError("need to handle the case for this turret");
					break;
			}
		}
	}

	private void ShootThreeBullets()
	{
		ShootSingleBullet(bulletStartPosList[0]);
		ShootSingleBullet(bulletStartPosList[1]);
		ShootSingleBullet(bulletStartPosList[2]);
	}

	private void ShootSingleBullet(Transform bulletStartPos)
	{
		GameObject bullet = ObjectPool.instance.GetPooledObject(bulletIndexInPool);
		bullet.transform.position = bulletStartPos.position;
		bullet.transform.rotation = Quaternion.identity;
		Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
		bullet.name = "bullet" + bulletCount++;
		bullet.SetActive(true);
		ReturnList.instance.AddToReturnList(bullet, bulletIndexInPool, bulletLife);
		if (bulletRB == null)
		{
			Debug.LogError("Bullet must have a rigidbody assigned");
			return;
		}

		bulletRB.velocity = bulletStartPos.forward * GameSettings.instance.bulletSpeed;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.transform == GameSettings.instance.playerTransform)
		{
			KillTurret();
		}
	}

	private void KillTurret()
	{
		turretTrans.gameObject.SetActive(false);
		myCollider.enabled = false;
		canFire = false;
		GameManager.instance.EnemyDestroyed();
	}

	public void Revive()
	{
		turretTrans.gameObject.SetActive(true);
		myCollider.enabled = true;
		canFire = true;
	}

	public int GetIndex()
	{
		return index;
	}

	public string GetJsonData()
	{
		TurretSaveData data = new TurretSaveData();
		data.enumTurret = turretType;

		return JsonConvert.SerializeObject(data);
	}

	public void GetXY(out int x, out int y)
	{
		x = this.x;
		y = this.y;
	}

	public void Initialize(string jsonData)
	{
		TurretSaveData data = JsonConvert.DeserializeObject<TurretSaveData>(jsonData);
		turretType = data.enumTurret;
	}

	public void SetIndex(int index)
	{
		this.index = index;
	}

	public void SetXY(int x, int y)
	{
		this.x = x;
		this.y = y;
	}
}

public struct TurretSaveData
{
	public EnumTurret enumTurret;
}
