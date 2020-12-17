using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnifiedFactorization.GUI {
    public class MenusCanvas : MonoBehaviour {
        [SerializeField] protected List<AbstractMenu> m_Menus = new List<AbstractMenu>();

        void Start() {
            foreach (Transform t in this.GetComponentInChildren<Transform>()) {
                AbstractMenu menu = t.gameObject.GetComponent<AbstractMenu>();
                if (menu != null)
                    m_Menus.Add(menu);
            }
        }
    }
}
