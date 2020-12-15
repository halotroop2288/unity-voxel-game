using UnityEngine;
using UnityEngine.SceneManagement;

namespace Minecraft {
	public sealed class PauseManu : AbstractMenu {
		[SerializeField] private OptionsMenu optionsMenu;

		public void ResumeGame() {
			this.Open();
		}

		public void OpenOptionsMenu() {
			optionsMenu.Open();
		}

		public void QuitToTitle() {
			SceneManager.LoadScene(0);
		}
	}
}
