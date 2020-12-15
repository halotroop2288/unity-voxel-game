using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Minecraft {
    public class Hotbar : MonoBehaviour {
        [SerializeField] public Sprite[] customSlotIcons = new Sprite[40];

        [SerializeField] private PlayerEntity player;
        [SerializeField] public GameObject slotGridObject;
        [SerializeField] public GameObject slotPrefab;
        [SerializeField] public GameObject highlight;

        [SerializeField] private int slotIndex = 0;
        [SerializeField] private int cooldown = 0;

        [Header("Do not edit")]
        [SerializeField] private UIItemSlot[] m_Slots = new UIItemSlot[40];

        private void Start() {
            if (player == null || player.Inventory == null)
                return;

            // Create UIItemSlots, name them
            for (int i = 0; i < m_Slots.Length; i++) {
                GameObject newSlot = Instantiate(slotPrefab, slotGridObject.transform);
                newSlot.name = "Hotbar Slot " + (i+1);
                m_Slots[i] = newSlot.GetComponent<UIItemSlot>();
            }

            // Link UIItemSlots to PlayerInventory's ItemSlots
            player.Inventory.LinkMainSlots(m_Slots);

            // Give UIItemSlots custom icons
            for (int i = 0; i < customSlotIcons.Length && i < m_Slots.Length; i++) {
                m_Slots[i].slotIcon.sprite = customSlotIcons[i];
                if (m_Slots[i].slotIcon.sprite != null) {
                    m_Slots[i].slotIcon.enabled = true;
                }
            }
        }

        private void Update() {
            if (player == null || player.Inventory == null || CanvasManager.ActiveMenu != null)
                return;

            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if (scroll != 0) {
                slotGridObject.SetActive(true);
                if (scroll > 0)
                    slotIndex--;
                else
                    slotIndex++;

                if (slotIndex > m_Slots.Length - 1)
                    slotIndex = 0;
                if (slotIndex < 0)
                    slotIndex = m_Slots.Length - 1;

                highlight.transform.position = m_Slots[slotIndex].transform.position;
                highlight.SetActive(true);

                cooldown = (int)(100000 * Time.deltaTime);
            }

            // Hide the hotbar after the allotted time.
            if (cooldown > 0) {
                cooldown--;
            } else if (slotGridObject.activeSelf || highlight.activeSelf) {
                slotGridObject.SetActive(false);
                highlight.SetActive(false);
            }
        }
	}
}
