using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
	private void Start()
	{
		GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("Main"));
	}
}