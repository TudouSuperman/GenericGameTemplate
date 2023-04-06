using Bright.Serialization;

{{
    name = x.name
    namespace = x.namespace
    tables = x.tables
}}
namespace {{namespace}}
{
   
public partial class {{name}}
{

    private System.Collections.Generic.Dictionary<string, object> tables;

    {{~for table in tables ~}}
{{~if table.comment != '' ~}}
    /// <summary>
    /// {{table.escape_comment}}
    /// </summary>
{{~end~}}
    public {{table.full_name}} {{table.name}} {get; private set; }
    {{~end~}}

    

    public void LoadAllDataTable(GenericGameTemplate.TableComponent component,ref System.Collections.Generic.List<string> allLoadingTables){
        tables = new System.Collections.Generic.Dictionary<string, object>();

        {{~for table in tables ~}}
        {{table.name}} = component.CreateDataTable<{{table.full_name}}>();
        var {{table.output_data_file}}_readPath = AssetUtility.GetDataTableLuBanAsset("{{table.output_data_file}}");
        {{table.name}}.ReadData({{table.output_data_file}}_readPath);
        allLoadingTables.Add({{table.output_data_file}}_readPath);
        tables.Add("{{table.full_name}}", {{table.name}});
        {{~end~}}

    }

    public void ResolveAll(){

        {{~for table in tables ~}}
        {{table.name}}.Resolve(tables); 
        {{~end~}}

        tables.Clear();
    }



    public void TranslateText(System.Func<string, string, string> translator)
    {
        {{~for table in tables ~}}
        {{table.name}}.TranslateText(translator); 
        {{~end~}}
    }
}

}