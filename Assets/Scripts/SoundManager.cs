using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour {

	public List<AudioClip> audios = new List<AudioClip>();

	public AudioMixerSnapshot mainMenu;
	public AudioMixerSnapshot inGame;
    public AudioMixerSnapshot paused;

    private AudioSource src;

    /*private bool isInGame;
	private bool lastIsInGame;*/

	void Start ()
    {
		if(SceneManager.GetActiveScene().buildIndex >= 1)
		{
            SetAudioSnapshot(1);
		}
		else if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            SetAudioSnapshot(0);
		}

        src = GetComponent<AudioSource>();
	}

	public void SetAudioSnapshot(int select)
	{
        switch (select)
        {
            case 0:
                mainMenu.TransitionTo(0f);
                break;

            case 1:
                inGame.TransitionTo(0f);
                break;

            case 2:
                paused.TransitionTo(0f);
                break;
        }
	}

	public AudioClip Sound(int sound)
	{
		return audios[sound];
	}

    public void ClickSound(int sound)
    {
        src.PlayOneShot(Sound(sound));
    }
}
