using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Minecraft;

namespace UnifiedFactorization.GUI {
	public class UIItemSlot : MonoBehaviour {
		public bool isLinked = false;
		private ItemSlot itemSlot;
		public Image slotImage;
		public Image slotIcon;
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

		// Used for slots which should only allow certain types of items
		public virtual bool ShouldAcceptStack(ItemStack stack) {
			return true;
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

	public class UIEquipmentSlot : UIItemSlot {
		private EquipmentSlot m_Slot;

		public EquipmentSlot Slot => m_Slot;
		
		public override bool ShouldAcceptStack(ItemStack stack) {
			return (stack.GetItem is EquipmentItem equipment && equipment.Slot == this.Slot);
		}
	}

	[System.Serializable]
	public class ItemSlot {
		private ItemStack m_Stack = null;
		private UIItemSlot m_UIItemSlot = null;

		public ItemStack Stack => m_Stack;
		public UIItemSlot UISlot => m_UIItemSlot;

		public ItemSlot(UIItemSlot uiItemSlot) {
			this.m_UIItemSlot = uiItemSlot;
			this.m_UIItemSlot.Link(this);
		}

		public ItemSlot(UIItemSlot uiItemSlot, ItemStack stack) {
			this.m_Stack = stack;
			this.m_UIItemSlot = uiItemSlot;
			this.m_UIItemSlot.Link(this);
		}

		public void EmptySlot() {
			m_Stack = null;
			if (m_UIItemSlot != null)
				m_UIItemSlot.UpdateSlot();
		}

		public int Take(int amount) {
			if (amount > m_Stack.Count) { // More than
				int ret = m_Stack.Count;
				this.EmptySlot();
				return ret;
			} else if (amount < m_Stack.Count) { // Less than
				m_Stack.Decrement(amount);
				m_UIItemSlot.UpdateSlot();
				return amount;
			} else { // Equal to
				this.EmptySlot();
				return amount;
			}
		}

		public void LinkUISlot(UIItemSlot uiSlot) {
			this.m_UIItemSlot = uiSlot;
		}

		public void UnlinkUISlot() {
			this.m_UIItemSlot = null;
		}

		public bool HasItem {
			get {
				return (m_Stack != null);
			}
		}
	}

	public class ItemStack {
		public static readonly ItemStack EMPTY = new ItemStack(null, 0);	

		private Item m_Item;
		private int m_Count;
		private int m_Damage;
		private Entity m_Holder;

		public Item GetItem => m_Item;
		public int Count => m_Count;
		public int Damage => m_Damage;
		public Entity Holder => m_Holder;
		public bool Empty {
			get {
				if (this == EMPTY)
					return true;
				else if (this.GetItem != null)
					return this.Count <= 0;
				else
					return true;
			}
		}

		public ItemStack(Item item, int count) {
			this.m_Item = item == null ? null : item;
			if (item is StackableItem stackable) {
				// Ensure count does not exceed maximum
				this.m_Count = count < stackable.MaxCount ? count : stackable.MaxCount;
			}
			if (item is BreakableItem) {
				this.m_Damage = 0;
			}
		}

		// Adds damage to the stack's damage value
		// Returns whether damage reached maximum (break item)
		// Fails if stack's item is not breakable.
		public bool DamageStack(int damageAmount, PlayerEntity player) {
			if (this.m_Item is BreakableItem) {
				BreakableItem breakable = (BreakableItem) m_Item;
				// Handle enchantments?

				if (damageAmount <= 0) {
					return false;
				}

				int i = Damage + damageAmount;
				this.m_Damage = i;
				return i >= breakable.MaxDamage;
			} else {
				return false;
			}
		}

		public void Increment(int amount) {
			this.m_Count += amount;
		}

		public void Decrement(int amount) {
			this.m_Count -= amount;
		}

		public void SetItem(Item item, int amount) {
			this.m_Item = item;
			this.m_Count = amount;
		}
	}
}
