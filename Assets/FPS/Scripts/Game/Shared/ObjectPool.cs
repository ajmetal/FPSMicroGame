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
				AddObject();
			}
		}

		private void OnValidate()
		{
			if (prefab == null)
			{
				prefab = GetComponent<WeaponController>().ProjectilePrefab;
			}
		}

		private void AddObject()
		{
			ProjectileBase projectile = Instantiate(prefab);
			projectile.pool = this;
			projectile.gameObject.SetActive(false);
			pool.Enqueue(projectile.GetComponent<ProjectileBase>());
		}

		public ProjectileBase GetObject()
		{
			ProjectileBase projectile = null;
			if (pool.Count > 0)
			{
				projectile = pool.Dequeue();
			}
			else
			{
				AddObject();
				projectile = pool.Dequeue();
			}

			projectile.gameObject.SetActive(true);
			return projectile;

		}

		public void ReturnObject(ProjectileBase proj)
		{
			proj.gameObject.SetActive(false);
			pool.Enqueue(proj);
		}
	}
}