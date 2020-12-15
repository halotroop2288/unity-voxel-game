using UnityEngine;

#pragma warning disable CS0649

namespace Minecraft.ItemsData {
	[CreateAssetMenu(menuName = "Minecraft/Generic Item")]
	public class Item : ScriptableObject {
		[SerializeField] private ItemType m_Type;

		[SerializeField] private string m_ItemName;
		[SerializeField] private Sprite m_Icon;
		
		private event ItemEventAction OnItemUse;

		public virtual void Use(PlayerEntity player) {
			// Override me!
			Debug.Log("Used item: " + this.m_ItemName);
			OnItemUse.Invoke(player, this);
		}

		public ItemType Type => m_Type;
		public string ItemName => m_ItemName;
		public Sprite Icon => m_Icon;
	}

	[CreateAssetMenu(menuName = "Minecraft/Stackable Item")]
	public class StackableItem : Item {
		[SerializeField] [Range(1, 999)] private int m_MaxCount;
		public int MaxCount => m_MaxCount;
	}

	[CreateAssetMenu(menuName = "Minecraft/Breakable Item")]
	public class BreakableItem : Item {
		[SerializeField] [Range(1, 9999)] private int m_MaxDamage;
		public int MaxDamage => m_MaxDamage;
	}

	public enum TrinketSlot : byte {
		Mask, Cape, Belt, Aglet,
		Charm, Necklace, Gloves, Ring
	}

	public enum ArmorSlot : byte {

		// aka Head, Shoulders, Knees, and Toes!
		Head, Chest, Legs, Feet
	}
}