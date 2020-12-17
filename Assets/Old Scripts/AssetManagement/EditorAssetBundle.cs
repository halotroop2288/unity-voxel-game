﻿#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using Object = UnityEngine.Object;

namespace Minecraft.AssetManagement
{
    internal sealed class EditorAssetBundle : IAssetBundle
    {
        private readonly string m_Name;
        private readonly bool m_IsStreamedSceneAssetBundle;
        private readonly HashSet<string> m_AssetPaths;
        private readonly AsyncHandler m_AsyncHandler;

        string IAssetBundle.Name => m_Name;

        bool IAssetBundle.IsStreamedSceneAssetBundle => m_IsStreamedSceneAssetBundle;

        AsyncHandler IAssetBundle.AsyncHandler => m_AsyncHandler;

        public EditorAssetBundle(string name)
        {
            m_Name = name;

            string[] paths = AssetDatabase.GetAssetPathsFromAssetBundle(m_Name);

            m_IsStreamedSceneAssetBundle = (paths != null && paths.Length > 0) ?
                AssetDatabase.GetMainAssetTypeAtPath(paths[0]) == typeof(SceneAsset) : false;

            IEqualityComparer<string> comparer = StringComparer.OrdinalIgnoreCase;

            m_AssetPaths = paths == null
                ? new HashSet<string>(comparer)
                : new HashSet<string>(paths, comparer);

            m_AsyncHandler = AsyncHandler.CreateCompleted();
        }

        private AsyncAssets LoadAllAssets(Type type)
        {
            if (type == null)
            {
                type = typeof(Object);
            }

            List<Object> objs = new List<Object>(m_AssetPaths.Count);

            foreach (string path in m_AssetPaths)
            {
                Type objType = AssetDatabase.GetMainAssetTypeAtPath(path);

                if (objType != null && type.IsAssignableFrom(objType))
                {
                    objs.Add(AssetDatabase.LoadAssetAtPath(path, type));
                }
            }

            return new AsyncAssets(objs.ToArray());
        }

        AsyncAssets IAssetBundle.LoadAllAssets<T>()
        {
            return LoadAllAssets(typeof(T));
        }

        AsyncAssets IAssetBundle.LoadAllAssets(Type type)
        {
            return LoadAllAssets(type);
        }

        AsyncAsset<T> IAssetBundle.LoadAsset<T>(string path)
        {
            if (m_AssetPaths.Contains(path))
            {
                return new AsyncAsset<T>(AssetDatabase.LoadAssetAtPath<T>(path));
            }

            foreach (string p in m_AssetPaths)
            {
                if (p.EndsWith(path, StringComparison.OrdinalIgnoreCase))
                {
                    return new AsyncAsset<T>(AssetDatabase.LoadAssetAtPath<T>(p));
                }
            }

            return new AsyncAsset<T>(obj: null);
        }

        AsyncAsset<Object> IAssetBundle.LoadAsset(string path, Type type)
        {
            if (m_AssetPaths.Contains(path))
            {
                return new AsyncAsset<Object>(AssetDatabase.LoadAssetAtPath(path, type));
            }

            foreach (string p in m_AssetPaths)
            {
                if (p.EndsWith(path, StringComparison.OrdinalIgnoreCase))
                {
                    return new AsyncAsset<Object>(AssetDatabase.LoadAssetAtPath(p, type));
                }
            }

            return new AsyncAsset<Object>(obj: null);
        }

        void IAssetBundle.Unload(bool unloadAllLoadedObjects) { }
    }
}
#endif