using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Container : MonoBehaviour
{
	public int teamNumber;
	public ContainerSetting setting;
	public int row { get { return setting.row; } }
	public int col { get { return setting.col; } }
	public float rowSize { get { return setting.rowSize; } }
	public float colSize { get { return setting.colSize; } }
	public float forceScale { get { return setting.forceScale; } }
	public float attackDuration { get { return setting.attackDuration; } }

	// game objects
	public TextScore textScore;
	public GameObject blockGroup;

	// temperary variable for test
	public int score = 0;

	private Vector2 force;

	private Transform self;
	private GameObject block;
	private MathBlock[,] blockGrid;
	private Transform[,] blockTransform;

	private MathBlock selectedBlock;
	private CameraShake cameraShake;

	// Use this for initialization
	void Start()
	{
		GameManager.instance.registerTeam(this.teamNumber, this);
		this.cameraShake = FindObjectOfType<CameraShake>();

		// initialize cache
		this.self = transform;
		this.block = ResourceManager.instance.mathBlockPrefab;

		this.selectedBlock = null;
		this.blockGrid = new MathBlock[this.row, this.col];
		this.blockTransform = new Transform[this.row, this.col];

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
				mb.calculate(this.getBlockRandomValue());

				this.blockGrid[i, j] = mb;
				this.blockTransform[i, j] = temp.transform;

				curr.x += this.colSize;
			}
			curr.x = self.position.x;
			curr.y -= this.rowSize;
		}
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
			System.Tuple<MathBlock, MathBlock> targets = opponent.getTargets();


			// attack first block
			GameObject bullet1 = Instantiate(ResourceManager.instance.bulletPrefab, this.selectedBlock.self.position, Quaternion.identity);
			bullet1.transform
			.DOMove(targets.Item1.self.position, this.attackDuration)
			.OnComplete( () =>
				{
					targets.Item1.calculate(this.getBlockRandomValue());
					targets.Item1.anim.SetBool(MathBlock.IS_AIMED, false);
					Destroy(bullet1);

					// camera shake
					cameraShake.addForce(Util.unitVec2() * this.forceScale);

					// add score
					this.score++;
					this.textScore.addScore();
					this.textScore.setText($"Score: {this.score}");

					// play audio
					
				}
			);

			// attack second one
			GameObject bullet2 = Instantiate(ResourceManager.instance.bulletPrefab, mb.self.position, Quaternion.identity);
			bullet2.transform
			.DOMove(targets.Item2.self.position, this.attackDuration)
			.OnComplete( () =>
				{
					targets.Item2.calculate(this.getBlockRandomValue());
					targets.Item2.anim.SetBool(MathBlock.IS_AIMED, false);
					Destroy(bullet2);
				}
			);

			// re-generate blocks
			this.selectedBlock.calculate(this.getBlockRandomValue());
			mb.calculate(this.getBlockRandomValue());
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

	public System.Tuple<MathBlock, MathBlock> getTargets()
	{
		// choose two blocks to re-generate value
		MathBlock mb, mb2;
		do
		{
			mb = this.blockGrid[Random.Range(0, this.row), Random.Range(0, this.col)];
		}while(mb.isSelected);

		mb.anim.SetBool(MathBlock.IS_AIMED, true);
		Debug.Log($"attacked: {mb.name}");

		// second one
		do
		{
			mb2 = this.blockGrid[Random.Range(0, this.row), Random.Range(0, this.col)];
		} while (mb2.isSelected || mb == mb2);
		
		mb2.anim.SetBool(MathBlock.IS_AIMED, true);
		Debug.Log($"attacked: {mb2.name}");

		return System.Tuple.Create(mb, mb2);
	}

	public Vector3 queryBlockPosition(int r, int c)
	{
		if(r < 0 || r >= this.row || c < 0 || c >= this.col) return Vector3.zero;

		Debug.Log(this.blockTransform[r, c].position);

		return this.blockTransform[r, c].position;
	}

	public void selectBlock(int r, int c)
	{
		if(r < 0 || r >= this.row || c < 0 || c >= this.col) return;

		this.blockGrid[r, c].select();
	}

	private int getBlockRandomValue()
	{
		return Random.Range(1, 13);
	}
}