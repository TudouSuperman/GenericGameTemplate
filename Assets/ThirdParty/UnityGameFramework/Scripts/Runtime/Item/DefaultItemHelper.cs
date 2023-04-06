using GameFramework.Item;
using UnityEngine;
namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 默认物品组辅助器。
    /// </summary>
    public class DefaultItemHelper : ItemHelperBase
    {
        private ResourceComponent m_ResourceComponent = null;

        /// <summary>
        /// 实例化物品。
        /// </summary>
        /// <param name="itemAsset">要实例化的物品资源。</param>
        /// <returns>实例化后的物品。</returns>
        public override object InstantiateItem(object itemAsset)
        {
            return Instantiate((Object)itemAsset);
        }

        /// <summary>
        /// 创建物品。
        /// </summary>
        /// <param name="itemInstance">物品实例。</param>
        /// <param name="itemGroup">物品所属的物品组。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>物品。</returns>
        public override IItem CreateItem(object itemInstance, IItemGroup itemGroup, object userData)
        {
            GameObject gameObject = itemInstance as GameObject;
            if (gameObject == null)
            {
                Log.Error("Item instance is invalid.");
                return null;
            }

            Transform transform = gameObject.transform;
            transform.SetParent(((MonoBehaviour)itemGroup.Helper).transform);
            return gameObject.GetOrAddComponent<Item>();
        }

        /// <summary>
        /// 释放物品。
        /// </summary>
        /// <param name="itemAsset">要释放的物品资源。</param>
        /// <param name="itemInstance">要释放的物品实例。</param>
        public override void ReleaseItem(object itemAsset, object itemInstance)
        {
            m_ResourceComponent.UnloadAsset(itemAsset);
            Destroy((Object)itemInstance);
        }

        private void Start()
        {
            m_ResourceComponent = GameEntry.GetComponent<ResourceComponent>();
            if (m_ResourceComponent == null)
            {
                Log.Fatal("Resource component is invalid.");
                return;
            }
        }
    }
}
