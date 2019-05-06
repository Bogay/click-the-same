using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
	public float forceScale;
	public float speed;
	public float radius;
	public float eps;
	public Rigidbody2D rb2d { get; private set; }

	private Vector3 origin;
	private Vector3 current;
	private Vector3 target;
	private Transform self;

	void Start()
	{
		this.self = transform;
		this.origin = this.self.position;
		this.current = this.origin;
		this.rb2d = GetComponent<Rigidbody2D>();

		this.updateTarget();
		StartCoroutine(this.toTarget());
		StartCoroutine(this.shake());
	}

	private void FixedUpdate()
	{
		this.self.position = Vector3.Lerp(this.self.position, this.current, 5 * Time.deltaTime);
		// Debug.Log($"target: {this.target}");
		// Debug.Log($"current: {this.current}");
	}

	public void addForce(Vector2 force)
	{
		this.rb2d.velocity += force;
		Debug.Log(this.rb2d.velocity);
	}

	private IEnumerator shake()
	{
		while(true)
		{
			this.rb2d.velocity = Vector2.Lerp(this.rb2d.velocity, (Vector2)(this.current - this.self.position) * (this.forceScale), 5 * Time.deltaTime);
			yield return null;
		}
	}

	private IEnumerator toTarget()
	{
		while(true)
		{
			Vector3 step = (this.target - this.current).normalized * this.speed;
			// Debug.Log($"target: {this.target}");
			// Debug.Log($"current: {this.current}");
			// Debug.Log($"Step: {step}");

			while(Vector3.Distance(this.target, this.current) > this.eps)
			{
				this.current += step * Time.deltaTime;

				yield return null;
			}

			this.updateTarget();
		}
	}

	private void updateTarget()
	{
		this.target = this.origin + (Vector3)Util.unitVec2() * Random.Range(this.radius / 2, this.radius);

		while(Vector3.Distance(this.target, this.current) < eps)
		{
			this.target = this.origin + (Vector3)Util.unitVec2() * Random.Range(this.radius / 2, this.radius);
		}
	}
}