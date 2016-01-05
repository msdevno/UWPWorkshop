using System;

namespace UWPWorkshop.Infrastructure
{
    public class MissingMethodForCommand : ArgumentException
    {
        public MissingMethodForCommand(Type type, string methodName) :
            base(string.Format("Missing method '{0}' on '{1}'", methodName, type.AssemblyQualifiedName)) { }
    }
}
