using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class GameManager : MonoBehaviour
{
	private static GameManager _instance;
	public static GameManager instance { get { return _instance; } }

	public Dictionary<int, Container> teams;
	public Dictionary<int, int> scores;
	public AudioClip bgm;
	public int gameDuration;

	void Awake()
	{
		if(_instance && _instance != this)
		{
			Destroy(this);
			return;
		}

		_instance = this;
		this.teams = new Dictionary<int, Container>();
		this.scores = new Dictionary<int, int>();
		SceneManager.sceneLoaded += this.onSceneLoaded;
		DontDestroyOnLoad(this);
	}

	public Container queryTeam(int number)
	{
		if(!this.teams.ContainsKey(number))
		{
			Debug.Log($"Team [{number}] haven't been registered.");
			return null;
		}

		return this.teams[number];
	}

	public int queryScore(int number)
	{
		if(!this.scores.ContainsKey(number))
		{
			Debug.Log($"Team [{number}] haven't been registered.");
			return 0;
		}

		return this.scores[number];
	}

	public void registerTeam(int number, Container team)
	{
		if(this.teams.ContainsKey(number))
		{
			Debug.Log($"Team [{number}] has been registered by [{this.teams[number]}]");
			return;
		}

		this.teams[number] = team;
	}

	private void onSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		string sceneName = scene.name;
		switch(sceneName)
		{
			case "Menu":
				DOTween.Init();
				break;
			case "Main":
				// set BGM
				AudioManager.instance.bgmSource.volume = 1;

				AudioManager.instance.setBGM(this.bgm);
				AudioManager.instance.playBGM();
				break;
			case "End":
				this.scores.Clear();
				foreach (var item in this.teams)
					this.scores.Add(item.Key, item.Value.score);
				this.teams = new Dictionary<int, Container>();
				break;
			default:
				Debug.Log($"No event for {sceneName}");
				break;
		}
	}
}