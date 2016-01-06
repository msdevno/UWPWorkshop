## Objectives

Building on exercise 5 we are going to look at converting values during binding to be converted
to match the type in the target property. In addition we are going to look at how changes
in a source property being bound to can automatically reflect its change in the frontend.

1. Add an Active property on the Customer object:

        public class Customer
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public bool Active { get; set; }
        }

2. Lets add a TextBlock reflecting status inside the form. To do this we need another row in the grid.

        <TextBlock Text="Status" 
                   Grid.Row="2" Grid.Column="0"/>
        <TextBlock Text="{Binding Active}"
                   Grid.Row="2" Grid.Column="1"/>

    This should show True or False depending on wether or not it is active or not. 
    
3. Lets add a constructor to the Customer to set the Active flag to true by default

        public Customer()
        {
            Active = true;
        }

4. To alter the status we're going to put in a button inside the grid. We will be needing another row in the
   grid. The button should be hooked up to an event handler in the code-behind:
   
        <Button Grid.Row="3" Click="Button_Click">Disable</Button>
   
5. The event handler will deal with changing the status:

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var customer = (Customer)listBox.SelectedItem;
            customer.Active = false;
        }

    This will not reflect any changes in the UI. The object will change state however. 
    
 7. In order for the view to react to the change happening, the view recognises an interface called
    INotifyPropertyChanged. By implementing this, the View will automatically react to the change.
    Make the Customer class implement INotifyPropertyChanged from the System.ComponentModel namespace.
    
    Add the using statement at the top:
    
        using System.ComponentModel;
        
    Then inherit from the interface and implement it:

        public class Customer : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

8. We now need something that triggers the event PropertyChanged whenever something changes. A 
   common pattern is to create a private method that deals with the triggering. Implement
   a private method called OnPropertyChanged:
   
        private void OnPropertyChanged(string property)
        {
            if( PropertyChanged != null )
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

9. With this method in place we can now change our properties to be explicitly implemented to
   call the OnPropertyChanged method when a value is set. Lets do this for the Active property.
   We need to introduce a private backing field called _active and make the property use this
   and on set call the OnPropertyChanged method:
   
        private bool _active;
        public bool Active
        {
            get { return _active; }
            set
            {
                _active = value;
                OnPropertyChanged("Active");
            }
        }

At this stage, you should see the Status change automatically in the view when clicking the button.

          
### ValueConverters

Having the raw value be displayed from something like an Active property might not be what you want.
In many cases you want to visualize this in a different way, either with a different text making more
sense to the user or even an image representing it. This can be achieved by using something called
a ValueConverter. The purpose of it is to take part of the binding process and convert back and forth
between the source and target type.

1. Start by adding a new class called StatusValueConverter and make it implement IValueConverter from
   the Windows.UI.Xaml.Data namespace. We want it to convert from the source so that when the bool
   is true it returns the string "Active" and "Not active" if not:
   
        public class StatusValueConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, string language)
            {
                if ((bool)value == true) return "Active";
                return "Not active";
            }

            public object ConvertBack(object value, Type targetType, object parameter, string language)
            {
                throw new NotImplementedException();
            }
        }
   
2. The ValueConverter can now be brought into the page as a resource to be used. We keep it close for now
   by adding it into the Grids resource dictionary:
   
        <Grid.Resources>
            <local:StatusValueConverter x:Key="statusValueConverter"/>
        </Grid.Resources>
     
3. By changing the Active binding we can now get the correct text displayed:

        <TextBlock Text="{Binding Active, Converter={StaticResource statusValueConverter}}"
                    Grid.Row="2" Grid.Column="1"/>

    