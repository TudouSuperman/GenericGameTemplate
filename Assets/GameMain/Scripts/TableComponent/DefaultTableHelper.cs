using UnityEngine;
using UnityGameFramework.Runtime;

namespace GenericGameTemplate
{
    public class DefaultTableHelper : TableHelperBase
    {
        public override bool ReadData(TableBase dataProviderOwner, string dataAssetName, object dataAsset,
            object userData)
        {
            TextAsset textAsset = dataAsset as TextAsset;
            if (textAsset == null)
            {
                Log.Warning("Data table asset '{0}' is invalid.", dataAssetName);
                return false;
            }

            return dataProviderOwner.ParseData(textAsset.bytes, userData);
        }

        public override bool ReadData(TableBase dataProviderOwner, string dataAssetName, byte[] dataBytes,
            int startIndex, int length,
            object userData)
        {
            return dataProviderOwner.ParseData(dataBytes, startIndex, length, userData);
        }

        public override bool ParseData(TableBase dataProviderOwner, string dataString, object userData)
        {
            return false;
        }

        public override bool ParseData(TableBase dataProviderOwner, byte[] dataBytes, int startIndex, int length,
            object userData)
        {
            dataProviderOwner.LoadByteBuf(dataBytes);
            return true;
        }

        public override void ReleaseDataAsset(TableBase dataProviderOwner, object dataAsset)
        {
            GameEntry.Resource.UnloadAsset(dataAsset);
        }
    }
}