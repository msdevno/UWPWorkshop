## Objective

Building on exercise 6 we are going to make a collection of Customers that we can bind to

1. Introduce a new class called Customers and make it inherit List<Customer> from the 
   System.Collections.Generic namespace and also add a couple of customers by default:
   
        public class Customers : List<Customer>
        {
            public Customers()
            {
                Add(new Customer { FirstName = "John", LastName = "Doe" });
                Add(new Customer { FirstName = "Someone", LastName = "Else" });
            }
        }
   
2. Add an instance of Customers as a resource in the page at the top within the Page element:

        <Page.Resources>
            <local:Customers x:Key="customers"/>
        </Page.Resources>

3. Remove the Customer items hard-coded within the ListBox
4. Set the ItemsSource property to be connected to the resource. Your ListBox should look like:

        <ListBox x:Name="listBox" ItemsSource="{StaticResource customers}">
            <ListBox.ItemTemplate>
                <DataTemplate>
                    <StackPanel Orientation="Horizontal">
                        <TextBlock Text="{Binding FirstName}"/>
                        <TextBlock Text="{Binding LastName}" Margin="8,0,0,0"/>
                    </StackPanel>
                </DataTemplate>
            </ListBox.ItemTemplate>
        </ListBox>

5. After the ListBox, lets add a button that will add Customers:

        <Button Click="AddClicked">Add</Button>

6. In the code-behind we can now implement it to get the resource from the page and then add to it 
   directly.
   
        private void AddClicked(object sender, RoutedEventArgs e)
        {
            var customers = Resources["customers"] as Customers;
            customers.Add(new Customer { FirstName = "New", LastName = "Customer" });
        }
   
    This will not be reflected in the view yet. 
    
7. In order for the view to be able to respond to the change to the collection of customers, we need
   to implement one of the interfaces that the view recognises. One of these are called 
   INotifyCollectionChanged. There is also a concrete implementation that we can use and inherit from
   instead of the List<Customer>. In the System.Collections.ObjectModel namespace sits an implementation
   called ObservableCollection. Lets change to this:
   
        public class Customers : ObservableCollection<Customer>
        {
            public Customers()
            {
                Add(new Customer { FirstName = "John", LastName = "Doe" });
                Add(new Customer { FirstName = "Someone", LastName = "Else" });
            }
        }

Your changes to the collection should now be reflected.