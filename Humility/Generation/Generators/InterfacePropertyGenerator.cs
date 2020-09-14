using System;

namespace Lasm.UAlive
{
    public sealed class InterfacePropertyGenerator : ConstructGenerator
    {
        public string name;
        public Type type;
        public string get;
        public string set;

        public override string Generate(int indent)
        {
            return type.Name + " " + name.LegalMemberName() + " " + "{ " + get + " " + set + "}";
        }

        private InterfacePropertyGenerator() { }

        public static InterfacePropertyGenerator Property(string name, Type type, bool get, bool set)
        {
            var interfaceProp = new InterfacePropertyGenerator();
            interfaceProp.name = name;
            interfaceProp.type = type;
            interfaceProp.get = (get ? "get; " : string.Empty);
            interfaceProp.set = (set ? "set; " : string.Empty);
            return interfaceProp;
        }
    }
}