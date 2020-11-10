using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdlessChaye.VRStory
{
	public class BaseManager
	{
		public virtual void Init() { }

		public virtual void Tick(float deltaTime) { }
	}
}