using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdlessChaye.VRStory
{
	public class PlayerHomeController : MonoBehaviour
	{
		private Transform _transform;
		private Camera _cam;
		private Vector2 sc_center;
		private void Awake()
		{
			_transform = this.transform;
			_cam = Camera.main;
			sc_center = new Vector2(Screen.width / 2f, Screen.height / 2f);
		}
		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				Ray ray = _cam.ScreenPointToRay(sc_center);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit, 100f))
				{
					// 如果射线与平面碰撞，打印碰撞物体信息  
					Debug.Log("碰撞对象: " + hit.collider.name);
					// 在场景视图中绘制射线  
					Debug.DrawLine(ray.origin, hit.point, Color.red);

					var go = hit.collider.gameObject;
					if (go.CompareTag("Book"))
					{
						go.GetComponent<Rigidbody>().AddForce(Random.insideUnitSphere * 500f);
						PlayerData.I.ReadBookCount++;
					}
				}
			}
		}
	}
}