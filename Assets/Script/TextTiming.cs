using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

[RequireComponent(typeof(Text))]
public class TextTiming : MonoBehaviour
{
    public AudioClip alarmSound;

    private int remainTime;
    private Text timingText;

    // Start is called before the first frame update
    void Start()
    {
        this.timingText = GetComponent<Text>();
        this.remainTime = GameManager.instance.gameDuration;

        StartCoroutine(this.timing());
    }

    private IEnumerator timing()
    {
        while(this.remainTime > 0)
        {
            // final count down
            if(this.remainTime <= 10)
            {
                this.timingText.color = Color.red;
                AudioManager.instance.playSE(this.alarmSound);
            }

            // bgm fade out
            if(this.remainTime == 3)
            {
                AudioManager.instance.bgmSource.DOFade(0, 3f);
            }

            this.timingText.text = $"{ this.remainTime, 02 }";
            this.remainTime--;

            yield return new WaitForSeconds(1);
        }

        // game end
        this.timingText.text = "00";
        AudioManager.instance.stopBGM();
        SceneManager.LoadScene("End");
    }
}
