using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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



	}
}