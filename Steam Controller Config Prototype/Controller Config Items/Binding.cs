using System.Text;
using VDF_Parser;

namespace Controller_Config_Items
{
    class Binding : ControllerComponentObject
    {

        public Binding()
        {
        }
        public override string ExportToVDF()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("\"bindings\"");
            sb.AppendLine("{");
            foreach(KeyValue kv in base.keyValues)
            {
                sb.AppendLine("\t\"" + kv.GetKey() + "\"\t\"" + kv.GetValue() + "\"");
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
