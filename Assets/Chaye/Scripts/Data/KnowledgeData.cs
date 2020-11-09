using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdlessChaye.VRStory
{
	[CreateAssetMenu(fileName = "KnowledgeData", menuName = "VRStory/Knowledge")]
	public class KnowledgeData : ScriptableObject
	{
		public string itemName = "";
		public string itemDescribe = "";
	}
}