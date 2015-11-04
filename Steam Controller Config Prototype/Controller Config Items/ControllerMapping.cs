using System.Collections.Generic;
using System.Text;
using VDF_Parser;

namespace Controller_Config_Items
{
    class ControllerMapping : ControllerComponentObject
    {
        public List<Group> groups { get; set; }
        public List<Preset> presets { get; set; }

        public ControllerMapping()
        {
            groups = new List<Group>();
            presets = new List<Preset>();
        }

        public override void ParseParentKey(ParentKey parent)
        {
            base.keyValues = parent.GetKeyValues();
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
            foreach(KeyValue keyValue in base.keyValues)
            {
                sb.AppendLine("\t\"" + keyValue.GetKey() + "\"\t\"" + keyValue.GetValue() + "\"");
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
