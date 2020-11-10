using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdlessChaye.VRStory
{
	public class PlayerSpaceController : MonoBehaviour
	{
		private static PlayerSpaceController _instance;
		public static PlayerSpaceController I => _instance;

		public float viewMoveSpeed = 600f;
		public float posMoveSpeed = 10f;

		public float posMoveForce = 400f;

		private Transform _transform;
		private Rigidbody _rigidbody;

		private void Awake()
		{
			_instance = this;
			_transform = this.transform;
			_rigidbody = GetComponent<Rigidbody>();
		}

		void Start()
		{
			_rigidbody.AddForce(-Vector3.one * posMoveForce);
			_rigidbody.AddTorque(-Vector3.one);
		}

		void Update()
		{
			float vertical = Input.GetAxis("Vertical") * posMoveForce * Time.deltaTime;
			float horizontal = Input.GetAxis("Horizontal") * posMoveForce * Time.deltaTime;
			bool isUp = Input.GetKey(ConstData.spaceUp);
			bool isDown = Input.GetKey(ConstData.spaceDown);
			float upDown = 0f;
			if (isUp && !isDown)
				upDown = posMoveForce * Time.deltaTime;
			else if (isDown && !isUp)
				upDown = -posMoveForce * Time.deltaTime;
			_rigidbody.AddRelativeForce(new Vector3(horizontal, upDown, vertical));

			//float vertical = Input.GetAxis("Vertical") * posMoveSpeed * Time.deltaTime;
			//float horizontal = Input.GetAxis("Horizontal") * posMoveSpeed * Time.deltaTime;
			//bool isUp = Input.GetKey(ConstData.spaceUp);
			//bool isDown = Input.GetKey(ConstData.spaceDown);
			//float upDown = 0f;
			//if (isUp && !isDown)
			//	upDown = posMoveSpeed * Time.deltaTime;
			//else if(isDown && !isUp)
			//	upDown = -posMoveSpeed * Time.deltaTime;
			//_transform.Translate(new Vector3(horizontal, upDown, vertical), Space.Self);

			if (ModeManager.I.GameMode != GameMode.World)
				return;

			float viewMouseX = Input.GetAxis("Mouse X") * viewMoveSpeed * Time.deltaTime;
			float viewMouseY = -Input.GetAxis("Mouse Y") * viewMoveSpeed * Time.deltaTime;
			_transform.Rotate(new Vector3(viewMouseY, viewMouseX, 0), Space.Self);
		}
	}
}