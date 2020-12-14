using UnityEngine;
using UnityEngine.SceneManagement;

namespace Minecraft {
	public sealed class PauseManu : MonoBehaviour {
		[SerializeField] private GameObject pauseMenu;
		[SerializeField] private WorldManager worldManager;

		private void Start() {
			worldManager.InUI = true;
		}

		public void ResumeGame() {
			worldManager.InUI = false;
			pauseMenu.SetActive(false);
		}

		public void OpenOptionsMenu() {
			// TODO
		}

		public void QuitToTitle() {
			SceneManager.LoadScene(0);
		}
	}
}
