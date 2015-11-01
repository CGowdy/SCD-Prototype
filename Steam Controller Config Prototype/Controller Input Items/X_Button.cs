using System.Linq;

namespace Controller_Input_Items
{
    class X_Button : Button
    {
        private const string STEAM_X_BUTTON_TEXT = "button_X";
        private const string STEAM_DIAMOND_BUTTON_TEXT = "button_diamond";

        public X_Button(ControllerMapping controllerMapping)
        {
            //TODO: Will want to allow multiple presets at some point
            Preset preset = controllerMapping.presets.First<Preset>();

            foreach (KeyValue keyvalue in preset.groupSourceBindings.First<GroupSourceBinding>().keyValues)
            {
                if (keyvalue.GetValue().Equals(STEAM_DIAMOND_BUTTON_TEXT))
                {
                    groupID = keyvalue.GetKey();
                    break;
                }
            }

            foreach (Group group in controllerMapping.groups)
            {
                KeyValue kv;
                if ((kv = group.FindBinding(STEAM_X_BUTTON_TEXT)) != null)
                    action = kv.GetValue();
            }

        }
    }
}
