using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class OrbitalCalculations : MonoBehaviour {

	public Text labelDate;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (labelDate)
			labelDate.text = DateTime.UtcNow.ToJulian ().ToString();
	}
}

