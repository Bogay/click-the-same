using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRainfall : MonoBehaviour
{
    public GameObject rain;

    [Header("Bounds")]
    public float left;
    public float right;
    public float upper;
    public float lower;

    public float coolDown;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(this.generate());
    }

    private IEnumerator generate()
    {
        while(true)
        {
            if(Random.Range(0, 1f) > 0.5f)
            {
                GameObject go = Instantiate(this.rain, new Vector3(Random.Range(this.left, this.right), this.upper), Quaternion.identity);
                float sy = Random.Range(0.8f, 1.5f);
                float sx = sy * Random.Range(0.5f, 0.8f);
                go.transform.localScale = new Vector3(sx, sy, 1);

                go.GetComponent<RainBlock>().speed = Random.Range(4f, 12f);

                yield return new WaitForSeconds(this.coolDown);
            }
        }
    }
}
