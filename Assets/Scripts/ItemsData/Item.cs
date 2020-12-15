using UnityEngine;

#pragma warning disable CS0649

namespace Minecraft.ItemsData {
	[CreateAssetMenu(menuName = "Minecraft/Item/Unstackable Item")]
	public class Item : ScriptableObject, ItemConvertible {
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

		public Item AsItem() {
			return this;
		}
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

	public interface ItemConvertible {
		Item AsItem();
	}

	public enum TrinketSlot : byte {
	}

	public enum ArmorSlot : byte {
	}
}