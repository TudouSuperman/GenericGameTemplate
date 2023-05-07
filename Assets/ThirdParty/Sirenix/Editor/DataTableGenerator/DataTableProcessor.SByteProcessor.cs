using System.IO;

namespace GenericGameTemplate.Editor.DataTableTools
{
    public sealed partial class DataTableProcessor
    {
        private sealed class SByteProcessor : GenericDataProcessor<sbyte>
        {
            public override bool IsSystem
            {
                get
                {
                    return true;
                }
            }

            public override string LanguageKeyword
            {
                get
                {
                    return "sbyte";
                }
            }

            public override string[] GetTypeStrings()
            {
                return new string[]
                {
                    "sbyte",
                    "system.sbyte"
                };
            }

            public override sbyte Parse(string value)
            {
                return sbyte.Parse(value);
            }

            public override void WriteToStream(DataTableProcessor dataTableProcessor, BinaryWriter binaryWriter, string value)
            {
                binaryWriter.Write(Parse(value));
            }
        }
    }
}
