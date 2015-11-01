using VDF_Parser;
using System.Collections.Generic;
using System.Text;

namespace Controller_Config_Items
{
    class ControllerMapping : ControllerComponentObject
    {
        public List<Group> groups { get; set; }
        public List<Preset> presets { get; set; }
        public List<KeyValue> keyValues { get; set; }

        public ControllerMapping()
        {
            groups = new List<Group>();
            presets = new List<Preset>();
            keyValues = new List<KeyValue>();
        }

        public override void ParseParentKey(ParentKey parent)
        {
            keyValues = parent.GetKeyValues();
            foreach (ParentKey child in parent.GetChildren())
            {
                switch (child.GetKey())
                {
                    case "group":
                        Group group = new Group();
                        group.ParseParentKey(child);
                        groups.Add(group);
                        break;
                    case "preset":
                        Preset preset = new Preset();
                        preset.ParseParentKey(child);
                        presets.Add(preset);
                        break;
                }
            }
        }

        public override string ExportToVDF()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("\"controller_mappings\"");
            sb.AppendLine("{");
            foreach(KeyValue keyValue in keyValues)
            {
                sb.AppendLine("\t\"" + keyValue.GetKey() + "\"\t\"" + keyValue.GetKey() + "\"");
            }
            foreach (Group group in groups)
            {
                sb.AppendLine("\t" + group.ExportToVDF());
            }

            foreach (Preset preset in presets)
            {
                sb.AppendLine("\t" + preset.ExportToVDF());
            }
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
