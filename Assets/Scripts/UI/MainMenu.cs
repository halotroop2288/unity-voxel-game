using UnityEngine;

#pragma warning disable CS0649

namespace Minecraft {
    public sealed class MainMenu : MonoBehaviour {
        [Header("Menus")]
        [SerializeField] private GameObject m_SelectWorldMenu;
        [SerializeField] private GameObject m_OptionsMenu;

        public void Quit() {
            Application.Quit();
        }

        public void Play() {
            m_SelectWorldMenu.SetActive(true);
            gameObject.SetActive(false);
        }

        public void Options() {
            m_OptionsMenu.SetActive(true);
            gameObject.SetActive(false);
		}

		public void OnCopyrightClicked() {
            Application.OpenURL("https://github.com/Jin-Yuhan/MinecraftClone-Unity");
        }
    }
}