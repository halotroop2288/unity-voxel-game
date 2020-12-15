using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minecraft {
    public abstract class AbstractMenu : MonoBehaviour {
		[Header("Menus")]
		[SerializeField] protected AbstractMenu m_ParentMenu;

		public bool Active {
			get {
				return this.gameObject.activeSelf;
			}
		}

		public void Open() {
			CanvasManager.ActiveMenu = this;
			this.gameObject.SetActive(true);
		}

		public void Close() {
			if (CanvasManager.ActiveMenu == this) {
				CanvasManager.ActiveMenu = null;
			}
		}

		public void OnBackButtonPressed() {
			m_ParentMenu.Open();
		}
	}
}
