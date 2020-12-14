using UnityEngine;

namespace Minecraft.ItemsData {

	// ArmorItems can be equipped to the player
	[CreateAssetMenu(menuName = "Minecraft/Item/Armor Item")]
	public class ArmorItem : Item {
		[SerializeField] [Tooltip("Slot item can be equipped to")] public ArmorSlot m_ArmorSlot;
		[SerializeField] [Tooltip("Governs damage reduction")] [Range(0, 255)] public int m_ArmourRating;

		public ArmorSlot ArmorSlot => m_ArmorSlot;

		public int ArmourRating => m_ArmourRating;
	}
}