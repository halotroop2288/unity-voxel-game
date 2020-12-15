using UnityEngine;
using UnityEngine.SceneManagement;

namespace Minecraft {
	public sealed class PauseManu : AbstractMenu {
		[SerializeField] private OptionsMenu optionsMenu;

		public void ResumeGame() {
			this.Close();
		}

		public void OpenOptionsMenu() {
			optionsMenu.Open();
		}

		public void QuitToTitle() {
			SceneManager.LoadScene(0);
		}
	}
}
