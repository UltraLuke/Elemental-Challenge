using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PwrSpwnr : MonoBehaviour {

	public List<GameObject> powerups;
	private int spectrum;
	// Use this for initialization
	void Start ()
	{
		spectrum = 3 + (SceneManager.GetActiveScene().buildIndex - 1);
		if(spectrum > powerups.Capacity)
			spectrum = powerups.Capacity;

		GameObject power = GameObject.Instantiate(powerups[Random.Range(0, spectrum)]);
		power.transform.position = transform.position;
	}
}
