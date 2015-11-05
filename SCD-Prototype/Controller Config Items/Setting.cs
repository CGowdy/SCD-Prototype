using System.Collections.Generic;
using System.Text;
using VDF_Parser;

namespace Controller_Config_Items
{
    class Setting : ControllerComponentObject
    {
        public List<KeyValue> setting { get; set; }

        public Setting()
        {
            setting = new List<KeyValue>();
        }
        public override string ExportToVDF()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\"settings\"");
            sb.AppendLine("{");
            foreach(KeyValue keyvalue in setting)
            {
                sb.AppendLine("\t\"" + keyvalue.GetKey() + "\"\t\"" + keyvalue.GetValue() + "\"");
            }
            sb.AppendLine("}");
            return sb.ToString();
        }

        public override void ParseParentKey(ParentKey parent)
        {
            setting = parent.GetKeyValues();
        }
        
    }
}
