using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdlessChaye.VRStory
{
	public class MusicManager : MonoBehaviour
	{
		private static MusicManager _instance;
		public static MusicManager I => _instance;


		public AudioClip[] bgms;

		public AudioSource bgmSource;

		private void Awake()
		{
			if (_instance != null)
			{ 
				DestroyImmediate(this.gameObject);
				return;
			}

			_instance = this;

			bgmSource.loop = true;
			bgmSource.playOnAwake = false;

			InitLastPart();
		}



		public void SetBGMOnSceneLoaded(string sceneName)
		{
			StopBGM();

			if (GameManager.I.IsScene(ConstData.scene00))
			{
				InitLastPart();
			}
			else if (GameManager.I.IsScene(ConstData.scene01))
			{
				PlayBGM(ConstData.scene01BGM);
			}
			else if (GameManager.I.IsScene(ConstData.scene02))
			{
				PlayBGM(ConstData.scene02BGM);
			}
			else
			{

			}
		}

		private void InitLastPart()
		{
			PlayBGM(ConstData.scene00BGM);
		}

		
		public void StopBGM()
		{
			bgmSource.Stop();
			bgmSource.clip = null;
		}

		public void PlayBGM(string clipName)
		{
			var clip = GetAudioClip(clipName);
			if (clip == null)
				return;
			bgmSource.clip = clip;
			bgmSource.Play();
			StartCoroutine(ClipVolumeGrowth(bgmSource, 2f));
		}

		private WaitForEndOfFrame _waitForEndOfFrame = new WaitForEndOfFrame();
		private IEnumerator ClipVolumeGrowth(AudioSource source, float time)
		{
			source.volume = 0f;
			float curVolume = 0f;
			while(curVolume <= time)
			{
				yield return _waitForEndOfFrame;
				curVolume += Time.deltaTime;
				var volumeValue = Mathf.Clamp(curVolume, 0f, 1f);
				source.volume = volumeValue;
			}
			yield return 0;
		}

		private AudioClip GetAudioClip(string clipName)
		{
			if (string.IsNullOrEmpty(clipName))
				return null;

			foreach (var clip in bgms)
			{
				if (clip.name.Equals(clipName))
					return clip;
			}
			return null;
		}
	}
}