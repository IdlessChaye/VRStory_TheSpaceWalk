using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdlessChaye.VRStory
{
	public enum GameMode
	{
		World,
		AVG,
		CutScene
	}

	public class ModeManager : BaseManager
	{
		private static ModeManager _instance = new ModeManager();
		public static ModeManager I => _instance;


		private GameMode _currentMode;
		public GameMode GameMode => _currentMode;

		public override void Init()
		{
			TransferTo(GameMode.World);
		}

		public void TransferTo(GameMode newMode)
		{
			_currentMode = newMode;

			switch (newMode)
			{
				case GameMode.World:
					Cursor.lockState = CursorLockMode.Locked;
					Cursor.visible = false;
					break;
				case GameMode.AVG:
					Cursor.lockState = CursorLockMode.Confined;
					Cursor.visible = true;
					break;
				case GameMode.CutScene:
					Cursor.lockState = CursorLockMode.Confined;
					Cursor.visible = true;
					break;
			}
		}
	}
}