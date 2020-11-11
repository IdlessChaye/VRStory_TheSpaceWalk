using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using IdlessChaye.IdleToolkit.AVGEngine;

namespace IdlessChaye.VRStory
{
	public class KnowledgeManager : BaseManager
	{
		private static KnowledgeManager _instance = new KnowledgeManager();
		public static KnowledgeManager I => _instance;


		public KnowledgeData[] knowledgeDataArray;


		public bool IsWorking { get; set; }

		private List<KnowledgePiece> knowPieceRemainList = new List<KnowledgePiece>();
		private Queue<KnowledgeData> knowToBeRead = new Queue<KnowledgeData>();

		private float _moveDis = -1000000f;
		private float _scene02startTime = 1000000f;


		public void AddKnowledge(string itemName)
		{
			if (string.IsNullOrEmpty(itemName))
				return;
			var data = GetKnowledgeData(itemName);
			if (data == null)
				return;
			PlayerData.I.AddKnowledge(data.itemName);
			FinishKnowledge();
		}
		public void AddKnowledge(KnowledgeData data)
		{
			PlayerData.I.AddKnowledge(data.itemName);
			FinishKnowledge();
		}


		public KnowledgeData GetKnowledgeData(string itemName)
		{
			if (string.IsNullOrEmpty(itemName))
				return null;

			foreach (var item in knowledgeDataArray)
			{
				if (item.itemName != null && item.itemName.Equals(itemName))
				{
					return item;
				}
			}

			return null;
		}


		public override void Init()
		{
			IsWorking = false;

			foreach(var data in knowledgeDataArray)
			{
				System.Func<bool> canBeObtained = null;
				var itemName = data.itemName;
				if (itemName.Equals(ConstData.太空漫步))
				{
					canBeObtained = () =>
					{
						if (GameManager.I.IsScene(ConstData.scene01) == false)
							return false;
						var player = PlayerSpaceController.I;
						if (player == null)
							return false;
						var stationPos = new Vector3(-100, 12.84f, -85);
						float trigDisSqr = 2414;
						var dis = Vector3.SqrMagnitude(stationPos - player.transform.position);
						if (dis <= trigDisSqr)
						{
							return true;
						}
						return false;
					};
				}
				else if (itemName.Equals(ConstData.地球))
				{
					canBeObtained = () =>
					{
						var player = PlayerSpaceController.I;
						if (player == null)
							return false;
						var earthDir = -Vector3.forward;
						var cosValue = Vector3.Dot(player.transform.forward.normalized, earthDir);
						if (cosValue > 0.9f)
							return true;
						return false;
					};
				}
				else if (itemName.Equals(ConstData.空间站))
				{
					canBeObtained = () =>
					{
						if (GameManager.I.IsScene(ConstData.scene02) == false)
							return false;
						if (Input.GetKey(KeyCode.W))
						{
							_moveDis += 1f;
						}
						return _moveDis > 1000f;
					};
				}
				else if (itemName.Equals(ConstData.生命保障系统))
				{
					canBeObtained = () =>
					{
						if (GameManager.I.IsScene(ConstData.scene02) == false)
							return false;
						return PlayerData.I.ReadBookCount >= 1;
					};
				}
				else if (itemName.Equals(ConstData.星际旅行))
				{
					canBeObtained = () =>
					{
						if (GameManager.I.IsScene(ConstData.scene02) == false)
							return false;
						return PlayerData.I.ReadBookCount >= 4 && Time.time - _scene02startTime > 20f;
						;
					};
				}

				var piece = new KnowledgePiece
				{
					Data = data,
					CanBeObtainedCallback = canBeObtained
				};
				knowPieceRemainList.Add(piece);
			}
		}

		public override void Tick(float deltaTime)
		{
			#region Ready Knowledge
			for (int i = 0; i < knowPieceRemainList.Count; i++)
			{
				var piece = knowPieceRemainList[i];
				if (piece.CanBeObtainedCallback != null && piece.CanBeObtainedCallback())
				{
					knowPieceRemainList.Remove(piece);
					i--;
					ReadyKnowledge(piece.Data);
				}
			}
			#endregion

			#region Invoke Knowledge
			if (knowToBeRead.Count != 0 && IsWorking == false)
			{
				string sceneName = SceneManager.GetActiveScene().name;
				if (sceneName.Equals(ConstData.scene01) || sceneName.Equals(ConstData.scene02))
				{
					if (PachiGrimoire.I.IsIdle() == true && UIManager.I.CurrentAni == null && ModeManager.I.GameMode == GameMode.World)
					{
						IsWorking = true;
						InvokeKnowledge(knowToBeRead.Dequeue().itemName);
					}
				}
			}
			#endregion
		}

		public void ReadyKnowledge(KnowledgeData knowData)
		{
			if (knowData != null)
				knowToBeRead.Enqueue(knowData);
		}

		private void InvokeKnowledge(string itemName)
		{
			if (string.IsNullOrEmpty(itemName))
				return;

			PachiGrimoire.I.StartAVGEngine(itemName);
		}

		private void FinishKnowledge()
		{
			IsWorking = false;
		}

		public void OnSceneLoaded(string sceneName)
		{
			if (GameManager.I.IsScene(ConstData.scene02))
			{
				_moveDis = 0f;
				_scene02startTime = Time.time;
			}
		}
	}
}