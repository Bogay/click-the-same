using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
	public MathBlock target;
	public Vector3 targetPos;
	public float speed;

	private Transform self;

	// Use this for initialization
	void Start()
	{
		this.self = transform;
		this.self.DOMove(this.targetPos, Vector3.Distance(this.targetPos, this.self.position) / speed).OnComplete(() => Destroy(gameObject));
	}
}