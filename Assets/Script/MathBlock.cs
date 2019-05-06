using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MathBlock : MonoBehaviour
{
	// animator parameter
	public const string IS_SELECTED = "isSelected";
	public const string IS_AIMED = "isAimed";

	public Container container;
	public int value { get; private set; }
	public bool isSelected { get { return this.anim.GetBool(MathBlock.IS_SELECTED); } }
	public Animator anim { get; private set; }
	public Transform self { get; private set; }

	private Text textFormula;
	private SpriteRenderer spriteRenderer;

	void Awake()
	{
		this.value = 0;

		this.self = transform;
		this.anim = GetComponent<Animator>();

		this.spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		
		// setup text
		GameObject go = Instantiate(ResourceManager.instance.textPrefab, RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position), Quaternion.identity, ResourceManager.instance.canvasObject.transform);
		go.name = $"text-root-{gameObject.name}";
		go.GetComponentInChildren<MathText>().targetBlock = this.self;
		this.textFormula = go.GetComponentInChildren<Text>();
		go.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100) * this.self.localScale.x;
	}

	public void select()
	{
		this.anim.SetBool(MathBlock.IS_SELECTED, !this.isSelected);

		// this.refresh();
		this.container.onSelect(this);
	}

	private void refresh()
	{
		// Debug.Log($"refresh: { this.isSelected }");

		if(this.isSelected)
			// this.spriteRenderer.color = Color.red;
			this.spriteRenderer.DOColor(Color.red, 0.3f);
		else
			// this.spriteRenderer.color = Color.white;
			this.spriteRenderer.DOColor(Color.white, 0.3f);
	}

	public void calculate(int v)
	{
		this.anim.SetBool(MathBlock.IS_SELECTED, false);
		// this.refresh();
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