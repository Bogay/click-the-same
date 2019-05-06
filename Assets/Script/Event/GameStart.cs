using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
	private void Start()
	{
		GameObject.Find("ButtonStart").GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("Main"));
		GameObject.Find("ButtonQuit").GetComponent<Button>().onClick.AddListener(() => Application.Quit());
		GameObject.Find("ButtonRepoLink").GetComponent<Button>().onClick.AddListener(() => Application.OpenURL("https://github.com/Bogay/click-the-same"));
	}
}