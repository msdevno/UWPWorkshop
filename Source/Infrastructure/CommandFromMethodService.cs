using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;

namespace UWPWorkshop.Infrastructure
{
    public class CommandFromMethodService
    {
        public static DependencyProperty CommandFromMethodDependencyProperty =
            DependencyProperty.RegisterAttached("CommandFromMethod", typeof(string), typeof(CommandFromMethodService), null);


        public static void SetCommandFromMethod(ButtonBase target, string expression)
        {
            target.SetValue(CommandFromMethodDependencyProperty, expression);

            var method = string.Empty;
            string canExecuteWhen = null;

            var segments = expression.Split(';');
            method = segments[0];
            if (segments.Length > 1) canExecuteWhen = segments[1];

            target.DataContextChanged += (s, e) =>
            {
                target.Command = new CommandForMethod(target.DataContext, method, canExecuteWhen, null);
            };

            if( target.Command != null ) target.DataContext = new CommandForMethod(target.DataContext, method, canExecuteWhen, null);
        }

        public static string GetCommandFromMethod(ButtonBase target)
        {
            var expression = target.GetValue(CommandFromMethodDependencyProperty) as string;
            return expression;
        }
    }
}
