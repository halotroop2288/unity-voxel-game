using UnityEngine;
using XLua;

#pragma warning disable CS0649

namespace Minecraft.ItemsData {

	[LuaCallCSharp]
	[CreateAssetMenu(menuName = "Minecraft/Item/Generic")]
	public class Item : ScriptableObject {
		[SerializeField] private string m_ItemName;
		[SerializeField] private ItemType m_Type;
		[SerializeField] [Tooltip("Amount allowed in a slot")] [Range(1, 99)] private int m_MaxStackSize = 99;
		[SerializeField] [Tooltip("Times/sec blocks will recieve damage using this")] [Range(0, 60)] private float m_DigSpeed = 3;
		[SerializeField] private Sprite m_Icon;

		private event ItemEventAction OnItemUse;

		public virtual void Use(PlayerEntity player) {
			// Override me!
			Debug.Log("Used item: " + this.m_ItemName);
			OnItemUse.Invoke(player, this);
		}

		public string ItemName => m_ItemName;

		public ItemType Type => m_Type;

		public int MaxStackSize => m_MaxStackSize;

		public float DiggingSpeed => m_DigSpeed;

		public Sprite Icon => m_Icon;
	}

	public enum ArmorSlot : byte {

		// aka Head, Shoulders, Knees, and Toes!
		Head, Chest, Legs, Feet
	}
}