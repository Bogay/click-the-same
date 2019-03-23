using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Container : MonoBehaviour
{
	public GameObject block;
	public GameObject blockGroup;
	public int row;
	public int col;
	public float rowSize;
	public float colSize;

	private Transform self;
	private GameObject[,] blockGrid;

	// Use this for initialization
	void Start()
	{
		// Transform cache
		this.self = transform;

		this.blockGrid = new GameObject[this.row, this.col];

		Vector3 curr = transform.position;
		GameObject temp;
		for(int i=0 ; i<this.row ; i++)
		{
			for(int j=0 ; j<this.col ; j++)
			{
				Debug.Log("Spawn a block.");

				// spawn a block
				temp = Instantiate(block, curr, Quaternion.identity, blockGroup.transform);
				temp.name = $"block-{i}-{j}";

				// calculate the value
				MathBlock mb =  temp.GetComponent<MathBlock>();
				mb.calculate(Random.Range(1, 25));

				this.blockGrid[i, j] = temp;

				curr.x += this.colSize;
			}
			curr.x = self.position.x;
			curr.y -= this.rowSize;
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}