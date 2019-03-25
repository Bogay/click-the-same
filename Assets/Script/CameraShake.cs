using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	public float forceScale;
	public Rigidbody2D rb2d { get; private set; }

	private Vector3 origin;
	private Vector3 last;
	private Vector3 next;
	private Transform self;

	private List<Vector2> forceList;

	private void Awake()
	{
		this.forceList = new List<Vector2>();
	}

	void Start()
	{
		this.self = transform;
		this.last = this.self.position;
		this.origin = this.self.position;
		this.rb2d = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		// this.next = Vector3.Lerp(this.self.position, this.origin, 5 * Time.deltaTime); // - (last - this.self.position) * this.forceScale;
		// this.next.z = -10;
		// this.last = this.self.position;
		// this.self.position = this.next;

		this.rb2d.velocity = Vector2.Lerp(this.rb2d.velocity, (Vector2)(this.origin - this.self.position) * (this.forceScale), 5 * Time.deltaTime); // * Mathf.Min(3, (this.origin - this.self.position).magnitude) / 3);
		// for(int i=0 ; i<this.forceList.Count ; i++)
		// {
		// 	this.rb2d.velocity += this.forceList[i];
		// }
		// this.forceList.Clear();
	}

	public void addForce(Vector2 force)
	{
		// this.forceList.Add(force);
		this.rb2d.velocity += force;
		Debug.Log(this.rb2d.velocity);
	}
}