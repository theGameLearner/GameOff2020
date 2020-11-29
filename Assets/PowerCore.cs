using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCore : MonoBehaviour,IGridObject,IDestroyableEnemy
{

    #region Variables
        [SerializeField] GameObject renderer;
        BoxCollider boxCollider;        
    #endregion
    #region IgridObject
		
		int index;
		int x;
		int y;

		public int GetIndex()
		{
			return index;
		}

		public string GetJsonData()
		{
			return "";
		}

		public void GetXY(out int x, out int y)
		{
			x = this.x;
			y = this.y;
		}

		public void Initialize(string jsonData)
		{
			
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

	#endregion

    #region monobehaviour

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            boxCollider = GetComponent<BoxCollider>();
        }
        /// <summary>
        /// OnCollisionEnter is called when this collider/rigidbody has begun
        /// touching another rigidbody/collider.
        /// </summary>
        /// <param name="other">The Collision data associated with this collision.</param>
        void OnCollisionEnter(Collision collision)
        {
            if (collision.transform == GameSettings.instance.playerTransform)
            {
                renderer.SetActive(false);
                boxCollider.enabled = false;
                GameManager.instance.EnemyDestroyed();
                GameObject explosionGo = ObjectPool.instance.GetPooledObject(GameSettings.instance.TurretExplosionVfxPoolIndex);
                explosionGo.SetActive(true);
                explosionGo.transform.position = transform.position;
                GameManager.instance.EnemyDestroyed();
            }
        }
    #endregion
}
