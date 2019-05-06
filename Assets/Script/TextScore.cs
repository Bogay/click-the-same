using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextScore : MonoBehaviour
{
	public TextScoreSetting setting;

	private float downForce { get { return setting.downForce; } }
	private float upForce { get { return setting.upForce; } }
	private AudioClip addScoreClip { get { return setting.addScoreClip; } }

	private Transform self;
	private Vector3 origin;
	private Text selfText;

	// Use this for initialization
	void Start()
	{
		this.self = transform;
		this.origin = this.self.position;
		this.selfText = GetComponent<Text>();
	}

	private void Update()
	{
		this.self.position = Vector3.Lerp(this.self.position, this.origin, this.downForce * Time.deltaTime);
	}

	public void setText(string s) => this.selfText.text = s;
	public void addScore()
	{
		this.self.position += Vector3.up * this.upForce;
		AudioManager.instance.playSE(this.addScoreClip);
	} 
}