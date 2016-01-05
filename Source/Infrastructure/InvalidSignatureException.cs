using System;
using System.Reflection;

namespace UWPWorkshop.Infrastructure
{
    /// <summary>
    /// Exception that is thrown when signature of a method does not match
    /// how it is called. Typically used when dynamically invoking a <see cref="WeakDelegate"/>
    /// </summary>
    public class InvalidSignatureException : ArgumentException
    {
        /// <summary>
        /// Initialzes a new instance of <see cref="InvalidSignatureException"/>
        /// </summary>
        /// <param name="expectedSignature"><see cref="MethodInfo"/> that represents the expected signature</param>
        public InvalidSignatureException(MethodInfo expectedSignature) : base(string.Format("Method '{0}' was invoked with the wrong signature, expected: {1}", expectedSignature.Name, expectedSignature.ToString()))
        {
        }
    }
}
