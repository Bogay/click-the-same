using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/TextScore")]
public class TextScoreSetting : ScriptableObject
{
	public float upForce;
	public float downForce;
	public AudioClip addScoreClip;
}