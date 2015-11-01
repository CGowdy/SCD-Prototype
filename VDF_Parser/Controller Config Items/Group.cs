using System.Collections.Generic;
using System.Text;
using VDF_Parser;

namespace Controller_Config_Items
{
    class Group : ControllerComponentObject
    {
        public List<KeyValue> keyValues { get; set; }
        public List<Binding> bindings { get; set; }
        public List<Setting> settings { get; set; }
        public Group()
        {
            keyValues = new List<KeyValue>();
            bindings = new List<Binding>();
            settings = new List<Setting>();
        }

        public KeyValue FindBinding(string key)
        {
            foreach (Binding binding in bindings)
            {
                if (binding.keyValues.Find(x => x.GetKey().Equals(key)) != null)
                    return binding.keyValues.Find(x => x.GetKey().Equals(key));
            }
            return null;
        }

        public override void ParseParentKey(ParentKey parent)
        {
            keyValues = parent.GetKeyValues();
            foreach (ParentKey child in parent.GetChildren())
            {
                switch (child.GetKey())
                {
                    case "bindings":
                        Binding b = new Binding();
                        b.ParseParentKey(child);
                        bindings.Add(b);
                        break;
                    case "settings":
                        Setting s = new Setting();
                        s.ParseParentKey(child);
                        settings.Add(s);
                        break;
                }
            }
        }

        public override string ExportToVDF()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("\"group\"");
            sb.AppendLine("}");
            foreach (KeyValue keyvalue in keyValues)
            {
                sb.AppendLine("\t\"" + keyvalue.GetKey() + "\"\t\"" + keyvalue.GetValue() + "\"");
            }
            foreach (Binding binding in bindings)
            {
                sb.AppendLine("\t" + binding.ExportToVDF());
            }
            foreach (Setting setting in settings)
            {
                sb.AppendLine("\t" + setting.ExportToVDF());
            }
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
