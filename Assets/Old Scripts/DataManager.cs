using Minecraft.AssetManagement;
using Minecraft.BlocksData;
using Minecraft.DebugUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Object = UnityEngine.Object;
using UnifiedFactorization;

namespace Minecraft {
	public sealed class DataManager : IDisposable, IDebugMessageSender {
		private sealed class BlockTypeComparer : IEqualityComparer<BlockType> {
			public bool Equals(BlockType x, BlockType y) {
				return x == y;
			}

			public int GetHashCode(BlockType obj) {
				return (int) obj;
			}
		}

		private readonly Dictionary<BlockType, Block> m_BlocksMap;
		private readonly Dictionary<string, Item> m_ItemsMap;
		private readonly Dictionary<string, byte[]> m_LuaMap;
		private readonly AssetBundleLoader m_Loader;

		public Material ChunkMaterial {
			get; private set;
		}

		public Material LiquidMaterial {
			get; private set;
		}

		public Material BlockEntityMaterial {
			get; private set;
		}

		public int BlockCount => m_BlocksMap.Count;

		public int ItemCount => m_ItemsMap.Count;

		string IDebugMessageSender.DisplayName => "DataManager";

		public bool DisableLog {
			get; set;
		}

		public DataManager(string resourcePackName) {
			m_BlocksMap = new Dictionary<BlockType, Block>(new BlockTypeComparer());
			m_ItemsMap = new Dictionary<string, Item>();
			m_LuaMap = new Dictionary<string, byte[]>(StringComparer.Ordinal);
			m_Loader = new AssetBundleLoader(Path.Combine(Application.streamingAssetsPath, WorldConsts.ResourcePackagesFolderName, resourcePackName));
		}

		public IEnumerator InitBlocks() {
			IAssetBundle ab = m_Loader.LoadAssetBundle("minecraft/blocks");
			yield return ab.AsyncHandler;

			AsyncAssets assets = ab.LoadAllAssets<Block>();

			while (!assets.IsDone) {
				yield return null;
			}

			Object[] blocks = assets.Assets;

			for (int i = 0; i < blocks.Length; i++) {
				Block block = blocks[i] as Block;
				m_BlocksMap.Add(block.Type, block);
			}
		}

		public IEnumerator InitItems() {
			IAssetBundle ab = m_Loader.LoadAssetBundle("minecraft/items");
			yield return ab.AsyncHandler;

			AsyncAssets assets = ab.LoadAllAssets<Item>();

			while (!assets.IsDone) {
				yield return null;
			}

			Object[] items = assets.Assets;

			for (int i = 0; i < items.Length; i++) {
				Item item = items[i] as Item;
				m_ItemsMap.Add(item.ItemName, item);
			}
		}

		public IEnumerator InitMaterials() {
			IAssetBundle ab = m_Loader.LoadAssetBundle("minecraft/materials");
			yield return ab.AsyncHandler;

			AsyncAsset<Material> asset;

			asset = ab.LoadAsset<Material>("chunkmaterial.mat");

			while (!asset.IsDone) {
				yield return null;
			}

			ChunkMaterial = asset.Asset;

			asset = ab.LoadAsset<Material>("liquidmaterial.mat");

			while (!asset.IsDone) {
				yield return null;
			}

			LiquidMaterial = asset.Asset;

			asset = ab.LoadAsset<Material>("blockentitymaterial.mat");

			while (!asset.IsDone) {
				yield return null;
			}

			BlockEntityMaterial = asset.Asset;
		}

		public void Dispose() {
			DisposeBlockEvents();

			m_BlocksMap.Clear();
			m_ItemsMap.Clear();
			m_LuaMap.Clear();

			m_Loader.UnloadAllAssetBundles(true);
		}

		private void DisposeBlockEvents() {
			foreach (Block block in m_BlocksMap.Values) {
				block.ClearEvents();
			}
		}

		public void ForeachAllBlocks(Action<Block> callback) {
			if (callback == null)
				return;

			foreach (Block block in m_BlocksMap.Values) {
				callback(block);
			}
		}

		public Block GetBlockByType(BlockType type) {
			return m_BlocksMap.TryGetValue(type, out Block block) ? block : null;
		}

		public Item GetItemByName(String name) {
			return m_ItemsMap.TryGetValue(name, out Item item) ? item : null;
		}
	}
}