using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Windows.Storage;

namespace UWPWorkshop.Infrastructure
{
    public class TypeDiscoverer : ITypeDiscoverer
    {
        IEnumerable<Type> _types;

        public TypeDiscoverer()
        {
            CollectTypes();
        }


        public Type FindByName(string name)
        {
            var type = _types.SingleOrDefault(t => t.Name == name);
            return type;
        }


        public Type FindByFullName(string @namespace, string name)
        {
            var type = _types.SingleOrDefault(t => t.Namespace == @namespace && t.Name == name);
            return type;
        }

        void CollectTypes()
        {
            var types = new List<Type>();
            var assemblies = CollectAssemblies();

            foreach (var assembly in assemblies) types.AddRange(assembly.GetTypes());

            _types = types;
        }


        static IEnumerable<Assembly> CollectAssemblies()
        {
            var folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var assemblies = new List<Assembly>();

            IEnumerable<StorageFile> files = null;
            folder.GetFilesAsync().AsTask().ContinueWith(f => files = f.Result).Wait();

            foreach (var file in files)
            {
                if (file.FileType == ".dll" || file.FileType == ".exe")
                {
                    var name = new AssemblyName() { Name = System.IO.Path.GetFileNameWithoutExtension(file.Name) };
                    try
                    {
                        Assembly asm = Assembly.Load(name);
                        assemblies.Add(asm);
                    }
                    catch { }
                }
            }
            return assemblies;
        }
    }
}
