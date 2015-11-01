using System.Collections.Generic;
using System.Text;
using VDF_Parser;

namespace Controller_Config_Items
{
    class GroupSourceBinding : ControllerComponentObject
    {
        public List<KeyValue> keyValues { get; set; }
        
        public GroupSourceBinding()
        {
            keyValues = new List<KeyValue>();
        }
        public override string ExportToVDF()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\"group_source_bindings\"");
            sb.AppendLine("{");
            foreach(KeyValue keyvalue in keyValues)
            {
                sb.AppendLine("\t\"" + keyvalue.GetKey() + "\"\t\"" + keyvalue.GetValue() + "\"");
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
