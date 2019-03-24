using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Container : MonoBehaviour
{
	public int teamNumber;
	public GameObject blockGroup;
	public int row;
	public int col;
	public float rowSize;
	public float colSize;

	// temperary variable for test
	public int score = 0;

	private Transform self;
	private GameObject block;
	private GameObject[,] blockGrid;

	private MathBlock selectedBlock;

	// Use this for initialization
	void Start()
	{
		GameManager.instance.registerTeam(this.teamNumber, this);

		// initialize cache
		this.self = transform;
		this.block = ResourceManager.instance.mathBlockPrefab;

		this.selectedBlock = null;
		this.blockGrid = new GameObject[this.row, this.col];

		Vector3 curr = transform.position;
		GameObject temp;
		for(int i=0 ; i<this.row ; i++)
		{
			for(int j=0 ; j<this.col ; j++)
			{
				// spawn a block
				temp = Instantiate(block, curr, Quaternion.identity, blockGroup.transform);
				temp.name = $"block-{i}-{j}";

				// initialize
				MathBlock mb =  temp.GetComponent<MathBlock>();
				mb.container = this;
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

	public void onSelect(MathBlock mb)
	{
		// select first block
		if(!this.selectedBlock)
		{
			this.selectedBlock = mb;
			return;
		}
		// unselect block
		if(this.selectedBlock == mb)
		{
			this.selectedBlock = null;
			return;
		}

		// select right block
		if(this.selectedBlock.value == mb.value)
		{
			Debug.Log("Correct!");

			// attack opponent
			Container opponent = GameManager.instance.queryTeam(1 - this.teamNumber);
			opponent.onAttacked();

			// add score
			this.score++;
			Debug.Log($"Team [{this.teamNumber}]: {this.score}");

			// re-generate blocks
			this.selectedBlock.calculate(Random.Range(1, 25));
			mb.calculate(Random.Range(1, 25));
			this.selectedBlock = null;
		}
		// wrong selection
		else
		{
			Debug.Log("Bu~Bu~~desuwa");

			// punishment
			this.selectedBlock.calculate(this.selectedBlock.value);
			mb.calculate(mb.value);
			this.selectedBlock = null;
		}
	}

	public void onAttacked()
	{
		// choose two blocks to re-generate value
		MathBlock mb = this.blockGrid[Random.Range(0, this.row), Random.Range(0, this.col)].GetComponent<MathBlock>();
		mb.calculate(Random.Range(1, 25));
		Debug.Log($"attacked: {mb.name}");

		// second one
		MathBlock mb2;
		do
		{
			mb2 = this.blockGrid[Random.Range(0, this.row), Random.Range(0, this.col)].GetComponent<MathBlock>();
		} while (mb == mb2);
		mb2.calculate(Random.Range(1, 25));
		Debug.Log($"attacked: {mb2.name}");
	}
}