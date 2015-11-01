namespace Controller_Input_Items
{
    abstract class Button
    {
        public string action { get; set; }
        public string groupID { get; set; }

        public void ChangeAction(string newAction) {
            action = newAction;
        }
    }
}
