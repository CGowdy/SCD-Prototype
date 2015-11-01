namespace VDF_Parser
{
    class KeyValue : IKey
    {
        private string key;
        private string value;

        public KeyValue()
        {
            key = "";
            value = "";
        }

        public KeyValue(string key, string value)
        {
            this.key = key;
            this.value = value;
        }
        public string GetKey()
        {
            return key;
        }

        public string GetValue()
        {
            return value;
        }

        public override string ToString()
        {
            return string.Format("[key: {0}; value: {1}]", key, value);
        }
    }
}
