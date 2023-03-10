using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.FPS.Game
{
	public class ObjectPool : MonoBehaviour
	{
		private Queue<ProjectileBase> pool = new Queue<ProjectileBase>();
		[SerializeField]
		private int size = 10;

		public ProjectileBase prefab = null;

		private void Awake()
		{
			for (int i = 0; i < size; ++i)
			{
				ProjectileBase projectile = Instantiate(prefab);
				projectile.gameObject.SetActive(false);
				pool.Enqueue(projectile.GetComponent<ProjectileBase>());
			}
		}

		private void OnValidate()
		{
			if (prefab == null)
			{
				prefab = GetComponent<WeaponController>().ProjectilePrefab;
			}
		}

		public ProjectileBase GetObject()
		{
			if(pool.Count > 0)
			{
				ProjectileBase proj = pool.Dequeue();
				proj.gameObject.SetActive(true);
				return proj;
			}

			ProjectileBase p = Instantiate(prefab);
			return p;

		}

		public void ReturnObject(ProjectileBase proj)
		{
			proj.gameObject.SetActive(false);
			pool.Enqueue(proj);
		}
	}
}