using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Minecraft;

namespace UnifiedFactorization.GUI {
	public class PlayerInventoryMenu : InventoryMenu {
		[SerializeField] public Sprite[] customSlotIcons = new Sprite[40];

		[SerializeField] private GameObject m_MainSlotGrid;
		[SerializeField] private GameObject m_EquipmentSlotGrid;
		[SerializeField] private GameObject m_SlotPrefab;

		private UIItemSlot[] m_EquipmentSlots = new UIItemSlot[12];
		private UIItemSlot[] m_MainSlots = new UIItemSlot[40];

		[SerializeField] [Range(0, 40)] private int m_SlotIndex = 0;
		[SerializeField] private int m_Cooldown = 0;

		[SerializeField] private GameObject highlight;

		private bool m_ScrollOpened = false;

		public int SlotIndex => m_SlotIndex;

		public int Cooldown {
			get;
			private set;
		}

		private void Start() {
			// Create UIItemSlots, name them, Give them custom icons
			m_EquipmentSlots = m_EquipmentSlotGrid.GetComponents<UIItemSlot>();
			for (int i = 0; i < m_MainSlots.Length; i++) {
				GameObject newSlot = Instantiate(m_SlotPrefab, m_MainSlotGrid.transform);
				newSlot.name = "Slot " + i;
				switch (i) {
					case 0:
						newSlot.name += " (Sword)";
						break;
					case 1:
						newSlot.name += " (Pickaxe)";
						break;
					case 2:
						newSlot.name += " (Axe)";
						break;
					case 3:
						newSlot.name += " (Shovel)";
						break;
					case 4:
						newSlot.name += " (Hoe)";
						break;
					default:
						break;
				}
				m_MainSlots[i] = newSlot.GetComponent<UIItemSlot>();
			}

			// Link UIItemSlots to PlayerInventory's ItemSlots

		}

		private void OnValidate() {
			if (customSlotIcons.Length > m_MainSlots.Length) {
				customSlotIcons = new Sprite[m_MainSlots.Length];
			}
		}

		private void Update() {
			if (Input.GetKeyDown("e")) {
				this.Close();
			}

			// Hide the menu after the allotted time.
			if (this.Cooldown > 0) {
				this.Cooldown--;
			} else if (m_ScrollOpened) {
				m_MainSlotGrid.SetActive(false);
				m_ScrollOpened = false;
			}
		}

		private void OnEnable() {
			// Ensure slot grid shows up when opened.
			// Even if it was hidden by the scroll sequence.
			m_MainSlotGrid.SetActive(true);
		}

		private void OnDisable() {
			// Ensure slot grid gets disabled when this is closed.	
			m_MainSlotGrid.SetActive(false);
		}

		// Used to scroll through item slots during active gameplay
		public void ScrollSlots(bool backward) {
			m_MainSlotGrid.SetActive(true);

			if (backward)
				m_SlotIndex--;
			else
				m_SlotIndex++;

			if (m_SlotIndex > m_MainSlots.Length - 1)
				m_SlotIndex = 0;
			if (m_SlotIndex < 0)
				m_SlotIndex = m_MainSlots.Length - 1;

			highlight.transform.SetParent(m_MainSlots[m_SlotIndex].gameObject.transform);

			m_Cooldown = (int) (100000 * Time.deltaTime);
			m_ScrollOpened = true;
		}
	}

	public class PlayerInventory {
		private ItemSlot[] m_EquipmentSlots = new ItemSlot[12];
		private ItemSlot[] m_MainSlots = new ItemSlot[40];
		private PlayerEntity player;

		public PlayerInventory(PlayerEntity player) {
			this.player = player;
		}

		public void SetItem(int index, Item item, int amount) {
			this.m_MainSlots[index].Stack.SetItem(item, amount);
		}

		public void SetItem(int index, Item item) {
			this.SetItem(index, item, 1);
		}

		public void LinkMainSlots(UIItemSlot[] mainSlots) {
			for (int i = 0; i < mainSlots.Length && i < m_MainSlots.Length; i++) {
				m_MainSlots[i].LinkUISlot(mainSlots[i]);
			}
		}

		public void LinkEquipmentSlots(UIItemSlot[] equipmentSlots) {
			for (int i = 0; i < equipmentSlots.Length && i < m_EquipmentSlots.Length; i++) {
				m_EquipmentSlots[i].LinkUISlot(equipmentSlots[i]);
			}
		}

		public ItemSlot[] EquipmentSlots {
			get {
				return m_EquipmentSlots;
			}
			set {
				m_EquipmentSlots = value;
			}
		}

		public ItemSlot[] MainSlots {
			get {
				return m_MainSlots;
			}

			set {
				m_MainSlots = value;
			}
		}
	}
}
