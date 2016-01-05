using System.Text;
using Autofac;
using Windows.UI.Xaml;


namespace UWPWorkshop.Infrastructure
{

    public class ViewModelService
    {

        public static DependencyProperty ViewModelDependencyProperty =
            DependencyProperty.RegisterAttached("ViewModel", typeof(string), typeof(ViewModelService), null);

        public static void SetViewModel(FrameworkElement target, string viewModel)
        {
            var @namespace = GetNamespaceFrom(target);

            var type = App.Container.Resolve<ITypeDiscoverer>().FindByFullName(@namespace, viewModel);

            var viewModelInstance = App.Container.Resolve(type);
            target.DataContext = viewModelInstance;
        }

        private static string GetNamespaceFrom(FrameworkElement target)
        {
            var namespaces = target.BaseUri.PathAndQuery.Split('/');

            var namespaceBuilder = new StringBuilder();

            namespaceBuilder.Append(typeof(App).Namespace);
            for (var namespaceIndex = 1; namespaceIndex < namespaces.Length - 1; namespaceIndex++) namespaceBuilder.Append($".{namespaces[namespaceIndex]}");

            return namespaceBuilder.ToString();
        }

        public static string GetViewModel(FrameworkElement target)
        {
            if (target.DataContext == null) return string.Empty;
            return target.DataContext.GetType().Name;
        }
    }
}
