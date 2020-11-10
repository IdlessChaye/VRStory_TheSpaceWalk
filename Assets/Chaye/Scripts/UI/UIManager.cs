using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

		public GameObject knowButton;
		public RectTransform leftContent;
		public Text rightHeader;
		public Text rightText;

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
			InitPanel(ani);
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
			else if (sceneName.Equals(ConstData.scene02))
			{
				OpenPanel(menuAni);
			}
		}

		private void InitPanel(Animator ani)
		{
			if (ani == notesAni)
			{
				rightHeader.text = "";
				rightText.text = "";
				var knowList = PlayerData.I.knowledgeList;
				if (knowList.Count != 0)
				{
					var knowData = KnowledgeManager.I.GetKnowledgeData(knowList[0]);
					var childButton = leftContent.Find(knowData.itemName);
					if (childButton == null)
					{ 
						var buttonGO = Instantiate(knowButton, leftContent, false);
						buttonGO.name = knowData.itemName;
						var buttonText = buttonGO.transform.GetComponentsInChildren<Text>();
						buttonText[0].text = knowData.itemName;
						var button = buttonGO.GetComponent<Button>();
						button.onClick.RemoveAllListeners();
						button.onClick.AddListener(() =>
						{
							rightHeader.text = knowData.itemName;
							rightText.text = knowData.itemDescribe;
						});
						childButton = buttonGO.transform;
					}

					rightHeader.text = knowData.itemName;
					rightText.text = knowData.itemDescribe;
					EventSystem.current.SetSelectedGameObject(childButton.gameObject);
				}

				for(int i = 1; i < knowList.Count; i++)
				{
					var knowData = KnowledgeManager.I.GetKnowledgeData(knowList[i]);
					var childButton = leftContent.Find(knowData.itemName);
					if (childButton == null)
					{
						var buttonGO = Instantiate(knowButton, leftContent, false);
						buttonGO.name = knowData.itemName;
						var buttonText = buttonGO.transform.GetComponentsInChildren<Text>();
						buttonText[0].text = knowData.itemName;
						var button = buttonGO.GetComponent<Button>();
						button.onClick.RemoveAllListeners();
						button.onClick.AddListener(() =>
						{
							rightHeader.text = knowData.itemName;
							rightText.text = knowData.itemDescribe;
						});
					}
				}
			}
		}

	}
}