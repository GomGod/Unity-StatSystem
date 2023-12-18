﻿using System;
using System.Collections.Generic;
using GeneralGameDevKit.Utils;
using UnityEngine;

namespace GeneralGameDevKit.ValueTableSystem.Internal
{
    /// <summary>
    /// Manager class that manage KeyTableAssets.<br/>
    /// This class is designed for attributes, so don't use this class in your code 
    /// </summary>
    public class KeyTableAssetManager : NonMonoSingleton<KeyTableAssetManager>
    {
        private readonly Dictionary<string, KeyTableAsset> _loadedTableAssets = new();
        
        public KeyTableAssetManager()
        {
            var loadedTableAssets = Resources.LoadAll<KeyTableAsset>("ValueTableAssets");
            if (loadedTableAssets.Length <= 0) return;
            
            _loadedTableAssets.Clear();
            foreach (var tableAsset in loadedTableAssets)
            {
                _loadedTableAssets.Add(tableAsset.name, tableAsset);
            }
        }

        public IEnumerable<string> GetAllParameters(string containerName)
        {
            return _loadedTableAssets.TryGetValue(containerName, out var asset) ? asset.GetAllKeys() : null;
        }
    }
}

namespace GeneralGameDevKit.ValueTableSystem
{
    /// <summary>
    /// Displays the values in the table with the same name.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class KeyTableAttribute : PropertyAttribute
    {
        public readonly string AssetName;

        public KeyTableAttribute(string assetName)
        {
            AssetName = assetName;
        }
    }
}