using System.Collections.Generic;

using FirstVM.ValueTypes;
namespace FirstVM
{
    static class VariablesTable
    {
        private static Dictionary<string, IValue> Variables = new Dictionary<string, IValue>();
        public static void SetValue(string name, IValue value)
        {
            Variables[name] = value;
        }
        public static IValue GetValue(string name)
        {
            return Variables[name];
        }
    }
    static class Contexts
    {
        private static Stack<Dictionary<string, IValue>> _stackValues = new Stack<Dictionary<string, IValue>>();

        //  { [Context] }
        //  while() { [Context] }
        //  if () { [Context] }
        //
        //  Contexts = [3]
        //  [Context#0]
        //  {
        //      [Context#1]
        //      a = 0
        //
        //      if (true)
        //      {
        //          [Context#2]
        //          a = 1 [1][0].Set
        //          b = 0 [2][1].Set
        //      }
        //      //a=1
        //      //b - not found
        //  }

        public static void PushNewContext()
        {
            PushContext(new Dictionary<string, IValue>());
        }
        public static void PushContext(Dictionary<string, IValue> context)
        {
            _stackValues.Push(context);
        }
        public static void PopContext()
        {
            _stackValues.Pop();
        }

        public static IValue GetValue(string s)
        {
            foreach (Dictionary<string, IValue> context in _stackValues)
                if (context.ContainsKey(s)) return context[s];
            throw Error("Variable - " + s + ", not found");
        }
        public static void SetValue(string s, IValue value)
        {
            foreach (Dictionary<string, IValue> context in _stackValues)
                if (context.ContainsKey(s)) { context[s] = value; return; }
            _stackValues.Peek().Add(s, value);
        }
        public static bool IsExist(string s)
        {
            foreach (Dictionary<string, IValue> context in _stackValues)
                if (context.ContainsKey(s)) return true;
            return false;
        }

        private static System.Exception Error(string s)
        {
            return new System.Exception();
        }
    }
}