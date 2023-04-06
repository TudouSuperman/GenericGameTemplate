using System;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Resource;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace GenericGameTemplate
{
    public sealed class TableComponent : GameFrameworkComponent
    {
        private TableManager _tableManager = null;


        [SerializeField] private bool m_EnableLoadDataTableUpdateEvent = false;

        [SerializeField] private bool m_EnableLoadDataTableDependencyAssetEvent = false;

        [SerializeField] private string m_DataTableHelperTypeName = "GenericGameTemplate.Runtime.DefaultTableHelper";

        [SerializeField] private TableHelperBase m_CustomDataTableHelper = null;

        [SerializeField] private int m_CachedBytesSize = 0;


        protected override void Awake()
        {
            base.Awake();

            _tableManager = new TableManager();
            if (_tableManager == null)
            {
                Log.Fatal("Data table manager is invalid.");
                return;
            }
        }

        private void Start()
        {
            if (GameEntry.Base.EditorResourceMode)
            {
                _tableManager.SetResourceManager(GameEntry.Base.EditorResourceHelper);
            }
            else
            {
                _tableManager.SetResourceManager(GameFrameworkEntry.GetModule<IResourceManager>());
            }

            TableHelperBase dataTableHelper = Helper.CreateHelper(m_DataTableHelperTypeName, m_CustomDataTableHelper);
            if (dataTableHelper == null)
            {
                Log.Error("Can not create data table helper.");
                return;
            }

            dataTableHelper.name = "Data Table Helper";
            Transform transform = dataTableHelper.transform;
            transform.SetParent(transform);
            transform.localScale = Vector3.one;

            _tableManager.SetDataProviderHelper(dataTableHelper);
            if (m_CachedBytesSize > 0)
            {
                EnsureCachedBytesSize(m_CachedBytesSize);
            }
        }

        /// <summary>
        /// 确保二进制流缓存分配足够大小的内存并缓存。
        /// </summary>
        /// <param name="ensureSize">要确保二进制流缓存分配内存的大小。</param>
        public void EnsureCachedBytesSize(int ensureSize)
        {
            _tableManager.EnsureCachedBytesSize(ensureSize);
        }

        /// <summary>
        /// 释放缓存的二进制流。
        /// </summary>
        public void FreeCachedBytes()
        {
            _tableManager.FreeCachedBytes();
        }

        /// <summary>
        /// 是否存在数据表。
        /// </summary>
        /// <typeparam name="T">数据表行的类型。</typeparam>
        /// <returns>是否存在数据表。</returns>
        public bool HasDataTable<T>() where T : TableBase
        {
            return _tableManager.HasDataTable<T>();
        }

        /// <summary>
        /// 是否存在数据表。
        /// </summary>
        /// <param name="dataRowType">数据表行的类型。</param>
        /// <returns>是否存在数据表。</returns>
        public bool HasDataTable(Type dataRowType)
        {
            return _tableManager.HasDataTable(dataRowType);
        }

        /// <summary>
        /// 是否存在数据表。
        /// </summary>
        /// <typeparam name="T">数据表行的类型。</typeparam>
        /// <param name="name">数据表名称。</param>
        /// <returns>是否存在数据表。</returns>
        public bool HasDataTable<T>(string name) where T : TableBase
        {
            return _tableManager.HasDataTable<T>(name);
        }

        /// <summary>
        /// 是否存在数据表。
        /// </summary>
        /// <param name="dataRowType">数据表行的类型。</param>
        /// <param name="name">数据表名称。</param>
        /// <returns>是否存在数据表。</returns>
        public bool HasDataTable(Type dataRowType, string name)
        {
            return _tableManager.HasDataTable(dataRowType, name);
        }

        /// <summary>
        /// 获取数据表。
        /// </summary>
        /// <typeparam name="T">数据表行的类型。</typeparam>
        /// <returns>要获取的数据表。</returns>
        public T GetDataTable<T>() where T : TableBase
        {
            return _tableManager.GetDataTable<T>();
        }

        /// <summary>
        /// 获取数据表。
        /// </summary>
        /// <param name="dataRowType">数据表行的类型。</param>
        /// <returns>要获取的数据表。</returns>
        public TableBase GetDataTable(Type dataRowType)
        {
            return _tableManager.GetDataTable(dataRowType);
        }

        /// <summary>
        /// 获取数据表。
        /// </summary>
        /// <typeparam name="T">数据表行的类型。</typeparam>
        /// <param name="name">数据表名称。</param>
        /// <returns>要获取的数据表。</returns>
        public T GetDataTable<T>(string name) where T : TableBase
        {
            return _tableManager.GetDataTable<T>(name);
        }

        /// <summary>
        /// 获取数据表。
        /// </summary>
        /// <param name="dataRowType">数据表行的类型。</param>
        /// <param name="name">数据表名称。</param>
        /// <returns>要获取的数据表。</returns>
        public TableBase GetDataTable(Type dataRowType, string name)
        {
            return _tableManager.GetDataTable(dataRowType, name);
        }

        /// <summary>
        /// 获取所有数据表。
        /// </summary>
        public TableBase[] GetAllDataTables()
        {
            return _tableManager.GetAllDataTables();
        }

        /// <summary>
        /// 获取所有数据表。
        /// </summary>
        /// <param name="results">所有数据表。</param>
        public void GetAllDataTables(List<TableBase> results)
        {
            _tableManager.GetAllDataTables(results);
        }

        /// <summary>
        /// 创建数据表。
        /// </summary>
        /// <typeparam name="T">数据表行的类型。</typeparam>
        /// <returns>要创建的数据表。</returns>
        public T CreateDataTable<T>() where T : TableBase, new()
        {
            return CreateDataTable<T>(null);
        }

        /// <summary>
        /// 创建数据表。
        /// </summary>
        /// <param name="dataRowType">数据表行的类型。</param>
        /// <returns>要创建的数据表。</returns>
        public TableBase CreateDataTable(Type dataRowType)
        {
            return CreateDataTable(dataRowType, null);
        }

        /// <summary>
        /// 创建数据表。
        /// </summary>
        /// <typeparam name="T">数据表行的类型。</typeparam>
        /// <param name="name">数据表名称。</param>
        /// <returns>要创建的数据表。</returns>
        public T CreateDataTable<T>(string name) where T : TableBase, new()
        {
            T dataTable = _tableManager.CreateDataTable<T>(name);
            TableBase dataTableBase = (TableBase)dataTable;
            dataTableBase.ReadDataSuccess += OnReadDataSuccess;
            dataTableBase.ReadDataFailure += OnReadDataFailure;

            if (m_EnableLoadDataTableUpdateEvent)
            {
                dataTableBase.ReadDataUpdate += OnReadDataUpdate;
            }

            if (m_EnableLoadDataTableDependencyAssetEvent)
            {
                dataTableBase.ReadDataDependencyAsset += OnReadDataDependencyAsset;
            }

            return dataTable;
        }

        /// <summary>
        /// 创建数据表。
        /// </summary>
        /// <param name="dataRowType">数据表行的类型。</param>
        /// <param name="name">数据表名称。</param>
        /// <returns>要创建的数据表。</returns>
        public TableBase CreateDataTable(Type dataRowType, string name)
        {
            TableBase dataTable = _tableManager.CreateDataTable(dataRowType, name);
            dataTable.ReadDataSuccess += OnReadDataSuccess;
            dataTable.ReadDataFailure += OnReadDataFailure;

            if (m_EnableLoadDataTableUpdateEvent)
            {
                dataTable.ReadDataUpdate += OnReadDataUpdate;
            }

            if (m_EnableLoadDataTableDependencyAssetEvent)
            {
                dataTable.ReadDataDependencyAsset += OnReadDataDependencyAsset;
            }

            return dataTable;
        }

        /// <summary>
        /// 销毁数据表。
        /// </summary>
        /// <typeparam name="T">数据表行的类型。</typeparam>
        /// <returns>是否销毁数据表成功。</returns>
        public bool DestroyDataTable<T>() where T : TableBase, new()
        {
            return _tableManager.DestroyDataTable<T>();
        }

        /// <summary>
        /// 销毁数据表。
        /// </summary>
        /// <param name="dataRowType">数据表行的类型。</param>
        /// <returns>是否销毁数据表成功。</returns>
        public bool DestroyDataTable(Type dataRowType)
        {
            return _tableManager.DestroyDataTable(dataRowType);
        }

        /// <summary>
        /// 销毁数据表。
        /// </summary>
        /// <typeparam name="T">数据表行的类型。</typeparam>
        /// <param name="name">数据表名称。</param>
        /// <returns>是否销毁数据表成功。</returns>
        public bool DestroyDataTable<T>(string name) where T : TableBase
        {
            return _tableManager.DestroyDataTable<T>(name);
        }

        /// <summary>
        /// 销毁数据表。
        /// </summary>
        /// <param name="dataRowType">数据表行的类型。</param>
        /// <param name="name">数据表名称。</param>
        /// <returns>是否销毁数据表成功。</returns>
        public bool DestroyDataTable(Type dataRowType, string name)
        {
            return _tableManager.DestroyDataTable(dataRowType, name);
        }

        /// <summary>
        /// 销毁数据表。
        /// </summary>
        /// <typeparam name="T">数据表行的类型。</typeparam>
        /// <param name="dataTable">要销毁的数据表。</param>
        /// <returns>是否销毁数据表成功。</returns>
        public bool DestroyDataTable<T>(T dataTable) where T : TableBase
        {
            return _tableManager.DestroyDataTable(dataTable);
        }

        /// <summary>
        /// 销毁数据表。
        /// </summary>
        /// <param name="dataTable">要销毁的数据表。</param>
        /// <returns>是否销毁数据表成功。</returns>
        public bool DestroyDataTable(TableBase dataTable)
        {
            return _tableManager.DestroyDataTable(dataTable);
        }

        private void OnReadDataSuccess(object sender, ReadDataSuccessEventArgs e)
        {
            GameEntry.Event.Fire(this, LoadDataTableSuccessEventArgs.Create(e));
        }

        private void OnReadDataFailure(object sender, ReadDataFailureEventArgs e)
        {
            Log.Warning("Load data table failure, asset name '{0}', error message '{1}'.", e.DataAssetName,
                e.ErrorMessage);
            GameEntry.Event.Fire(this, LoadDataTableFailureEventArgs.Create(e));
        }

        private void OnReadDataUpdate(object sender, ReadDataUpdateEventArgs e)
        {
            GameEntry.Event.Fire(this, LoadDataTableUpdateEventArgs.Create(e));
        }

        private void OnReadDataDependencyAsset(object sender, ReadDataDependencyAssetEventArgs e)
        {
            GameEntry.Event.Fire(this, LoadDataTableDependencyAssetEventArgs.Create(e));
        }
    }
}