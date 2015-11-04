using System.Collections.Generic;
using VDF_Parser;

namespace Controller_Config_Items
{
    abstract class ControllerComponentObject
    {
        public List<KeyValue> keyValues { get; set; }
        public abstract void ParseParentKey(ParentKey parent);

        public abstract string ExportToVDF();

        public KeyValue GetKeyValue(string key)
        {
            foreach(KeyValue keyValue in keyValues)
            {
                if (keyValue.GetKey().Equals(key))
                    return keyValue;
            }

            return null;    //TODO:May want to return something better
        }
    }
}
