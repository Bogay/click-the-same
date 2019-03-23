using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MathBlock : MonoBehaviour
{
	public GameObject textObject;
	public int value { get; private set; }
	public bool isSelected;
	private Text textFormula;
	private Transform self;

	void Awake()
	{
		this.value = 0;
		this.isSelected = false;
		GameObject go = Instantiate(textObject, transform.position, Quaternion.identity, GameObject.Find("Canvas").transform);
		go.name = $"text-{gameObject.name}";
		this.textFormula = go.GetComponent<Text>();
		this.self = transform;
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void calculate(int v)
	{
		this.value = v;

		int n; // temparary value for formula
		int op = Random.Range(0, 4);

		if(!this.textFormula) Debug.Log("Text not found!");

		switch(op)
		{
			// plus
			case 0:
				n = Random.Range(1, v);
				textFormula.text = $"{v - n} + {n}";
				break;
			// subtract
			case 1:
				n = Random.Range(1, v);
				textFormula.text = $"{v + n} - {n}";
				break;
			// multiply
			case 2:
				n = Random.Range(1, v);
				// insure n is a factor of v
				while(v % n != 0) n++;
				textFormula.text = $"{v / n} x {n}";
				break;
			// divide
			case 3:
				n = Random.Range(1, 5);
				textFormula.text = $"{v * n} / {n}";
				break;
		}
	}
}