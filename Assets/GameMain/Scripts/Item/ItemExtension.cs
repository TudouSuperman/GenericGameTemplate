using System;
using GameFramework.DataTable;
using UnityGameFramework.Runtime;

namespace GenericGameTemplate
{
    public static class ItemExtension
    {
        private static int s_SerialId = 0;
        
        public static void ShowItem(this ItemComponent itemComponent, int serialId, ItemId enumItem, object userData = null)
        {
            itemComponent.ShowItem(serialId, enumItem, null, userData);
        }

        public static void ShowItem<T>(this ItemComponent itemComponent, int serialId, ItemId enumItem, object userData = null)
        {
            itemComponent.ShowItem(serialId, enumItem, typeof(T), userData);
        }

        public static void ShowItem(this ItemComponent itemComponent, int serialId, ItemId enumItem, Type logicType, object userData = null)
        {
            itemComponent.ShowItem(serialId, (int)enumItem, logicType, userData);
        }

        public static void ShowItem(this ItemComponent itemComponent, int serialId, int itemId, object userData = null)
        {
            itemComponent.ShowItem(serialId, itemId, null, userData);
        }

        public static void ShowItem<T>(this ItemComponent itemComponent, int serialId, int itemId, object userData = null)
        {
            itemComponent.ShowItem(serialId, itemId, typeof(T), userData);
        }

        public static void ShowItem(this ItemComponent itemComponent, int serialId, int itemId, Type logicType, object userData = null)
        {
            IDataTable<DRItem> dtItem = GameEntry.DataTable.GetDataTable<DRItem>();
            DRItem drItem = dtItem.GetDataRow(itemId);

            if (drItem == null)
            {
                Log.Warning("Can not load item id '{0}' from data table.", drItem.Id.ToString());
                return;
            }

            itemComponent.ShowItem(serialId, logicType, drItem.AssetName, drItem.ItemGroupName, Constant.AssetPriority.ItemAsset, userData);
        }
        
        public static int GenerateSerialId(this ItemComponent itemComponent)
        {
            return ++s_SerialId;
        }
    }
}