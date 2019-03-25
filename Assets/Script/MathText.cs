using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MathText : MonoBehaviour
{
	public Transform targetBlock;
	private Text textSelf;
	private Transform self;

	// Use this for initialization
	void Start()
	{
		this.textSelf = GetComponent<Text>();
		this.self = transform;
	}

	// Update is called once per frame
	void Update()
	{
		this.self.position = RectTransformUtility.WorldToScreenPoint(Camera.main, targetBlock.position);
	}
}