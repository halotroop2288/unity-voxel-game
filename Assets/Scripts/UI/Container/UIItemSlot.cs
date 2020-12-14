using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Minecraft {
	public class UIItemSlot : MonoBehaviour {
		public bool isLinked = false;
		private ItemSlot itemSlot;
		public Image slotImage;
		public GameObject slotIcon; // Icon can be a model or a 2D image
		public TextMeshProUGUI slotAmount;

		public bool HasItem {
			get {
				return (itemSlot != null && itemSlot.HasItem);
			}
		}

		public void Link(ItemSlot slot) {
			this.itemSlot = slot;
			isLinked = true;
			itemSlot.LinkUISlot(this);
			this.UpdateSlot();
		}

		public void Unlink() {
			this.itemSlot.UnlinkUISlot();
			this.itemSlot = null;
			this.UpdateSlot();
		}

		public void UpdateSlot() {
			if (this.HasItem) {
				
			}
		}

		public void Clear() {
			slotIcon = null;
			slotAmount.text = "";
			slotAmount.enabled = false;
		}

		private void OnDestroy() {
			if (isLinked)
				itemSlot.UnlinkUISlot();
		}
	}

	[System.Serializable]
	public class ItemSlot {
		public ItemStack stack = null;
		public UIItemSlot uiItemSlot = null;

		public ItemSlot(UIItemSlot uiItemSlot) {
			this.uiItemSlot = uiItemSlot;
			this.uiItemSlot.Link(this);
		}

		public ItemSlot(UIItemSlot uiItemSlot, ItemStack stack) {
			this.stack = stack;
			this.uiItemSlot = uiItemSlot;
			this.uiItemSlot.Link(this);
		}

		public void EmptySlot() {
			stack = null;
			if (uiItemSlot != null)
				uiItemSlot.UpdateSlot();
		}

		public int Take(int amount) {
			if (amount > stack.amount) { // More than
				int ret = stack.amount;
				this.EmptySlot();
				return ret;
			} else if (amount < stack.amount) { // Less than
				stack.amount -= amount;
				uiItemSlot.UpdateSlot();
				return amount;
			} else { // Equal to
				this.EmptySlot();
				return amount;
			}
		}

		public void LinkUISlot(UIItemSlot uiSlot) {
			this.uiItemSlot = uiSlot;
		}

		public void UnlinkUISlot() {
			this.uiItemSlot = null;
		}

		public bool HasItem {
			get {
				return (stack != null);
			}
		}
	}
	public class ItemStack {
		public byte id;
		public int amount;

		public ItemStack(byte id, int amount) {
			this.id = id;
			this.amount = amount;
		}
	}
}
