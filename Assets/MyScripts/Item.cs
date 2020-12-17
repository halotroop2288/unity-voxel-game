using UnityEngine;
using Minecraft;

#pragma warning disable CS0649

namespace UnifiedFactorization {
	[CreateAssetMenu(menuName = "Minecraft/Item/Unstackable Item")]
	public class Item : ScriptableObject {
		[SerializeField] private string m_ItemName;
		[SerializeField] private Sprite m_Icon;
		
		private event ItemEventAction OnItemUse;

		public virtual void Use(PlayerEntity player) {
			// Override me!
			Debug.Log("Used item: " + this.m_ItemName);
			OnItemUse.Invoke(player, this);
		}
		public string ItemName => m_ItemName;
		public Sprite Icon => m_Icon;
	}

	[CreateAssetMenu(menuName = "Minecraft/Item/Stackable Item")]
	public class StackableItem : Item {
		[SerializeField] [Range(1, 999)] private int m_MaxCount;
		public int MaxCount => m_MaxCount;
	}

	[CreateAssetMenu(menuName = "Minecraft/Item/Breakable Item")]
	public class BreakableItem : Item {
		[SerializeField] [Range(1, 9999)] private int m_MaxDamage;
		public int MaxDamage => m_MaxDamage;
	}

	public class EquipmentItem : BreakableItem {
		private EquipmentSlot m_Slot;
		private int m_ArmorRating;
		public EquipmentSlot Slot => m_Slot;
		public int ArmorRating => m_ArmorRating;
	}

	public enum EquipmentSlot {
		Head, Chest, Legs, Feet, // aka Head, Shoulders, Knees, and Toes!
		Mask, Cape, Belt, Aglet,
		Charm, Necklace, Gloves, Ring
	}

	public enum TrinketSlot : byte {
	}

	public enum ArmorSlot : byte {
	}
}