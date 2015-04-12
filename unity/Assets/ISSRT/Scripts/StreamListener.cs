/* 
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
using System.Collections.Generic;
using System;

using StreamListenerDelegate = System.Action<StreamListenerArgs>;


/// <summary>
/// Listens for data updates and pushes to subscribed objects
/// </summary>
public class StreamListener : MonoBehaviour {

	//private event StreamListenerDelegate _UpdateValue;
	private Dictionary<string,List<WeakReference>> eventSubscriptions = new Dictionary<string, List<WeakReference>>();

	public void Subscribe(string key, IStreamSubscriber subscriber)
	{
		if(!eventSubscriptions.ContainsKey(key))
		{
			eventSubscriptions.Add(key,new List<WeakReference>());
		}
		eventSubscriptions [key].Add (new WeakReference (subscriber));
	}

	/// <summary>
	/// Updates the value specified in the key portion of input
	/// </summary>
	/// <param name="keyValuePair">string of format "key:value"</param>
	public void UpdateValue(string keyValuePair)
	{
		if (!string.IsNullOrEmpty (keyValuePair) && keyValuePair.Contains (":")) {
			string [] splitKeyValuePair = keyValuePair.Split (':');

			// if there are subscribtions to this event
			if(eventSubscriptions.ContainsKey(splitKeyValuePair[0]))
			{
				List<WeakReference> subscribers = eventSubscriptions[splitKeyValuePair[0]];
				StreamListenerArgs args = new StreamListenerArgs(splitKeyValuePair[0],splitKeyValuePair[1]);

				for(int i = 0; i < subscribers.Count; i++)
				{
					if(subscribers[i].IsAlive)
					{
						IStreamSubscriber subscriber = subscribers[i].Target as IStreamSubscriber;
						subscriber.UpdateValues(args);
					}
					else
					{
						subscribers.RemoveAt(i);
						i--;
					}
				}
			}
			else Debug.LogWarning("Value is not being subscribed to");
		}
		else Debug.LogError("Value is empty, null, or malformed");
	}

	#region subscription event handlers
	public void OnSubscription()
	{
		Debug.Log ("Sucessfully subscribed");
	}

	public void OnSubscriptionError(string errorCodeMessage)
	{
		if (!string.IsNullOrEmpty (errorCodeMessage) && errorCodeMessage.Contains (":")) {
			string [] splitErrorCodeMessage = errorCodeMessage.Split (':');

			if(errorCodeMessage.Length > 1)
			{
				Debug.LogError(string.Format("Received error code #{0} with message: {1}",splitErrorCodeMessage[0],splitErrorCodeMessage[1]));
			}
		}
	}

	public void OnUnsubscription()
	{
		Debug.Log ("Sucessfully unsubscribed");
	}
	#endregion

	void Start()
	{
		// TODO: create better system to do connect/subscribe to server
		Application.ExternalCall ("connectToLiveStream","USLAB000032,USLAB000035,USLAB000033,USLAB000036,USLAB000034,USLAB000037");
	}
}
