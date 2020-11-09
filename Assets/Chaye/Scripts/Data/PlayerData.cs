﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdlessChaye.VRStory
{
	public class PlayerData : BaseManager
	{
		private static PlayerData _playerData = new PlayerData();
		public static PlayerData I => _playerData;


		public List<string> knowledgeList = new List<string>();

		public void AddKnowledge(string itemName)
		{
			if (knowledgeList.Contains(itemName) == false)
			{
				knowledgeList.Add(itemName);
			}
		}
	}
}