using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnifiedFactorization.GUI {
    public class MainMenusCanvas : MenusCanvas {
        void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                // If escape is pressed, close all active menu(s)
                foreach (AbstractMenu menu in m_Menus) {
                    menu.Active = false;
                }
            }
        }
    }
}