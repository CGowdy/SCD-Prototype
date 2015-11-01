using System.Collections.Generic;
using System.Text;
using VDF_Parser;

namespace Controller_Config_Items
{
    class Binding : ControllerComponentObject
    {
        public List<KeyValue> keyValues { get; set; }

        public Binding()
        {
            keyValues = new List<KeyValue>();
        }
        public override string ExportToVDF()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("\"bindings\"");
            sb.AppendLine("{");
            foreach(KeyValue kv in keyValues)
            {
                sb.AppendLine("\t\"" + kv.GetKey() + "\"\t\"" + kv.GetValue() + "\"");
            }
            sb.AppendLine("}");
            return sb.ToString();
        }

        public override void ParseParentKey(ParentKey parent)
        {
            keyValues = parent.GetKeyValues();
        }
    }
}
