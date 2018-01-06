using System;

namespace CPlatypus.Execution.Object
{
    public class PlatypusFunctionAttribute : Attribute
    {
        public string Name { get; }

        public PlatypusFunctionAttribute(string name)
        {
            Name = name;
        }
       
    }
}