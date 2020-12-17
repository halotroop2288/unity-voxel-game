using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace UnifiedFactorization.GUI {
	public class MenuItem : MonoBehaviour {
		private void OnValidate() {
			this.GetComponentInChildren<TextMeshProUGUI>().text = this.gameObject.name;
		}

		public void ChangeScene(int scene) {
			SceneManager.LoadScene(scene);
		}
	}
}
