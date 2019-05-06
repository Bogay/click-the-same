using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndSceneButtonEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Button Restart").GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("Main"));
        GameObject.Find("Button Quit").GetComponent<Button>().onClick.AddListener(() => Application.Quit());
    }
}
