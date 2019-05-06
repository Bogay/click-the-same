using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
	public AudioClip bgm;

	private void Start()
	{
		GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("Main"));
	}
}