using System;

namespace UWPWorkshop.Infrastructure
{
    public interface ITypeDiscoverer
    {
        Type FindByName(string name);

        Type FindByFullName(string @namespace, string name);
    }
}
