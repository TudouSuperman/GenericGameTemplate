using System;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Resource;

namespace GenericGameTemplate
{
    internal sealed class TableManager 
    {
        private readonly Dictionary<TypeNamePair, TableBase> _allTables;
        private IDataProviderHelper<TableBase> _dataProviderHelper;
        private IResourceManager _resourceManager;


        public TableManager()
        {
            _allTables = new Dictionary<TypeNamePair, TableBase>();
            _dataProviderHelper = null;
            _resourceManager = null;
        }

        public void SetResourceManager(IResourceManager resourceManager)
        {
            _resourceManager = resourceManager ?? throw new GameFrameworkException("Resource manager is invalid.");
        }

        /// <summary>
        /// 设置数据表数据提供者辅助器。
        /// </summary>
        /// <param name="dataProviderHelper">数据表数据提供者辅助器。</param>
        public void SetDataProviderHelper(IDataProviderHelper<TableBase> dataProviderHelper)
        {
            _dataProviderHelper =
                dataProviderHelper ?? throw new GameFrameworkException("Data provider helper is invalid.");
        }


        /// <summary>
        /// 确保二进制流缓存分配足够大小的内存并缓存。
        /// </summary>
        /// <param name="ensureSize">要确保二进制流缓存分配内存的大小。</param>
        public void EnsureCachedBytesSize(int ensureSize)
        {
            DataProvider<TableBase>.EnsureCachedBytesSize(ensureSize);
        }

        /// <summary>
        /// 释放缓存的二进制流。
        /// </summary>
        public void FreeCachedBytes()
        {
            DataProvider<TableBase>.FreeCachedBytes();
        }


        /// <summary>
        /// 是否存在数据表。
        /// </summary>
        /// <typeparam name="T">数据表行的类型。</typeparam>
        /// <returns>是否存在数据表。</returns>
        public bool HasDataTable<T>() where T : TableBase
        {
            return InternalHasDataTable(new TypeNamePair(typeof(T)));
        }

        /// <summary>
        /// 是否存在数据表。
        /// </summary>
        /// <param name="dataRowType">数据表行的类型。</param>
        /// <returns>是否存在数据表。</returns>
        public bool HasDataTable(Type dataRowType)
        {
            if (dataRowType == null)
            {
                throw new GameFrameworkException("Data row type is invalid.");
            }

            if (!typeof(TableBase).IsAssignableFrom(dataRowType))
            {
                throw new GameFrameworkException(Utility.Text.Format("Data row type '{0}' is invalid.",
                    dataRowType.FullName));
            }

            return InternalHasDataTable(new TypeNamePair(dataRowType));
        }

        /// <summary>
        /// 是否存在数据表。
        /// </summary>
        /// <typeparam name="T">数据表行的类型。</typeparam>
        /// <param name="name">数据表名称。</param>
        /// <returns>是否存在数据表。</returns>
        public bool HasDataTable<T>(string name) where T : TableBase
        {
            return InternalHasDataTable(new TypeNamePair(typeof(T), name));
        }

        /// <summary>
        /// 是否存在数据表。
        /// </summary>
        /// <param name="dataRowType">数据表行的类型。</param>
        /// <param name="name">数据表名称。</param>
        /// <returns>是否存在数据表。</returns>
        public bool HasDataTable(Type dataRowType, string name)
        {
            if (dataRowType == null)
            {
                throw new GameFrameworkException("Data row type is invalid.");
            }

            if (!typeof(TableBase).IsAssignableFrom(dataRowType))
            {
                throw new GameFrameworkException(Utility.Text.Format("Data row type '{0}' is invalid.",
                    dataRowType.FullName));
            }

            return InternalHasDataTable(new TypeNamePair(dataRowType, name));
        }


        /// <summary>
        /// 获取数据表。
        /// </summary>
        /// <typeparam name="T">数据表行的类型。</typeparam>
        /// <returns>要获取的数据表。</returns>
        public T GetDataTable<T>() where T : TableBase
        {
            return (T)InternalGetDataTable(new TypeNamePair(typeof(T)));
        }

