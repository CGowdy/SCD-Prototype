using Controller_Config_Items;
using VDF_Parser;

namespace Controller_Input_Items
{
    abstract class Button
    {
        public string action { get; set; }
        public string groupID { get; set; }
        public Group group { get; set; }
        public KeyValue keyValue { get; set; }

        public void ChangeAction(string newAction) {
            keyValue.SetValue(newAction);
        }
    }
}
