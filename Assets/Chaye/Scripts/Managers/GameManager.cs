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
			if (_instance != null)
			{
				DestroyImmediate(this.gameObject);
				return;
			}

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
			PachiGrimoire.I.OnFinish = () => ModeManager.I.TransferTo(GameMode.World);
			KnowledgeManager.I.knowledgeDataArray = this.knowledgeDataArray;
			KnowledgeManager.I.Init();

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
				if (IsScene(ConstData.scene01) 
					|| IsScene(ConstData.scene02))
				{
					if (PachiGrimoire.I.IsIdle() == true && UIManager.I.CurrentAni == null)
					{
						UIManager.I.OpenPanel(UIManager.I.menuAni);
						ModeManager.I.TransferTo(GameMode.AVG);
					}
					else
					{
						UIManager.I.ClosePanel();
						ModeManager.I.TransferTo(GameMode.World);
					}
				}
			}


			#region Tick

			float deltaTime = Time.deltaTime;
			KnowledgeManager.I.Tick(deltaTime);

			#endregion
		}

		public void StartAVGEngine(string storyFileName)
		{
			ModeManager.I.TransferTo(GameMode.AVG);
			PachiGrimoire.I.StartAVGEngine(storyFileName);
		}

		public void NewGame()
		{
			UIManager.I.ClosePanel();
			SceneManager.LoadScene(ConstData.scene01);
		}

		public void BackToMainMenu()
		{
			UIManager.I.ClosePanel();
			SceneManager.LoadScene(ConstData.scene00);
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

			MusicManager.I.SetBGMOnSceneLoaded(sceneName);
			KnowledgeManager.I.OnSceneLoaded(sceneName);
		}

		public bool IsScene(string sceneName)
		{
			return SceneManager.GetActiveScene().name.Equals(sceneName);
		}

		public void LoadScene(string sceneName)
		{
			if (PachiGrimoire.I.IsIdle() && ModeManager.I.GameMode == GameMode.World)
				SceneManager.LoadScene(sceneName);
			else
				StartCoroutine(WaitForLoadScene(sceneName));
		}

		
		private WaitForSeconds _waitForOneSecond = new WaitForSeconds(2);
		IEnumerator WaitForLoadScene(string sceneName)
		{
			while(true)
			{
				yield return _waitForOneSecond;
				if (PachiGrimoire.I.IsIdle() && ModeManager.I.GameMode == GameMode.World)
					SceneManager.LoadScene(sceneName);
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