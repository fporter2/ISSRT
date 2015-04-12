using UnityEngine;
using System.Collections;

public class ButtonFaceOut : MonoBehaviour {

	[SerializeField]
	Transform lookAwayFrom;
	
	// Update is called once per frame
	void Update () {
		if (lookAwayFrom) {
			transform.LookAt(lookAwayFrom.position);
		}
	}
}
