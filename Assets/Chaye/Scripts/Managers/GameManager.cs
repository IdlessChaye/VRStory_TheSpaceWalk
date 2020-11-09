using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IdlessChaye.IdleToolkit.AVGEngine;
using UnityEngine.SceneManagement;

namespace IdlessChaye.VRStory
{
	public class GameManager : MonoBehaviour
	{
		#region Instance
		private static GameManager _instance;
		public static GameManager I => _instance;

		private void Awake()
		{
			_instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		#endregion

		
		

		private void Start()
		{
			SceneManager.sceneLoaded += OnSceneLoaded;
			ModeManager.I.Init();

			PachiGrimoire.I.OnFinish = () => ModeManager.I.TransferTo(GameMode.World);

			//StartAVGEngine(ConstData.avgScriptName);
		}


		private void Update()
		{
			#region Test
			if (Input.GetKeyDown(KeyCode.T))
			{
				StartAVGEngine(ConstData.avgScriptName);
			}
			#endregion
		}

		public void StartAVGEngine(string storyFileName)
		{
			ModeManager.I.TransferTo(GameMode.AVG);
			PachiGrimoire.I.StartAVGEngine(storyFileName);
		}




		private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
		{
			string sceneName = scene.name;
			if (sceneName.Equals(ConstData.scene00))
			{
				ModeManager.I.TransferTo(GameMode.Idle);
				Debug.Log("Current Scene: " + ConstData.scene00);
			}
			else if (sceneName.Equals(ConstData.scene01))
			{
				ModeManager.I.TransferTo(GameMode.World);
				StartAVGEngine(ConstData.avgScriptName);
			}
			else if(sceneName.Equals(ConstData.scene02))
			{
				StartAVGEngine(ConstData.avgScriptName02);
			}
		}


	}
}