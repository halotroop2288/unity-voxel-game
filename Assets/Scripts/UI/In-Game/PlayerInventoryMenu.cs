using Minecraft.ItemsData;
using System.Collections.Generic;
using UnityEngine;

namespace Minecraft {
	public class PlayerInventoryMenu : AbstractMenu {
		[SerializeField] public Sprite[] customSlotIcons = new Sprite[40];

		[SerializeField] private PlayerEntity player;
		[SerializeField] private GameObject mainSlotGrid;
		[SerializeField] private GameObject equipmentSlotGrid; 
		[SerializeField] public GameObject slotPrefab;

		private UIItemSlot[] m_EquipmentSlots = new UIItemSlot[12];
		private UIItemSlot[] m_MainSlots = new UIItemSlot[40];

		private void Start() {
			if (player == null || player.Inventory == null)
				return;

			// Create UIItemSlots, name them
			m_EquipmentSlots = equipmentSlotGrid.GetComponents<UIItemSlot>();
			for (int i = 0; i < m_MainSlots.Length; i++) {
				GameObject newSlot = Instantiate(slotPrefab, mainSlotGrid.transform);
				newSlot.name = "Slot " + i;
				m_MainSlots[i] = newSlot.GetComponent<UIItemSlot>();
			}

			// Link UIItemSlots to PlayerInventory's ItemSlots
			player.Inventory.LinkEquipmentSlots(m_EquipmentSlots);
			player.Inventory.LinkMainSlots(m_MainSlots);

			// Give UIItemSlots custom icons
			for (int i = 0; i < customSlotIcons.Length && i < m_MainSlots.Length; i++) {
				m_MainSlots[i].slotIcon.sprite = customSlotIcons[i];
				if (m_MainSlots[i].slotIcon.sprite != null) {
					m_MainSlots[i].slotIcon.enabled = true;
				}
			}
		}

		private void OnValidate() {
			if (customSlotIcons.Length > m_MainSlots.Length) {
				customSlotIcons = new Sprite[m_MainSlots.Length];
			}
		}

		private void Update() {
			if (!CanvasManager.ActiveMenu == this)
				return;

			if (Input.GetKeyDown("e")) {
				this.Close();
			}
		}
	}

	public class PlayerInventory {
		private ItemSlot[] m_EquipmentSlots = new ItemSlot[12];
		private ItemSlot[] m_MainSlots = new ItemSlot[40];
		private PlayerEntity player;

		public PlayerInventory(PlayerEntity player) {
			this.player = player;
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
