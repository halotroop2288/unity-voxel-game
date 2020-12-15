using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minecraft {
	public sealed class CanvasManager : MonoBehaviour {
		#region Singleton
		public static CanvasManager Instance {
			get;
			private set;
		}

		private void Awake() {
			if (Instance != null) {
				Debug.LogWarning("More than one instance of CanvasManager found!");
				return;
			}

			Instance = this;
		}
		#endregion

		[SerializeField] private GameObject hud;

		[SerializeField] private List<AbstractMenu> menus;

		[SerializeField] private AbstractMenu m_ActiveMenu;

		// This is called whenever a script extending from AbstractMenu is enabled or activated
		// Setting this to null should close any and all open menus.
		public static AbstractMenu ActiveMenu {
			get {
				return Instance.m_ActiveMenu;
			}
			set {
				Instance.m_ActiveMenu = value;
				Instance.CheckOpenMenus();
			}
		}

		// Ensures only one menu is open at a time
		private void CheckOpenMenus() {
			List<AbstractMenu> uncheckedMenus = new List<AbstractMenu>(menus);
			uncheckedMenus.Remove(m_ActiveMenu);

			foreach (AbstractMenu menu in uncheckedMenus) {
				if (menu.Active)
					menu.gameObject.SetActive(false);
			}
		}

		private void Start() {
			// Find the first active menu (The main menu, or the loading menu.)
			foreach (AbstractMenu menu in menus)
				if (menu.Active)
					this.m_ActiveMenu = menu;
		}

		private void Update() {
			// Only true if in-game.
			if (this.hud != null) {
				if (ActiveMenu != null) {
					hud.SetActive(false);
				} else if (!hud.activeSelf) {
					hud.SetActive(true);
				}
			}
		}
	}
}
