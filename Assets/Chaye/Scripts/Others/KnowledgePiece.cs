using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdlessChaye.VRStory
{
	public class KnowledgePiece
	{
		public KnowledgeData Data { get; set; }

		public System.Func<bool> CanBeObtainedCallback { get; set; }
	}
}