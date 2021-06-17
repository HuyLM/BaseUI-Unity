using UnityEngine;

namespace GameSystem.Common.Utilities {
	public class DontDestroyOnLoad : MonoBehaviour {
		protected virtual void Awake() {
			DontDestroyOnLoad(gameObject);
		}
	}
}