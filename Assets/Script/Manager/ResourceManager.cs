using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// WARNING: no many check for sigleton
public class ResourceManager : MonoBehaviour
{
	// bad singleton
	private static ResourceManager _instance;
	public static ResourceManager instance
	{
		get
		{
			// if(!_instance)
			// {
			// 	_instance = FindObjectOfType<ResourceManager>();
			// 	if(!_instance)
			// 	{
			// 		_instance = new GameObject("Resource Manager").AddComponent<ResourceManager>();
			// 	}
			// 	DontDestroyOnLoad(_instance);
			// }

			return _instance;
		}
	}

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
		if(_instance && _instance != this)
		{
			Destroy(this);
			return;
		}

		_instance = this;

		// preload
		// this.textPrefab = Resources.Load<GameObject>("Text Root");
		// this.mathBlockPrefab = Resources.Load<GameObject>("Block");
		// this.bulletPrefab = Resources.Load<GameObject>("Bullet");

		DontDestroyOnLoad(this);
	}
}