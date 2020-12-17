using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnifiedFactorization.GUI {
	public class PauseMenu : AbstractMenu {
		#region Singleton
		public static PauseMenu Instance {
			get;
			private set;
		}

		private void Awake() {
			Instance = this;
		}
		#endregion
	}
}