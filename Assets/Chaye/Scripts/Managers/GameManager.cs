﻿using System.Collections;
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

		public KnowledgeData[] knowledgeDataArray;

		private void Start()
		{
			SceneManager.sceneLoaded += OnSceneLoaded;

			PlayerData.I.Init();
			ModeManager.I.Init();
			KnowledgeManager.I.Init();

			PachiGrimoire.I.OnFinish = () => ModeManager.I.TransferTo(GameMode.World);
			KnowledgeManager.I.knowledgeDataArray = this.knowledgeDataArray;


			InitLastPart();
		}


		private void Update()
		{
			#region Test
			if (Input.GetKeyDown(KeyCode.T))
			{
				StartAVGEngine(ConstData.avgScriptName);
			}
			#endregion

			if (Input.GetKeyDown(ConstData.ESC))
			{
				if (SceneManager.GetActiveScene().name.Equals(ConstData.scene01))
				{
					if (PachiGrimoire.I.IsIdle() == true && UIManager.I.CurrentAni == null)
					{
						UIManager.I.OpenPanel(UIManager.I.menuAni);
					}
					else
					{
						UIManager.I.ClosePanel();
					}
				}
			}
		}

		public void StartAVGEngine(string storyFileName)
		{
			ModeManager.I.TransferTo(GameMode.AVG);
			PachiGrimoire.I.StartAVGEngine(storyFileName);
		}

		public void NewGame()
		{
			SceneManager.LoadScene(ConstData.scene01);
		}

		private void InitLastPart()
		{
			ModeManager.I.TransferTo(GameMode.Idle);
			UIManager.I.OpenPanel(UIManager.I.mainMenuAni);
		}

		private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
		{
			string sceneName = scene.name;
			UIManager.I.ClosePanel();
			if (sceneName.Equals(ConstData.scene00))
			{
				InitLastPart();
			}
			else if (sceneName.Equals(ConstData.scene01))
			{
				ModeManager.I.TransferTo(GameMode.World);
				StartAVGEngine(ConstData.avgScriptName);
			}
			else if(sceneName.Equals(ConstData.scene02))
			{
				ModeManager.I.TransferTo(GameMode.World);
				StartAVGEngine(ConstData.avgScriptName02);
			}
		}

		public void Quit()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
		}

	}
}