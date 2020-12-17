using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnifiedFactorization.GUI {
	public class InventoryMenusCanvas : MenusCanvas {

        void FixedUpdate() {
            foreach (Transform t in this.GetComponentInChildren<Transform>()) {
                AbstractMenu menu = t.gameObject.GetComponent<AbstractMenu>();
                if (menu != null && !m_Menus.Contains(menu))
                    m_Menus.Add(menu);
            }
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                bool closed = false;
                // If escape is pressed, close all active menu(s)
                foreach (AbstractMenu menu in m_Menus) {
                    if (menu.Active) {
                        menu.Close();
                        closed = true;
                    }
                }
                if (!closed) {
                    PauseMenu.Instance.Open(null);
                }
            }
        }
    }
}