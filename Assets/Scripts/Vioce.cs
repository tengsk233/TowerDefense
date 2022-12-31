using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Vioce : MonoBehaviour {
	public GameObject obje;
	GameObject obj=null;
	// Use this for initialization
	void Start () {
		
		GameObject[] audioes = GameObject.FindGameObjectsWithTag ("Audio");
		if (audioes.Length == 2) {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (this.gameObject);
	}
	

}
