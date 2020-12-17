using System.Diagnostics;
using UnityEditor;
using UnityEngine;

namespace Minecraft
{
    public sealed class OpenSaveFolder
    {
        [ContextMenu("Minecraft-Unity/Open Save Folder")]
        public static void OpenWorldSavingFolder()
        {
            Process.Start(Application.persistentDataPath);
        }
    }
}