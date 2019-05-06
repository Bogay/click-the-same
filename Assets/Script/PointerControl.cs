using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PointerControl : MonoBehaviour
{
	public int teamNumber;
	public PointerSetting setting;

	private KeyCode fireKey { get { return this.setting.fireKey; } }
	private KeyCode upKey { get { return this.setting.upKey; } }
	private KeyCode downKey { get { return this.setting.downKey; } }
	private KeyCode leftKey { get { return this.setting.leftKey; } }
	private KeyCode rightKey { get { return this.setting.rightKey; } }

	private float cooldown { get { return this.setting.cooldown; } }
	private Vector2 offset { get { return this.setting.offset; } }

	private int row;
	private int col;

	private Vector3 target;
	private Transform self;
	private Rigidbody2D rb2d;
	private Container container;

	// Use this for initialization
	private void Start()
	{
		this.self = transform;
		this.rb2d = GetComponent<Rigidbody2D>();

		this.container = GameManager.instance.queryTeam(this.teamNumber);
		this.row = this.col = 0;

		this.self.position = this.container.queryBlockPosition(0, 0) + (Vector3)this.offset;
		this.target = this.self.position;

		StartCoroutine(this.cmove());
	}

	private void Update()
	{
		this.self.position = Vector3.Lerp(this.self.position, this.target, 20 * Time.deltaTime);
		this.click();
	}

	private IEnumerator cmove()
	{
		while(true)
		{
			int moved = 0;

			// vertical
			if(Input.GetKey(this.upKey) && this.row > 0)
			{
				this.row--; 
				moved = 1;
			}
			else if(Input.GetKey(this.downKey) && this.row < this.container.row - 1)
			{
				this.row++;
				moved = 1;
			}
			// horizontal
			else if(Input.GetKey(this.leftKey) && this.col > 0)
			{
				this.col--; 
				moved = 1;
			}
			else if(Input.GetKey(this.rightKey) && this.col < this.container.col - 1)
			{
				this.col++; 
				moved = 1;
			}

			if(moved != 0)
			{
				this.target = this.container.queryBlockPosition(this.row, this.col) + (Vector3)this.offset;
				yield return new WaitForSeconds(this.cooldown);
			}
			else
			{
				yield return null;
			}
		}
	}

	public void click()
	{
		if(!Input.GetKeyDown(this.fireKey)) return;
		this.container.selectBlock(this.row, this.col);
	}
}