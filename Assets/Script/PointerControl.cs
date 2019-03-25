using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PointerControl : MonoBehaviour
{
	public int teamNumber;

	[Header("Keys")]
	public KeyCode fireKey;
	public KeyCode upKey;
	public KeyCode downKey;
	public KeyCode leftKey;
	public KeyCode rightKey;

	[Header("Move")]
	public float stepX;
	public float stepY;
	public float cooldown;
	public float speed;

	// click parameter
	public Vector2 offset;
	public float radius;

	public Vector2 initOffset;

	private Transform self;
	private Rigidbody2D rb2d;
	private Vector2 speedV;

	// Use this for initialization
	private void Start()
	{
		this.self = transform;
		this.rb2d = GetComponent<Rigidbody2D>();
		transform.position = GameManager.instance.queryTeam(this.teamNumber).transform.position + (Vector3)this.initOffset;

		StartCoroutine("cmove");
	}

	private void Update()
	{
		this.click();
	}

	private void FixedUpdate()
	{
		// this.move();
	}

	private IEnumerator cmove()
	{
		while(true)
		{
			this.speedV = Vector2.zero;

			// vertical
			if(Input.GetKey(this.upKey)) this.speedV = Vector2.up;
			else if(Input.GetKey(this.downKey)) this.speedV = Vector2.down;
			// horizontal
			else if(Input.GetKey(this.leftKey)) this.speedV = Vector2.left;
			else if(Input.GetKey(this.rightKey)) this.speedV = Vector2.right;

			if(this.speedV != Vector2.zero)
			{
				this.speedV.x *= this.stepX;
				this.speedV.y *= this.stepY;
				this.self.Translate(this.speedV);
				yield return new WaitForSeconds(this.cooldown);
			}
			else
			{
				yield return null;
			}
		}
	}

	// // for debug
	// private void OnDrawGizmos()
	// {
	// 	Gizmos.color = Color.green;
	// 	Gizmos.DrawSphere((Vector2)transform.position + this.offset, this.radius);
	// }

	public void click()
	{
		if(!Input.GetKeyDown(this.fireKey)) return;

		Collider2D hit = Physics2D.OverlapCircle((Vector2) this.self.position + this.offset, this.radius);
		if(!hit)
		{
			Debug.Log("miss!");
			return;
		}

		MathBlock target = hit.GetComponent<MathBlock>();
		if(!target) return;

		target.select();
	}

	public void move()
	{
		// move
		this.speedV = Vector2.zero;

		// vertical
		if(Input.GetKey(this.upKey)) this.speedV += Vector2.up;
		else if(Input.GetKey(this.downKey)) this.speedV += Vector2.down;

		// horizontal
		if(Input.GetKey(this.leftKey)) this.speedV += Vector2.left;
		else if(Input.GetKey(this.rightKey)) this.speedV += Vector2.right;

		this.speedV *= this.speed;
		rb2d.velocity = this.speedV;

	}
}