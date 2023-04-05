using GameFramework.Item;
using UnityEngine;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 物品辅助器基类。
    /// </summary>
    public abstract class ItemHelperBase : MonoBehaviour, IItemHelper
    {
        /// <summary>
        /// 实例化物品。
        /// </summary>
        /// <param name="itemAsset">要实例化的物品资源。</param>
        /// <returns>实例化后的物品。</returns>
        public abstract object InstantiateItem(object itemAsset);

        /// <summary>
        /// 创建物品。
        /// </summary>
        /// <param name="itemInstance">物品实例。</param>
        /// <param name="itemGroup">物品所属的物品组。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>物品。</returns>
        public abstract IItem CreateItem(object itemInstance, IItemGroup itemGroup, object userData);

        /// <summary>
        /// 释放物品。
        /// </summary>
        /// <param name="itemAsset">要释放的物品资源。</param>
        /// <param name="itemInstance">要释放的物品实例。</param>
        public abstract void ReleaseItem(object itemAsset, object itemInstance);
    }
}
