using System;
using System.Reflection;

namespace UWPWorkshop.Infrastructure
{
    /// <summary>
    /// Exception that is thrown when a target is not alive in a weak reference
    /// </summary>
    public class CannotInvokeMethodBecauseTargetIsNotAlive : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CannotInvokeMethodBecauseTargetIsNotAlive"/>
        /// </summary>
        /// <param name="method"></param>
        public CannotInvokeMethodBecauseTargetIsNotAlive(MethodInfo method) : base(string.Format("Method '{0}' can't be invoked, since target has been collected by the garbage collector", method)) { }
    }
}
