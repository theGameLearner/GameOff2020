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

public class TurretHandler : MonoBehaviour
{
    public int bulletIndexInPool;
    public float bulletLife;

    public Transform targetTransform;
    public Transform bulletPrefab;
    public List<Transform> bulletStartPosList;
    public EnumTurret turretType;

    private float chargeMax = 2;
    private float currCharge;

    private static int bulletCount;
    
    void Start()
    {
        if(targetTransform == null)
		{
            targetTransform = GameSettings.instance.playerTransform;
        }
        if(bulletPrefab == null)
		{
            bulletPrefab = GameSettings.instance.bulletPrefab;
        }
        currCharge = 0;
        bulletLife = GameSettings.instance.bulletLifeTime;
        bulletIndexInPool = GameSettings.instance.bulletPoolIndex;
    }

    void Update()
    {
        if(targetTransform == null)
		{
            return;
		}

        transform.LookAt(targetTransform);
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

    private void ShootSingleBullet(Transform bulletStartPos )
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
}
