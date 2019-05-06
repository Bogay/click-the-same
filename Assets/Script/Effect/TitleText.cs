using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Text))]
public class TitleText : MonoBehaviour
{
    public List<Color> colors;
    public float duration;

    private Text title;
    private int curr;

    // Start is called before the first frame update
    void Start()
    {
        title = GetComponent<Text>();
        curr = 0;

        // StartCoroutine(this.changeColor());
    }

    private IEnumerator changeColor()
    {
        while(true)
        {
            int next = Random.Range(0, colors.Count);
            while(next == this.curr)
            {
                next = Random.Range(0, colors.Count);
            }

            this.curr = next;
            this.title.DOBlendableColor(colors[this.curr], this.duration);
            yield return new WaitForSeconds(this.duration);
        }
    }
}
