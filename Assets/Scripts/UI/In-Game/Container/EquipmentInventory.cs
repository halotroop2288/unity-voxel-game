using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minecraft {
	public class EquipmentInventory : MonoBehaviour {
		PlayerInventory inventory;

		[SerializeField] private UIItemSlot[] slots;

		private void Start() {
			slots = gameObject.GetComponentsInChildren<UIItemSlot>();
		}
	}
}