        /// <summary>
        /// 获取数据表。
        /// </summary>
        /// <param name="dataRowType">数据表行的类型。</param>
        /// <returns>要获取的数据表。</returns>
        public TableBase GetDataTable(Type dataRowType)
        {
            if (dataRowType == null)
            {
                throw new GameFrameworkException("Data row type is invalid.");
            }

            if (!typeof(TableBase).IsAssignableFrom(dataRowType))
            {
                throw new GameFrameworkException(Utility.Text.Format("Data row type '{0}' is invalid.",
                    dataRowType.FullName));
            }

            return InternalGetDataTable(new TypeNamePair(dataRowType));
        }

        /// <summary>
        /// 获取数据表。
        /// </summary>
        /// <typeparam name="T">数据表行的类型。</typeparam>
        /// <param name="name">数据表名称。</param>
        /// <returns>要获取的数据表。</returns>
        public T GetDataTable<T>(string name) where T : TableBase
        {
            return (T)InternalGetDataTable(new TypeNamePair(typeof(T), name));
        }

        /// <summary>
        /// 获取数据表。
        /// </summary>
        /// <param name="dataRowType">数据表行的类型。</param>
        /// <param name="name">数据表名称。</param>
        /// <returns>要获取的数据表。</returns>
        public TableBase GetDataTable(Type dataRowType, string name)
        {
            if (dataRowType == null)
            {
                throw new GameFrameworkException("Data row type is invalid.");
            }

            if (!typeof(TableBase).IsAssignableFrom(dataRowType))
            {
                throw new GameFrameworkException(Utility.Text.Format("Data row type '{0}' is invalid.",
                    dataRowType.FullName));
            }

            return InternalGetDataTable(new TypeNamePair(dataRowType, name));
        }


        /// <summary>
        /// 获取所有数据表。
        /// </summary>
        /// <returns>所有数据表。</returns>
        public TableBase[] GetAllDataTables()
        {
            int index = 0;
            TableBase[] results = new TableBase[_allTables.Count];
            foreach (KeyValuePair<TypeNamePair, TableBase> dataTable in _allTables)
            {
                results[index++] = dataTable.Value;
            }

            return results;
        }

        /// <summary>
        /// 获取所有数据表。
        /// </summary>
        /// <param name="results">所有数据表。</param>
        public void GetAllDataTables(List<TableBase> results)
        {
            if (results == null)
            {
                throw new GameFrameworkException("Results is invalid.");
            }

            results.Clear();
            foreach (KeyValuePair<TypeNamePair, TableBase> dataTable in _allTables)
            {
                results.Add(dataTable.Value);
            }
        }

        /// <summary>
        /// 创建数据表。
        /// </summary>
        /// <typeparam name="T">数据表行的类型。</typeparam>
        /// <returns>要创建的数据表。</returns>
        public T CreateDataTable<T>() where T : TableBase, new()
        {
            return CreateDataTable<T>(string.Empty);
        }

        /// <summary>
        /// 创建数据表。
        /// </summary>
        /// <param name="dataRowType">数据表行的类型。</param>
        /// <returns>要创建的数据表。</returns>
        public TableBase CreateDataTable(Type dataRowType)
        {
            return CreateDataTable(dataRowType, string.Empty);
        }


        /// <summary>
        /// 创建数据表。
        /// </summary>
        /// <typeparam name="T">数据表行的类型。</typeparam>
        /// <param name="name">数据表名称。</param>
        /// <returns>要创建的数据表。</returns>
        public T CreateDataTable<T>(string name) where T : TableBase, new()
        {
            if (_resourceManager == null)
            {
                throw new GameFrameworkException("You must set resource manager first.");
            }

            if (_dataProviderHelper == null)
            {
                throw new GameFrameworkException("You must set data provider helper first.");
            }

            TypeNamePair typeNamePair = new TypeNamePair(typeof(T), name);
            if (HasDataTable<T>(name))
            {
                throw new GameFrameworkException(Utility.Text.Format("Already exist data table '{0}'.", typeNamePair));
            }

            var dataTable = new T();
            dataTable.SetResourceManager(_resourceManager);
            dataTable.SetDataProviderHelper(_dataProviderHelper);
            _allTables.Add(typeNamePair, dataTable);
            return dataTable;
        }

