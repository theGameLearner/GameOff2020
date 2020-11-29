using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[RequireComponent(typeof(Collider))]
public class WallObject : MonoBehaviour, IGridObject
{

    #region Variables
    public bool canBeDestroyed;
    public Transform wallRenderTrans;
        int index;
        int x;
        int y;
    Collider myCollider;
    #endregion
    #region Interface
    public int GetIndex()
        {
            return index;
        }

        public string GetJsonData()
        {
            WallData data = new WallData();
            return JsonConvert.SerializeObject(data);
        }

        public void GetXY(out int x, out int y)
        {
            x = this.x;
            y = this.y;
        }

        public void Initialize(string jsonData)
        {
            WallData data = JsonConvert.DeserializeObject<WallData>(jsonData);
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

	private void Start()
	{
        myCollider = GetComponent<Collider>();
    }

	private void OnCollisionEnter(Collision collision)
	{
		if(canBeDestroyed && collision.transform == GameSettings.instance.playerTransform)
		{
            if(wallRenderTrans != null)
			{
                wallRenderTrans.gameObject.SetActive(false);
			}
            myCollider.enabled = false;

            GameObject explosionGo = ObjectPool.instance.GetPooledObject(GameSettings.instance.DestructibleWallVfxPoolIndex);
            explosionGo.SetActive(true);
            explosionGo.transform.position = transform.position;

        }
	}
}

[System.Serializable]
public class WallData{
}

