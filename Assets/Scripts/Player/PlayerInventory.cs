using Minecraft.ItemsData;
using System.Collections.Generic;
using UnityEngine;
namespace Minecraft {
	public class PlayerInventory : AbstractMenu {
		#region Singleton
		public static PlayerInventory Instance {
			get;
			private set;
		}

		private void Awake() {
			if (Instance != null) {
				Debug.LogWarning("More than one instance of PlayerInventory found!");
				return;
			}

			Instance = this;
		}

		#endregion Singleton:

		[SerializeField] private ArmorItem[] equipment;
		[SerializeField] private int backpackSpace = 0;

		public List<Item> backpackItems = new List<Item>();

		// Tries to add an item, fails if there's not enough space
		// returns whether operation was successful
		public bool Add(Item item) {
			if (backpackItems.Count < backpackSpace) {
				backpackItems.Add(item);
				return true;
			} else {
				Debug.Log("Not enough space.");
				return false;
			}
		}

		public void Remove(Item item) {
			backpackItems.Remove(item);
		}

		// <returns>Item to put in cursor after operation</returns>
		public Item Equip(ArmorItem item, ArmorSlot slot) {
			if (item.ArmorSlot == slot) {
				Item cursor = equipment[(int)slot - 1];
				equipment[(int) slot - 1] = item;
				return cursor;
			} else {
				return item;
			}
		}
	}
}
