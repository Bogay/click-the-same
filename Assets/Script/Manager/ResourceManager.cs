using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// WARNING: no many check for sigleton
public class ResourceManager : MonoBehaviour
{
	// bad singleton
	public static ResourceManager instance { get; private set; }

	public GameObject textPrefab;
	public GameObject mathBlockPrefab;
	public GameObject bulletPrefab;
	public GameObject canvasObject
	{
		get
		{
			if(!this._canvasObject)
			{
				this._canvasObject = GameObject.Find("Canvas");
			}
			return this._canvasObject;
		}
		private set
		{
			this._canvasObject = value;
		}
	}
	private GameObject _canvasObject;

	// Use this for initialization
	void Start()
	{
		if(instance && instance != this)
		{
			Destroy(this);
			return;
		}

		instance = this;
		DontDestroyOnLoad(gameObject);
	}
}