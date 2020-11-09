using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdlessChaye.VRStory
{
	public class PlayerMenuController : MonoBehaviour
	{
		public float rotateSpeed;


		private Animator _ani;
		private Transform _transform;
		private Vector3 _rotateAxis;

		private void Awake()
		{
			_ani = GetComponent<Animator>();
			_transform = this.transform;
		}

		private void Start()
		{
			_ani.SetInteger("AnimationPar", 1);
			_rotateAxis = Vector3.up;
				//Random.insideUnitSphere;
		}

		private void Update()
		{
			_transform.Rotate(_rotateAxis, rotateSpeed * Time.deltaTime);
		}
	}
}