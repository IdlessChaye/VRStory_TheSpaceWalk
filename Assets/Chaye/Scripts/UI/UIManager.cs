using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IdlessChaye.VRStory
{
	public class UIManager : MonoBehaviour
	{
		private static UIManager _instance;
		public static UIManager I => _instance;


		public Animator mainMenuAni;
		public Animator settingAni;
		public Animator menuAni;
		public Animator notesAni;
		public PanelManager topPanelManager;

		public Animator CurrentAni => _currentAni;
		private Animator _currentAni;

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

		public void OpenPanel(Animator ani)
		{
			_currentAni = ani;
			topPanelManager.OpenPanel(ani);
		}

		public void ClosePanel()
		{
			topPanelManager.CloseCurrent();
			_currentAni = null;
		}

		public void CloseMenuPanel()
		{
			ModeManager.I.TransferTo(GameMode.World);
			ClosePanel();
		}

		public void NotesBackCallback()
		{
			var sceneName = SceneManager.GetActiveScene().name;
			if (sceneName.Equals(ConstData.scene00))
			{
				OpenPanel(mainMenuAni);
			}
			else if (sceneName.Equals(ConstData.scene01))
			{
				OpenPanel(menuAni);
			}
			else
			{

			}
		}

	}
}