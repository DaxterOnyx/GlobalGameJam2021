using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //Audio players
    public AudioSource effectSource;
    public AudioSource musicSource;

    //Pitch
    public float LowPitchRange = 0.95f;
	public float HighPitchRange = 0.95f;

	public AudioClip flip;
	public AudioClip footstep1;
	public AudioClip footstep2;
	public AudioClip low_trigger;
	public AudioClip maigoni;
	public AudioClip trigger;
	public AudioClip trigger2;
	public AudioClip ui_move;
	public AudioClip ui_select;
	
	
	//Singleton
	public static SoundManager Instance = null;

	private void Awake()
	{
		// If there is not already an instance of SoundManager, set it to this.
		if (Instance == null)
		{
			Instance = this;
		}
		//If an instance already exists, destroy whatever this object is to enforce the singleton.
		else if (Instance != this)
		{
			Destroy(gameObject);
		}

		//Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
		DontDestroyOnLoad(gameObject);
	}

	public void PlayEffect(AudioClip clip)
    {
		effectSource.clip = clip;
		effectSource.Play();
    }

	public void PlayMusic(AudioClip clip)
    {
		musicSource.clip = clip;
		musicSource.Play();
    }
}
