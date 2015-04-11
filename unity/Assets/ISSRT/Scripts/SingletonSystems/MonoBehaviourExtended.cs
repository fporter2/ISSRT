/*
 * based on: http://wiki.unity3d.com/index.php/GetOrAddComponent
 * license: http://creativecommons.org/licenses/by-sa/3.0/
 */

using UnityEngine;

/// <summary>
/// </summary>
static public class MethodExtensionForMonoBehaviourTransform {
	/// <summary>
	/// Gets or add a component. Usage example:
	/// BoxCollider boxCollider = transform.GetOrAddComponent<BoxCollider>();
	/// </summary>
	static public T GetOrAddComponent<T> (this Component child) where T: Component {
		T result = child.GetComponent<T>();
		if (result == null) {
			result = child.gameObject.AddComponent<T>();
		}
		return result;
	}
}