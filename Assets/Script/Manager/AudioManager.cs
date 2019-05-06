using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	private static AudioManager _instance;
	public static AudioManager instance { get { return _instance; } }
	
	// pitch
	[Range(0.5f, 2f)]
	public float pitchRangeLower;
	[Range(0.5f, 2f)]
	public float pitchRangeUpper;

	// volume
	[Range(0.5f, 1f)]
	public float volumeRangeLower;
	[Range(0.5f, 1f)]
	public float volumeRangeUpper;

	public AudioSource bgmSource;
	public AudioSource seSource;

	void Awake()
	{
		if(_instance && _instance != this)
		{
			Destroy(this);
			return;
		}

		_instance = this;

		DontDestroyOnLoad(this.seSource);
		DontDestroyOnLoad(this.bgmSource);
		DontDestroyOnLoad(this);
	}

	public void playSE(AudioClip cp)
	{
		this.seSource.pitch = Random.Range(this.pitchRangeLower, this.pitchRangeUpper);
		this.seSource.volume = Random.Range(this.volumeRangeLower, this.volumeRangeUpper);
		this.seSource.PlayOneShot(cp);
	} 

	public void setBGM(AudioClip cp) => this.bgmSource.clip = cp;

	public void playBGM()
	{
		if(!this.bgmSource.clip)
		{
			Debug.Log("BGM clip not set!");
			return;
		}

		this.bgmSource.Play();
	}

	public void stopBGM()
	{
		if(!this.bgmSource.clip)
		{
			Debug.Log("BGM clip not set!");
			return;
		}
		
		this.bgmSource.Stop();
	}
}