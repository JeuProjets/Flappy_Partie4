using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deplaceNuage : MonoBehaviour {

	public float vitesseX;
	// Use this for initialization
	void Start () {

		vitesseX = 0.01f;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.Translate (vitesseX, 0, 0);
		
	}
}
