using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Resource;
using UnityEngine;

namespace GenericGameTemplate
{
    /// <summary>
    /// All table base
    /// </summary>
    public abstract class TableBase : IDataProvider<TableBase>
    {
        public event EventHandler<ReadDataSuccessEventArgs> ReadDataSuccess
        {
            add => _dataProvider.ReadDataSuccess += value;
            remove => _dataProvider.ReadDataSuccess -= value;
        }

        public event EventHandler<ReadDataFailureEventArgs> ReadDataFailure
        {
            add => _dataProvider.ReadDataFailure += value;
            remove => _dataProvider.ReadDataFailure -= value;
        }

        public event EventHandler<ReadDataUpdateEventArgs> ReadDataUpdate
        {
            add => _dataProvider.ReadDataUpdate += value;
            remove => _dataProvider.ReadDataUpdate -= value;
        }

        public event EventHandler<ReadDataDependencyAssetEventArgs> ReadDataDependencyAsset
        {
            add => _dataProvider.ReadDataDependencyAsset += value;
            remove => _dataProvider.ReadDataDependencyAsset -= value;
        }

        private readonly DataProvider<TableBase> _dataProvider;
        private readonly string _name;

        internal string Name => string.IsNullOrEmpty(_name) ? Type.FullName : _name;

        internal Type Type => GetType();

        protected TableBase(string name)
        {
            _dataProvider = new DataProvider<TableBase>(this);
            this._name = name;
        }

        public void ReadData(string dataAssetName)
        {
            _dataProvider.ReadData(dataAssetName);
        }

        public void ReadData(string dataAssetName, int priority)
        {
            _dataProvider.ReadData(dataAssetName, priority);
        }

        public void ReadData(string dataAssetName, object userData)
        {
            _dataProvider.ReadData(dataAssetName, userData);
        }

        public void ReadData(string dataAssetName, int priority, object userData)
        {
            _dataProvider.ReadData(dataAssetName, priority, userData);
        }

        public bool ParseData(string dataString)
        {
            return _dataProvider.ParseData(dataString);
        }

        public bool ParseData(string dataString, object userData)
        {
            return _dataProvider.ParseData(dataString, userData);
        }

        public bool ParseData(byte[] dataBytes)
        {
            return _dataProvider.ParseData(dataBytes);
        }

        public bool ParseData(byte[] dataBytes, object userData)
        {
            return _dataProvider.ParseData(dataBytes, userData);
        }

        public bool ParseData(byte[] dataBytes, int startIndex, int length)
        {
            return _dataProvider.ParseData(dataBytes, startIndex, length);
        }

        public bool ParseData(byte[] dataBytes, int startIndex, int length, object userData)
        {
            return _dataProvider.ParseData(dataBytes, startIndex, length, userData);
        }

        internal void SetResourceManager(IResourceManager resourceManager)
        {
            _dataProvider.SetResourceManager(resourceManager);
        }

        internal void SetDataProviderHelper(IDataProviderHelper<TableBase> dataProviderHelper)
        {
            _dataProvider.SetDataProviderHelper(dataProviderHelper);
        }

        public virtual void LoadByteBuf(byte[] bytes)
        {
        }
    }
}