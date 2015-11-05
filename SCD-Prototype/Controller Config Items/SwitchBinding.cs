using System.Collections.Generic;
using System.Text;
using VDF_Parser;

namespace Controller_Config_Items
{
    class SwitchBinding : ControllerComponentObject
    {
        public List<Binding> bindings { get; set; }

        public SwitchBinding()
        {
            bindings = new List<Binding>();
        }
        public override string ExportToVDF()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\"switch_bindings\"");
            sb.AppendLine("{");
            foreach(Binding binding in bindings)
            {
                sb.AppendLine("\t" + binding.ExportToVDF());
            }
            sb.AppendLine("}");
            return sb.ToString();
        }

        public override void ParseParentKey(ParentKey parent)
        {
            foreach(ParentKey child in parent.GetChildren())
            {
                Binding b = new Binding();
                b.ParseParentKey(child);
                bindings.Add(b);
            }
        }
    }
}