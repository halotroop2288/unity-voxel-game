using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minecraft {
	public interface Inventory : Clearable {
		int Size();

		bool IsEmpty();

		ItemStack getStack(int slot);

		ItemStack removeStack(int slot, int amount);

		void setStack(int slot, ItemStack stack);
	}

	public interface Clearable {
		void Clear();
	}
}
