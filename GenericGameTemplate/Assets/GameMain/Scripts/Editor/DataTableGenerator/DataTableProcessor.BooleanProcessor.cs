﻿using System.IO;

namespace GenericGameTemplate.Editor.DataTableTools
{
    public sealed partial class DataTableProcessor
    {
        private sealed class BooleanProcessor : GenericDataProcessor<bool>
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
                    return "bool";
                }
            }

            public override string[] GetTypeStrings()
            {
                return new string[]
                {
                    "bool",
                    "boolean",
                    "system.boolean"
                };
            }

            public override bool Parse(string value)
            {
                return bool.Parse(value);
            }

            public override void WriteToStream(DataTableProcessor dataTableProcessor, BinaryWriter binaryWriter, string value)
            {
                binaryWriter.Write(Parse(value));
            }
        }
    }
}
