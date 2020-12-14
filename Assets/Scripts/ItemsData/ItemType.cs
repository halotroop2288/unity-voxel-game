using XLua;

namespace Minecraft.ItemsData {
    [LuaCallCSharp]
    public enum ItemType {
        None, // First
        Cobblestone, Mossy_Cobblestone,
        Bricks,
        Dirt,
        Log,
        Planks,
        Slab,
        Leaves,
        Sapling,
        Bookshelf,
        Rose, Dandelion,
        Brown_Mushroom, Red_Mushroom,
        Sand, Gravel,
        Sponge,
        Glass,
        Dynamite,
        Obsidian,
        Bedrock // Last
    }
}