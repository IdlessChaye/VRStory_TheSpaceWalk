using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IdlessChaye.VRStory
{
	public class EnterHomeTrigger : MonoBehaviour
	{
		private void OnTriggerEnter(Collider other)
		{
			if(other.GetComponent<PlayerSpaceController>() != null)
			{
				Debug.Log("Enter Spaceship...");
				SceneManager.LoadScene(ConstData.scene02);
			}
		}
	}
}