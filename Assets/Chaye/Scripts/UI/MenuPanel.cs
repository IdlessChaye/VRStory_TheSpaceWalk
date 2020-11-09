using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace IdlessChaye.VRStory
{
	public class MenuPanel : MonoBehaviour
	{
		public Button buttonGameStart;

		private void Start()
		{
			buttonGameStart.onClick.AddListener(() =>
			{
				SceneManager.LoadScene(ConstData.scene01);
			});
		}
	}
}