using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Minecraft {
    public class Hotbar : MonoBehaviour {
        [SerializeField] public GameObject slotPrefab;
        [SerializeField] public GameObject slotGrid;
        [SerializeField] public UIItemSlot[] slots = new UIItemSlot[40];
        [SerializeField] public Sprite[] customSlotIcons = new Sprite[40];
        [SerializeField] public GameObject highlight;

        [SerializeField] private int slotIndex = 0;
        [SerializeField] private int cooldown = 0;

        private void Start() {
            for (int i = 0; i < slots.Length; i++) {
                GameObject newSlot = Instantiate(slotPrefab, slotGrid.transform);
                newSlot.name = "Hotbar Slot " + i;
                slots[i] = newSlot.GetComponent<UIItemSlot>();
            }
            for (int i = 0; i < customSlotIcons.Length && i < slots.Length; i++) {
                slots[i].slotIcon.sprite = customSlotIcons[i];
                if (slots[i].slotIcon.sprite != null) {
                    slots[i].slotIcon.enabled = true;
                }
            }
        }

		private void OnValidate() {
            if (customSlotIcons.Length > slots.Length) {
                customSlotIcons = new Sprite[slots.Length];
            }
		}

		private void Update() {
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if (scroll != 0) {
                slotGrid.SetActive(true);
                if (scroll > 0)
                    slotIndex--;
                else
                    slotIndex++;

                if (slotIndex > slots.Length - 1)
                    slotIndex = 0;
                if (slotIndex < 0)
                    slotIndex = slots.Length - 1;

                highlight.transform.position = slots[slotIndex].transform.position;
                highlight.SetActive(true);

                cooldown = (int)(100000 * Time.deltaTime);
            }

            // Hide the hotbar after the allotted time.
            if (cooldown > 0) {
                cooldown--;
            } else if (slotGrid.activeSelf || highlight.activeSelf) {
                slotGrid.SetActive(false);
                highlight.SetActive(false);
            }
        }
	}
}
