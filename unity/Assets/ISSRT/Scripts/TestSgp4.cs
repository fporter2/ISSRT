using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using sgp4;

public class TestSgp4 : MonoBehaviour, IStreamSubscriber {

	float scale = 1/12742.0f;
	float scaleAltitude = 1 + 1/12742.0f;

	[SerializeField]
	MeshRenderer earthRenderer;

	[SerializeField]
	Transform sun;

	[SerializeField]
	Transform iss;

	[SerializeField]
	Transform sunPos;

	// sun label fields
	[SerializeField]
	Text latField;
	[SerializeField]
	Text longField;
	[SerializeField]
	Text azField;
	
	// iss label fields
	[SerializeField]
	Text latField_iss;
	[SerializeField]
	Text longField_iss;
	[SerializeField]
	Text azField_iss;

	DateTime time;

	public float timeScale = 1;

	public Vector3 issPos = Vector3.zero;
	public Vector3 issVol = Vector3.zero;
	private Eci issPosition = null;

	#region IStreamSubscriber implementation

	public void UpdateValues (StreamListenerArgs eventArgs)
	{
		float val = 0;
		switch (eventArgs.Key) {
		case("USLAB000032"):
			if(float.TryParse(eventArgs.RawValue,out val))
			   issPos.x = val;
			break;
		case("USLAB000033"):
			if(float.TryParse(eventArgs.RawValue,out val))
				issPos.y = val;
			break;
		case("USLAB000034"):
			if(float.TryParse(eventArgs.RawValue,out val))
				issPos.z = val;
			break;
		case("USLAB000035"):
			if(float.TryParse(eventArgs.RawValue,out val))
				issVol.x = val;
			break;
		case("USLAB000036"):
			if(float.TryParse(eventArgs.RawValue,out val))
				issVol.y = val;
			break;
		case("USLAB000037"):
			if(float.TryParse(eventArgs.RawValue,out val))
				issVol.z = val;
			break;
		default:
			break;
		}

		if (issPos.x != 0 && issPos.y != 0 && issPos.z != 0 /*&&
		    issVol.x != 0 && issVol.y != 0 && issVol.z != 0*/) {
			issPosition = new Eci(DateTime.UtcNow, new Vector4d(issPos.x,issPos.y,issPos.z,issPos.magnitude));
			// new Vector4d(issVol.x,issVol.y,issVol.z,issVol.magnitude) ); 
		}
	}

	#endregion

	// Use this for initialization
	void Start () {
		time = DateTime.UtcNow;
		StreamListener sl = Toolbox.RegisterComponent<StreamListener>();
		sl.Subscribe("USLAB000032", this);
		sl.Subscribe("USLAB000033", this);
		sl.Subscribe("USLAB000034", this);

		sl.Subscribe("USLAB000035", this);
		sl.Subscribe("USLAB000036", this);
		sl.Subscribe("USLAB000037", this);

		{
			issPosition = new Eci(DateTime.UtcNow, new Vector4d(issPos.x,issPos.y,issPos.z,issPos.magnitude));
			                     // new Vector4d(issVol.x,issVol.y,issVol.z,issVol.magnitude) ); 
		}
	}
	
	// Update is called once per frame
	void Update () {
		SolarPosition sp = new SolarPosition ();
		time = time.AddDays(Time.deltaTime * timeScale);
		Vector4d v = null;
		Eci pos = sp.FindPosition (time);
		var t = pos.ToGeodetic ();
		//Debug.Log (t.ToString());
		issVol = new Vector3 ((float)Util.RadiansToDegrees(t.latitude), (float)Util.RadiansToDegrees(t.longitude), (float)t.altitude);

		if (sun) {
			
			Eci e = new Eci(DateTime.UtcNow,t);

			if(latField && longField && azField)
			{
				latField.text = Util.RadiansToDegrees(t.latitude).ToString(".00");
				longField.text = Util.RadiansToDegrees(t.longitude).ToString(".00");
				azField.text = t.altitude.ToString(".00");
			}

			sun.transform.localPosition = this.transform.forward.normalized * (float)t.altitude * scale + this.transform.forward.normalized * earthRenderer.bounds.extents.z;
			sun.transform.RotateAround(this.transform.position,transform.right,-(float)Util.RadiansToDegrees(t.latitude));
			sun.transform.RotateAround(this.transform.position,transform.up,-(float)Util.RadiansToDegrees(t.longitude));

			sun.transform.LookAt(transform.position);

			if(sunPos)
			{
				sunPos.transform.localPosition = (sun.transform.position - this.transform.position).normalized * 0.5f;
				Debug.DrawLine(sun.transform.position, this.transform.position);
			}
		}
		
		#if UNITY_EDITOR
		// for testing purposes
		issPosition = new Eci(DateTime.UtcNow,issPos.x,issPos.y,issPos.z);
		#endif
		if (iss && issPosition != null) {
			t = issPosition.ToGeodetic();

			if(latField_iss && longField_iss && azField_iss)
			{
				latField_iss.text = Util.RadiansToDegrees(t.latitude).ToString(".00");
				longField_iss.text = Util.RadiansToDegrees(t.longitude).ToString(".00");
				azField_iss.text = t.altitude.ToString(".00");
			}

			
			iss.transform.localPosition = this.transform.forward.normalized * (float)t.altitude * scale + this.transform.forward.normalized * earthRenderer.bounds.extents.z;
			iss.transform.RotateAround(this.transform.position,transform.right,-(float)Util.RadiansToDegrees(t.latitude));
			iss.transform.RotateAround(this.transform.position,transform.up,-(float)Util.RadiansToDegrees(t.longitude));
			iss.transform.rotation = Quaternion.identity;


			t = issPosition.ToGeodetic ();
			//Debug.Log ("ISS:" + t.ToString());
		}
	}

	public void OpenURL()
	{
		//Application.OpenURL ("");
		Application.ExternalEval("window.open('https://github.com/fporter2/ISSRT','Information')");
	}
}
