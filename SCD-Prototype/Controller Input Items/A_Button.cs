using Controller_Config_Items;
using System.Linq;

namespace Controller_Input_Items
{
    class A_Button : Button
    {
        private const string STEAM_A_BUTTON_TEXT = "button_A";
        private const string STEAM_DIAMOND_BUTTON_TEXT = "button_diamond active";

        public A_Button(ControllerMapping controllerMapping) : base()
        {
            //TODO: Will want to allow multiple presets at some point
            Preset preset = controllerMapping.presets.First<Preset>();

            base.groupID = preset.groupSourceBindings.FirstOrDefault().GetKeyValueByValue(STEAM_DIAMOND_BUTTON_TEXT).GetKey();

            base.group = controllerMapping.groups.Where(x => x.GetKeyValueByKey("id").GetValue().Equals(groupID)).FirstOrDefault();

            base.keyValue = base.group.bindings.Where(x => x.GetKeyValueByKey(STEAM_A_BUTTON_TEXT) != null).FirstOrDefault().GetKeyValueByKey(STEAM_A_BUTTON_TEXT);

            base.action = base.keyValue.GetValue();
            
        }
    }
}
