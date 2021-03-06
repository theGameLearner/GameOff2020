﻿/*
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
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class TurretHandler : MonoBehaviour, IGridObject, IDestroyableEnemy
{
	public int bulletIndexInPool;
	public float bulletLife;

	public Image CircleIndicator;
	public Transform targetTransform;
	public Transform bulletPrefab;
	public List<Transform> bulletStartPosList;
	public EnumTurret turretType;
	public Transform turretTrans;

	private Collider myCollider;
	private bool canFire = true;
	public float chargeMax = 2;

	public float turnRate = 1;
	private float currCharge;
	private Vector3 lookAt;
	private Vector3 lookDirection;

	Quaternion lookRotation;

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

		lookAt = targetTransform.position;
		lookAt.y = turretTrans.position.y;
		// lookDirection = (lookAt - turretTrans.position).normalized;
		// lookRotation = Quaternion.LookRotation(lookDirection);
		// turretTrans.rotation = Quaternion.Lerp(transform.rotation,lookRotation,turnRate*Time.unscaledDeltaTime);

		turretTrans.LookAt(lookAt);
		
		currCharge += Time.deltaTime;
		CircleIndicator.fillAmount = currCharge/chargeMax;

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
		bullet.transform.rotation *= Quaternion.FromToRotation(Vector3.forward,bulletStartPos.forward);
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
		GameObject explosionGo = ObjectPool.instance.GetPooledObject(GameSettings.instance.TurretExplosionVfxPoolIndex);
		explosionGo.SetActive(true);
		explosionGo.transform.position = turretTrans.position;
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
