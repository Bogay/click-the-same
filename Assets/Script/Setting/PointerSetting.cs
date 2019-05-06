using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Pointer")]
public class PointerSetting : ScriptableObject
{
	[Header("Keys")]
	public KeyCode fireKey;
	public KeyCode upKey;
	public KeyCode downKey;
	public KeyCode leftKey;
	public KeyCode rightKey;

	[Header("Move")]
	public float cooldown;
	public Vector2 offset;
}