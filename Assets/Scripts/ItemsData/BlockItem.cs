using Minecraft.BlocksData;
using UnityEngine;

namespace Minecraft.ItemsData {
	[CreateAssetMenu(menuName = "Minecraft/Item/Block Item")]
	public class BlockItem : Item {
		[SerializeField] private BlockType m_MappedBlockType;

		public BlockType MappedBlockType => m_MappedBlockType;

		public override void Use(PlayerEntity player) {
			base.Use(player);

			// Try to place block
			if (this.MappedBlockType != BlockType.Air) {
				if (player.RaycastBlock(false, out Vector3Int hit, out Vector3Int normal, b => b.HasAnyFlag(BlockFlags.IgnorePlaceBlockRaycast))) {
					Vector3Int pos = hit + normal;

					Vector3 min = new Vector3(pos.x + 0.01f, pos.y + 0.01f, pos.z + 0.01f);
					Vector3 max = new Vector3(pos.x - 0.01f + 1, pos.y - 0.01f + 1, pos.z - 0.01f + 1);
					AABB blockAABB = new AABB(min, max);

					if (!player.BoundingBox.Intersects(blockAABB)) {
						WorldManager.Active.SetBlockType(pos.x, pos.y, pos.z, this.MappedBlockType);

						Block block = WorldManager.Active.DataManager.GetBlockByType(this.MappedBlockType);
						block.OnBlockPlace(pos.x, pos.y, pos.z);
						block.PlayPlaceAudio(player.AudioSource);
					}
				}
			}
		}
	}
}