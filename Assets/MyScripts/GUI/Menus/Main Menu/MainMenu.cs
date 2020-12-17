using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS0649

namespace UnifiedFactorization.GUI {
    public sealed class MainMenu : AbstractMenu {
        [SerializeField] private Button m_DebugWorldButton;

		private void Start() {
#if UNITY_EDITOR
            this.m_DebugWorldButton.gameObject.SetActive(true);
#else
            this.m_DebugWorldButton.gameObject.Destroy();
#endif
		}

		public override void Close() {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

		public void OnCopyrightClicked() {
            Application.OpenURL("https://github.com/Jin-Yuhan/MinecraftClone-Unity");
        }
    }
}