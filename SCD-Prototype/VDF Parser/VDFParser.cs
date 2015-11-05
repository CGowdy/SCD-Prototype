using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VDF_Parser
{
    class VDFParser
    {
        private bool stringOverride = false;
        private bool keyStarted = false;
        private bool valueStarted = false;
        private bool previousKey = false;
        private string key = "";
        private string value = "";
        private ParentKey currentParent;
        private Stack<ParentKey> parentKeys;
        private Stack<ParentKey> completedParents;

        public VDFParser()
        {
            parentKeys = new Stack<ParentKey>();
            completedParents = new Stack<ParentKey>();

        }
        public VDFParser(string filepath)
        {
            parentKeys = new Stack<ParentKey>();
            completedParents = new Stack<ParentKey>();
            if (File.Exists(filepath))
                Parse(new StreamReader(File.Open(filepath, FileMode.Open)));
        }
        public ParentKey Parse(StreamReader reader)
        {
            try
            {
                using (StreamReader sr = reader)
                {
                    StringBuilder sb = new StringBuilder();
                    char ch;
                    while (!sr.EndOfStream)
                    {
                        ch = (char)sr.Read();

                        switch (ch)
                        {
                            case '"':
                                if (keyStarted || valueStarted)
                                {

                                    if (valueStarted)
                                    {
                                        value = sb.ToString();
                                        sb.Clear();
                                        KeyValue kv = new KeyValue(key, value);
                                        currentParent.AddKeyValue(kv);
                                        keyStarted = false;
                                        valueStarted = false;
                                        previousKey = false;
                                        stringOverride = false;
                                    }
                                    else
                                    {
                                        key = sb.ToString();
                                        sb.Clear();
                                        keyStarted = false;
                                        previousKey = true;
                                        stringOverride = false;
                                    }
                                }
                                else
                                {
                                    if (previousKey)
                                    {
                                        valueStarted = true;
                                        stringOverride = true;
                                    }
                                    else
                                    {
                                        keyStarted = true;
                                        stringOverride = true;
                                    }
                                }
                                break;
                            case '{':
                                if (!previousKey)
                                    throw new FormatException("Cannot start ParentKey without Key, '{' without Key");
                                if (currentParent != null)
                                    parentKeys.Push(currentParent); //Push currentParent to close/append later
                                currentParent = new ParentKey(key);
                                previousKey = false;
                                sb.Clear();
                                break;
                            case '}':
                                completedParents.Push(currentParent);

                                //If there are no more on the stack we are on outer most parent
                                if (parentKeys.Count != 0)
                                {   
                                    ParentKey child = currentParent;
                                    currentParent = parentKeys.Pop();   //"Done" with current parent; get previous one to continue appending keys
                                    currentParent.AddParentKey(child);
                                }
                                break;
                            default:
                                if (!stringOverride && (ch == ' ' || ch == '\r' || ch == '\n' || ch == '\t'))
                                {
                                    if (keyStarted) {
                                        key = sb.ToString();
                                        sb.Clear();
                                        keyStarted = false;
                                    }
                                    if (valueStarted)
                                    {
                                        value = sb.ToString();
                                        sb.Clear();
                                        KeyValue kv = new KeyValue(key, value);
                                        currentParent.AddKeyValue(kv);
                                        valueStarted = false;
                                        previousKey = false;
                                    }
                                        
                                    continue;
                                }
                                if (!keyStarted && !previousKey)
                                    keyStarted = true;
                                if (!keyStarted && previousKey && !valueStarted)
                                    valueStarted = true;
                                sb.Append(ch);
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Throw it up for now
                throw ex;
            }

            return currentParent;
        }
    }
}
