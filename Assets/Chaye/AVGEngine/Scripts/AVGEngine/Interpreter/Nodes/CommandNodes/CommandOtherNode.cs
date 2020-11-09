using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdlessChaye.IdleToolkit.AVGEngine
{
	public class CommandOtherNode : BaseInterpreterNode
	{
		private List<BaseInterpreterNode> nodeList = new List<BaseInterpreterNode>();

		public override void Interpret(ScriptSentenceContext context)
		{
			context.SkipToken("Other");
			while (true)
			{
				string funcToken = context.CurrentToken;
				if (funcToken == null)
					break;

				if (!CanParse(funcToken, context))
					break;
			}
		}

		public override void Execute()
		{
			for (int i = 0; i < nodeList.Count; i++)
			{
				BaseInterpreterNode node = nodeList[i];
				node.Execute();
			}
		}

		private bool CanParse(string token, ScriptSentenceContext scriptSentenceContext)
		{
			bool canParse = true;

			BaseInterpreterNode node = null;
			if (token.Equals("KnowledgeAdd"))
			{
				node = new OtherKnowledgeAddNode();
			}
			else
			{
				canParse = false;
			}

			if (canParse)
			{
				nodeList.Add(node);
				node.Interpret(scriptSentenceContext);
			}
			else
			{
				Debug.Log("CommandImageNode FALSE canParse! token :" + token);
			}

			return canParse;
		}

	}
}