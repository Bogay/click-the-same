using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PointerControl : MonoBehaviour
{
	// keys
	public KeyCode fireKey;
	public KeyCode upKey;
	public KeyCode downKey;
	public KeyCode leftKey;
	public KeyCode rightKey;

	public float speed;
	// click parameter
	public Vector2 offset;
	public float radius;

	private Transform self;
	private Rigidbody2D rb2d;
	private Vector2 speedV;

	// Use this for initialization
	private void Start()
	{
		this.self = transform;
		this.rb2d = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		this.click();
	}

	private void FixedUpdate()
	{
		this.move();
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

		Collider2D hit = Physics2D.OverlapCircle((Vector2)this.self.position + this.offset, this.radius);
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