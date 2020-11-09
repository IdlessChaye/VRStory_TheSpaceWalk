using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdlessChaye.VRStory
{
	[RequireComponent(typeof(Rigidbody))]
	public class AddForceOnStart : MonoBehaviour
	{
		public Vector3 force;
		public Vector3 torque;

		private Rigidbody _rigidbody;

		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody>();
		}

		private void Start()
		{
			_rigidbody.AddForce(force);
			_rigidbody.AddTorque(torque);
		}
	}
}