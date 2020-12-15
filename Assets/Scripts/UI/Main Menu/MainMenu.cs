using UnityEngine;

#pragma warning disable CS0649

namespace Minecraft {
    public sealed class MainMenu : AbstractMenu {
        [Header("Menus")]
        [SerializeField] private SelectWorldMenu m_SelectWorldMenu;
        [SerializeField] private OptionsMenu m_OptionsMenu;

        public void Quit() {
            Application.Quit();
        }

        public void Play() {
            m_SelectWorldMenu.Open();
        }

        public void Options() {
            m_OptionsMenu.Open();
        }

		public void OnCopyrightClicked() {
            Application.OpenURL("https://github.com/Jin-Yuhan/MinecraftClone-Unity");
        }
    }
}