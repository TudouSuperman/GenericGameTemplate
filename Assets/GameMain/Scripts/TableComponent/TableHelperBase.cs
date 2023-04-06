using System.Collections;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;

namespace GenericGameTemplate
{
    public abstract class TableHelperBase : UnityEngine.MonoBehaviour, IDataProviderHelper<TableBase>
    {
        public abstract bool ReadData(TableBase dataProviderOwner, string dataAssetName, object dataAsset,
            object userData);


        public abstract bool ReadData(TableBase dataProviderOwner, string dataAssetName, byte[] dataBytes,
            int startIndex, int length,
            object userData);

        public abstract bool ParseData(TableBase dataProviderOwner, string dataString, object userData);

        public abstract bool ParseData(TableBase dataProviderOwner, byte[] dataBytes, int startIndex, int length,
            object userData);

        public abstract void ReleaseDataAsset(TableBase dataProviderOwner, object dataAsset);
    }
}