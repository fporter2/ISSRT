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

using System;

/// <summary>
/// StreamListener event argument.
/// </summary>
public class StreamListenerArgs : EventArgs
{
	private string _key;
	private string _rawValue;
	
	public StreamListenerArgs(string key, string rawValue)
	{
		this._key = key;
		this._rawValue = rawValue;
	}
	
	public string Key
	{
		get { return this._key; }
	}
	
	public string RawValue
	{
		get { return this._rawValue; }
	}
}