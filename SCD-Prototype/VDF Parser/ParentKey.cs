using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VDF_Parser
{
    class ParentKey : IKey
    {
        private List<KeyValue> keyValues;
        private List<ParentKey> childrenKeys;
        private string key;

        public ParentKey()
        {
            keyValues = new List<KeyValue>();
            childrenKeys = new List<ParentKey>();
            key = "";
        }

        public ParentKey(string key)
        {
            keyValues = new List<KeyValue>();
            childrenKeys = new List<ParentKey>();
            this.key = key;
        }

        public void AddKeyValue(KeyValue keyValue)
        {
            keyValues.Add(keyValue);
        }

        public void AddParentKey(ParentKey childKey)
        {
            childrenKeys.Add(childKey);
        }
        public string GetKey()
        {
            return key;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[key: " + key + ";");
            if(keyValues.Count != 0)
            {
                foreach(KeyValue kv in keyValues)
                {
                    sb.Append(kv.ToString() + ";");
                }
            }
            if(childrenKeys.Count() != 0)
            {
                foreach(ParentKey child in childrenKeys)
                {
                    sb.Append(child.ToString() + ";");
                }
            }
            sb.Append("]");
            return sb.ToString();
        }

        internal List<ParentKey> GetChildren()
        {
            return childrenKeys;
        }

        internal List<KeyValue> GetKeyValues()
        {
            return keyValues;
        }
    }
}
