using VDF_Parser;

namespace Controller_Config_Items
{
    abstract class ControllerComponentObject
    {
        public abstract void ParseParentKey(ParentKey parent);

        public abstract string ExportToVDF();
    }
}
