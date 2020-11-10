using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IdlessChaye.VRStory;

namespace IdlessChaye.IdleToolkit.AVGEngine
{
	public class OtherKnowledgeAddNode : FunNode
	{
		public override void Interpret(ScriptSentenceContext context)
		{
			context.SkipToken("KnowledgeAdd");
			InterpretPart(context);
		}



		protected override void OnUpdateStageContext()
		{
			if (paraList.Count != 1)
				throw new System.Exception("OtherKnowledgeAddNode");

			KnowledgeManager.I.AddKnowledge(paraList[0]);
		}


	}
}