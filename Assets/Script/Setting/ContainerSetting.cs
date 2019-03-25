using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Container")]
public class ContainerSetting : ScriptableObject
{
	public int row;
	public int col;
	public float rowSize;
	public float colSize;
	public float forceScale;
}