/*
 * based on: http://wiki.unity3d.com/index.php/Toolbox
 * license: http://creativecommons.org/licenses/by-sa/3.0/
 */

using UnityEngine;

/// <summary>
/// </summary>
public class Toolbox : Singleton<Toolbox> {
	protected Toolbox () {} // guarantee this will be always a singleton only - can't use the constructor!
	
	void Awake () {
		// Your initialization code here
	}
	
	// (optional) allow runtime registration of global objects
	static public T RegisterComponent<T> () where T: Component {
		return Instance.GetOrAddComponent<T>();
	}
}