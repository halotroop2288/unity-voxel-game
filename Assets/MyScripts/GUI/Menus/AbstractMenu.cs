using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnifiedFactorization.GUI {
    public abstract class AbstractMenu : MonoBehaviour {
		protected AbstractMenu m_ParentMenu;

		public bool Active {
			get {
				return this.gameObject.activeSelf;
			}
			set {
				if (value) {
					if (!this.gameObject.activeSelf)
						this.Open(null);

				} else {
					if (this.gameObject.activeSelf)
						this.Close();
				}
			}
		}

		// Set the parent menu if one is supplied, close it, then open this one.
		public virtual void Open(AbstractMenu parentMenu) {
			if (parentMenu != null)
				this.m_ParentMenu = parentMenu;
			if (this.m_ParentMenu != null)
				this.m_ParentMenu.gameObject.SetActive(false);
			this.gameObject.SetActive(true);
		}

		// Go back to previous menu if it exists, or just close this one.
		public virtual void Close() {
			this.gameObject.SetActive(false);
			if (this.m_ParentMenu != null)
				this.m_ParentMenu.gameObject.SetActive(true);
		}
	}
}
