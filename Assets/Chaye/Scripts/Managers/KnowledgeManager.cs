using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdlessChaye.VRStory
{
	public class KnowledgeManager : BaseManager
	{
		private static KnowledgeManager _instance = new KnowledgeManager();
		public static KnowledgeManager I => _instance;


		public KnowledgeData[] knowledgeDataArray;

		public void AchieveKnowledge(string itemName)
		{
			if (string.IsNullOrEmpty(itemName))
				return;

			foreach(var item in knowledgeDataArray)
			{
				if (item.itemName != null && item.itemName.Equals(itemName))
				{
					PlayerData.I.AddKnowledge(item.itemName);
				}
			}
		}
	}
}