using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minecraft {
    public class Hotbar : MonoBehaviour {
        [SerializeField] public GameObject row1;
        [SerializeField] public UIItemSlot[] slots;
        [SerializeField] public GameObject highlight;
        [SerializeField] public int slotIndex = 0;

        [SerializeField] private int cooldown = 0;

		private void Update() {
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if (scroll != 0) {
                row1.SetActive(true);
                if (scroll > 0)
                    slotIndex--;
                else
                    slotIndex++;

                if (slotIndex > slots.Length - 1)
                    slotIndex = 0;
                if (slotIndex < 0)
                    slotIndex = slots.Length - 1;

                highlight.transform.position = slots[slotIndex].slotIcon.transform.position;
                highlight.SetActive(true);

                cooldown = (int)(100000 * Time.deltaTime);
            }

            // Hide the hotbar after the allotted time.
            if (cooldown > 0) {
                cooldown--;
            } else if (row1.activeSelf || highlight.activeSelf) {
                row1.SetActive(false);
                highlight.SetActive(false);
            }
        }
	}
}
