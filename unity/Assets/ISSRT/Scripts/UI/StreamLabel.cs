﻿/* 
   Copyright 2015 Forrest Porter

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

     http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
 */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StreamLabel : MonoBehaviour, IStreamSubscriber {

	[SerializeField]
	Text labelToUpdate;

	[SerializeField]
	string puiKey;

	// Use this for initialization
	void Start () {
		if (labelToUpdate == null) 
		{
			labelToUpdate = GetComponent<Text> ();
		}

		if (labelToUpdate != null) 
		{
			if(!string.IsNullOrEmpty(puiKey))
			{
				StreamListener sl = Toolbox.RegisterComponent<StreamListener>();
				sl.Subscribe(puiKey, this);
			}
			else Debug.LogError("PUI key is not set");
		}
		else Debug.LogError("Text to update is not set");
	}
	
	#region IStreamSubscriber implementation
	
	public void UpdateValues (StreamListenerArgs eventArgs)
	{
		if (labelToUpdate != null)
		{
			double v = 0;
			if(double.TryParse(eventArgs.RawValue, out v))
			{
				labelToUpdate.text = v.ToString(".00");
			}
		}
	}
	
	#endregion
}