        public TableBase CreateDataTable(Type tableType, string name)
        {
            if (_resourceManager == null)
            {
                throw new GameFrameworkException("You must set resource manager first.");
            }

            if (_dataProviderHelper == null)
            {
                throw new GameFrameworkException("You must set data provider helper first.");
            }

            if (tableType == null)
            {
                throw new GameFrameworkException("Data row type is invalid.");
            }

            if (!typeof(TableBase).IsAssignableFrom(tableType))
            {
                throw new GameFrameworkException(Utility.Text.Format("Data row type '{0}' is invalid.",
                    tableType.FullName));
            }

            var typeNamePair = new TypeNamePair(tableType, name);
            if (HasDataTable(tableType, name))
            {
                throw new GameFrameworkException(Utility.Text.Format("Already exist data table '{0}'.", typeNamePair));
            }

            TableBase dataTable = (TableBase)Activator.CreateInstance(tableType, name);
            dataTable.SetResourceManager(GameFrameworkEntry.GetModule<IResourceManager>());
            dataTable.SetDataProviderHelper(_dataProviderHelper);
            _allTables.Add(typeNamePair, dataTable);
            return dataTable;
        }


        /// <summary>
        /// 销毁数据表。
        /// </summary>
        /// <typeparam name="T">数据表行的类型。</typeparam>
        public bool DestroyDataTable<T>() where T : TableBase
        {
            return InternalDestroyDataTable(new TypeNamePair(typeof(T)));
        }

        /// <summary>
        /// 销毁数据表。
        /// </summary>
        /// <param name="dataRowType">数据表行的类型。</param>
        /// <returns>是否销毁数据表成功。</returns>
        public bool DestroyDataTable(Type dataRowType)
        {
            if (dataRowType == null)
            {
                throw new GameFrameworkException("Data row type is invalid.");
            }

            if (!typeof(TableBase).IsAssignableFrom(dataRowType))
            {
                throw new GameFrameworkException(Utility.Text.Format("Data row type '{0}' is invalid.",
                    dataRowType.FullName));
            }

            return InternalDestroyDataTable(new TypeNamePair(dataRowType));
        }

        /// <summary>
        /// 销毁数据表。
        /// </summary>
        /// <typeparam name="T">数据表行的类型。</typeparam>
        /// <param name="name">数据表名称。</param>
        public bool DestroyDataTable<T>(string name) where T : TableBase
        {
            return InternalDestroyDataTable(new TypeNamePair(typeof(T), name));
        }

        /// <summary>
        /// 销毁数据表。
        /// </summary>
        /// <param name="dataRowType">数据表行的类型。</param>
        /// <param name="name">数据表名称。</param>
        /// <returns>是否销毁数据表成功。</returns>
        public bool DestroyDataTable(Type dataRowType, string name)
        {
            if (dataRowType == null)
            {
                throw new GameFrameworkException("Data row type is invalid.");
            }

            if (!typeof(TableBase).IsAssignableFrom(dataRowType))
            {
                throw new GameFrameworkException(Utility.Text.Format("Data row type '{0}' is invalid.",
                    dataRowType.FullName));
            }

            return InternalDestroyDataTable(new TypeNamePair(dataRowType, name));
        }

        /// <summary>
        /// 销毁数据表。
        /// </summary>
        /// <typeparam name="T">数据表行的类型。</typeparam>
        /// <param name="dataTable">要销毁的数据表。</param>
        /// <returns>是否销毁数据表成功。</returns>
        public bool DestroyDataTable<T>(T dataTable) where T : TableBase
        {
            if (dataTable == null)
            {
                throw new GameFrameworkException("Data table is invalid.");
            }

            return InternalDestroyDataTable(new TypeNamePair(typeof(T), dataTable.Name));
        }

        /// <summary>
        /// 销毁数据表。
        /// </summary>
        /// <param name="dataTable">要销毁的数据表。</param>
        /// <returns>是否销毁数据表成功。</returns>
        public bool DestroyDataTable(TableBase dataTable)
        {
            if (dataTable == null)
            {
                throw new GameFrameworkException("Data table is invalid.");
            }

            return InternalDestroyDataTable(new TypeNamePair(dataTable.Type, dataTable.Name));
        }


        private bool InternalHasDataTable(TypeNamePair typeNamePair)
        {
            return _allTables.ContainsKey(typeNamePair);
        }

        private TableBase InternalGetDataTable(TypeNamePair typeNamePair)
        {
            return _allTables.TryGetValue(typeNamePair, out var dataTable) ? dataTable : null;
        }

        private bool InternalDestroyDataTable(TypeNamePair typeNamePair)
        {
            if (_allTables.TryGetValue(typeNamePair, out var dataTable))
            {
                return _allTables.Remove(typeNamePair);
            }

            return false;
        }
        
    }
}