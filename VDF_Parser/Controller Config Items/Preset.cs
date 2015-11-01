using System.Collections.Generic;
using System.Text;
using VDF_Parser;

namespace Controller_Config_Items
{
    class Preset : ControllerComponentObject
    {
        public List<KeyValue> keyValues { get; set; }
        public List<GroupSourceBinding> groupSourceBindings { get; set; }
        public List<SwitchBinding> switchBindings { get; set; }
        public List<Setting> settings { get; set; }

        public Preset()
        {
            keyValues = new List<KeyValue>();
            groupSourceBindings = new List<GroupSourceBinding>();
            switchBindings = new List<SwitchBinding>();
            settings = new List<Setting>();
        }

        public override void ParseParentKey(ParentKey parent)
        {
            keyValues = parent.GetKeyValues();
            foreach(ParentKey child in parent.GetChildren())
            {
                switch (child.GetKey())
                {
                    case "group_source_bindings":
                        GroupSourceBinding g = new GroupSourceBinding();
                        g.ParseParentKey(child);
                        groupSourceBindings.Add(g);
                        break;
                    case "switch_bindings":
                        SwitchBinding sb = new SwitchBinding();
                        sb.ParseParentKey(child);
                        switchBindings.Add(sb);
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
            sb.AppendLine("\"preset\"");
            sb.AppendLine("{");
            foreach(KeyValue keyvalue in keyValues)
            {
                sb.AppendLine("\t\"" + keyvalue.GetKey() + "\"\t\"" + keyvalue.GetValue() + "\"");
            }
            foreach(GroupSourceBinding groupSourceBinding in groupSourceBindings)
            {
                sb.AppendLine("\t" + groupSourceBinding.ExportToVDF());
            }
            foreach(SwitchBinding switchBinding in switchBindings)
            {
                sb.AppendLine("\t" + switchBinding.ExportToVDF());
            }
            foreach(Setting setting in settings)
            {
                sb.AppendLine("\t" + setting.ExportToVDF());
            }
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
