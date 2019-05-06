using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Container")]
public class ContainerSetting : ScriptableObject
{
	[Header("Grid Size")]
	public int row;
	public int col;

	[Header("Block Size")]
	public float rowSize;
	public float colSize;

	[Header("Effects")]
	public float forceScale;
	public float attackDuration;
}