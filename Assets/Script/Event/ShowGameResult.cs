using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowGameResult : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Text textScore = GameObject.Find("TextScore").GetComponent<Text>();
        Text textResult = GameObject.Find("TextResult").GetComponent<Text>();
        int[] scores = { GameManager.instance.queryScore(0), GameManager.instance.queryScore(1) };

        textScore.text = $"{scores[0]} v.s. {scores[1]}";
        if(scores[0] > scores[1])
            textResult.text = "P1 Win!";
        else if(scores[0] < scores[1])
            textResult.text = "P2 Win!";
        else
            textResult.text = "Draw";
    }
}
