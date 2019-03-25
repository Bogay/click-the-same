using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;
	public static GameManager instance { get { return _instance; } }

	public Dictionary<int, Container> teams;

	void Awake()
	{
		if(_instance && _instance != this)
		{
			Destroy(this);
			return;
		}

		_instance = this;
		this.teams = new Dictionary<int, Container>();
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

	public void registerTeam(int number, Container team)
	{
		if(this.teams.ContainsKey(number))
		{
			Debug.Log($"Team [{number}] has been registered by [{this.teams[number]}]");
			return;
		}

		this.teams[number] = team;
	}
}