using System.Text;
using VDF_Parser;

namespace Controller_Config_Items
{
    class GroupSourceBinding : ControllerComponentObject
    {
        
        public GroupSourceBinding()
        {
        }
        public override string ExportToVDF()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\"group_source_bindings\"");
            sb.AppendLine("{");
            foreach(KeyValue keyvalue in base.keyValues)
            {
                sb.AppendLine("\t\"" + keyvalue.GetKey() + "\"\t\"" + keyvalue.GetValue() + "\"");
            }
            sb.AppendLine("}");
            return sb.ToString();
        }

        public override void ParseParentKey(ParentKey parent)
        {
            base.keyValues = parent.GetKeyValues();
        }
    }
}
