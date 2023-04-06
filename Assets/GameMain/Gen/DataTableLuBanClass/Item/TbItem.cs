//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;
using GenericGameTemplate;

namespace GenericGameTemplate.DataTables.Item
{
   
public partial class TbItem : TableBase
{
    private readonly Dictionary<int, Item.TbItemInfo> _dataMap;
    private readonly List<Item.TbItemInfo> _dataList;
    
    public TbItem(): base("TbItem")
    {
       _dataMap = new Dictionary<int, Item.TbItemInfo>();
       _dataList = new List<Item.TbItemInfo>();

    }

    public override void LoadByteBuf(byte[] bytes){
  
        var _buf = new ByteBuf(bytes);
        for(int n = _buf.ReadSize() ; n > 0 ; --n)
        {
            Item.TbItemInfo _v;
            _v = Item.TbItemInfo.DeserializeTbItemInfo(_buf);
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
        PostInit();
    }

    public Dictionary<int, Item.TbItemInfo> DataMap => _dataMap;
    public List<Item.TbItemInfo> DataList => _dataList;

    public Item.TbItemInfo GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public Item.TbItemInfo Get(int key) => _dataMap[key];
    public Item.TbItemInfo this[int key] => _dataMap[key];

    public void Resolve(Dictionary<string, object> _tables)
    {
        foreach(var v in _dataList)
        {
            v.Resolve(_tables);
        }
        PostResolve();
    }

    public void TranslateText(System.Func<string, string, string> translator)
    {
        foreach(var v in _dataList)
        {
            v.TranslateText(translator);
        }
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}