/*
 * Copyright (c) The Game Learner
 * https://connect.unity.com/u/rishabh-jain-1-1-1
 * https://www.linkedin.com/in/rishabh-jain-266081b7/
 * 
 * created on - #CREATIONDATE#
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
	TrailRenderer tRenderer;

	Rigidbody rigidbody;

	[SerializeField] int muzzleFlashVfxIndex;
	[SerializeField] int HitVfxIndex;
	const float faddeOutTime = 0.1f;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		tRenderer = transform.GetComponent<TrailRenderer>();
		rigidbody = GetComponent<Rigidbody>();
	}
	private void Start()
	{

		if (tRenderer != null)
		{
			tRenderer.Clear();
			tRenderer.time = faddeOutTime;
		}
	}

	private void OnEnable()
	{
		if(rigidbody.velocity!=Vector3.zero){
			GameObject muzzleFlashGo = ObjectPool.instance.GetPooledObject(muzzleFlashVfxIndex);
			muzzleFlashGo.transform.position = transform.position;
			muzzleFlashGo.SetActive(true);
		}
		if (tRenderer != null)
		{
			tRenderer.Clear();
		}
	}

	private void OnDisable()
	{
		if(rigidbody.velocity!=Vector3.zero){
			GameObject muzzleFlashGo = ObjectPool.instance.GetPooledObject(muzzleFlashVfxIndex);
			muzzleFlashGo.transform.position = transform.position;
			muzzleFlashGo.SetActive(true);
		}
		if (tRenderer != null)
		{
			tRenderer.Clear();
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		//Debug.Log("bullet collided with " + collision.transform.name);
		GameObject hitVfxGo = ObjectPool.instance.GetPooledObject(HitVfxIndex);
		hitVfxGo.transform.position = transform.position;
		hitVfxGo.transform.SetParent(null,true);
		hitVfxGo.SetActive(true);
		
		if(collision.transform == GameSettings.instance.playerTransform)
		{
			Debug.Log("Hit with player, need to call Game Over");
		}	

		ReturnList.instance.ReturnIfExisting(gameObject);
	}
}