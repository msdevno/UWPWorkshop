## Objectives

The purpose of this exercise is to learn about databinding.


1. Lets start by creating a new class called Customer that we will be using throughout. 
   Right click the project in solution explorer and select Add -> Class. Call it Customer.
   
   Add two properties to it; FirstName and LastName.
   
        public class Customer
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
   
2. Remove the button from the previous exercise and lets create a new ListBox where we add
   a couple of items to it by using instances of the new class directly.
   
        <ListBox Grid.Row="1">
            <local:Customer FirstName="John" LastName="Doe"/>
            <local:Customer FirstName="Someone" LastName="Else"/>
        </ListBox>
   
   Running this will show two items, but you can't make sense out of it since it is displaying
   the typename rather than the content. The reason for this is that we have not given it a
   template describing how to visualize it.
        
3. Add a name to the listBox and hook up the SelectionChanged event        

        <ListBox x:Name="listBox" Grid.Row="1" SelectionChanged="ListBox_SelectionChanged">
            <local:Customer FirstName="John" LastName="Doe"/>
            <local:Customer FirstName="Someone" LastName="Else"/>
        </ListBox>
        
4. In the code-behind (MainPage.xaml.cs), add a handler in the class to display a dialog with the customer information:

        private async void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var customer = (Customer)listBox.SelectedItem;
            var dialog = new MessageDialog($"Customer : {customer.FirstName} {customer.LastName}");
            await dialog.ShowAsync();
        }
        
5. On any ItemsControl, including the ListBox there is a property called ItemTemplate. This
   is the one we are going to use to describe how we want it to look like.
   Add the following inside the ListBox before the items:
   
        <ListBox.ItemTemplate>
            <DataTemplate>
                <StackPanel Orientation="Horizontal">
                    <TextBlock Text="{Binding FirstName}"/>
                    <TextBlock Text="{Binding LastName}" Margin="8,0,0,0"/>
                </StackPanel>
            </DataTemplate>
        </ListBox.ItemTemplate>
   
6. Lets wrap the ListBox in a Grid to indroduce a new TextBox that we can use in binding.

        <Grid Grid.Row="1">
            <Grid.RowDefinitions>
                <RowDefinition Height="30"/>
                <RowDefinition Height="*"/>
            </Grid.RowDefinitions>
            
            <ListBox> 
            
                ...
            
            </ListBox>
        </Grid>
        
7. Inside the Grid right before the ListBox we want to add a TextBox that can be bound to the SelectedItem.
   This is probably a good time to remove the SelectionChanged event handler.
   
        <TextBox Text="{Binding SelectedItem.FirstName, ElementName=ListBox}"/>
        
   The Binding markup expression has a few properties on it, the default property is called Path and is not
   necessary to specify in most cases. The path is the actual property navigation path of the object hierarchy
   you are bound to. If we didn't specify anything else, the binding would be relative to whatever is the 
   data context for the control. In our case at this stage that would be null. The ElementName specifies
   a source overriding the default source being the current data context. We point it directly to the
   ListBox and bind through the SelectedItem and then the FirstName property on the Customer instance
   we're expecting.
   
8. Lets redefine our Grid so that we can have a Master / Detail view. Wrap everything up in a StackPanel
   with Vertical orientation. Move the ListBox outside of the Grid and into the StackPanel after the Grid.
   Next we are going to create the layout of a form with labels and TextBoxes introducing 2 columns; 
   one for the label and one for the TextBoxes. In addition we don't want the second row to fill the
   remainder of the area but be same height as the first; 30. Also, the outer StackPanel should have the
   Grid.Row="1" attribute at this stage. The final result should like the following:
   
        <StackPanel Orientation="Vertical" Grid.Row="1">
            <Grid>
                <Grid.ColumnDefinitions>
                    <ColumnDefinition Width="100"/>
                    <ColumnDefinition Width="*"/>
                </Grid.ColumnDefinitions>
                <Grid.RowDefinitions>
                    <RowDefinition Height="30"/>
                    <RowDefinition Height="30"/>
                </Grid.RowDefinitions>

                <TextBox Text="{Binding SelectedItem.FirstName, ElementName=listBox}"/>
            </Grid>
            <ListBox x:Name="listBox">
                <ListBox.ItemTemplate>
                    <DataTemplate>
                        <StackPanel Orientation="Horizontal">
                            <TextBlock Text="{Binding FirstName}"/>
                            <TextBlock Text="{Binding LastName}" Margin="8,0,0,0"/>
                        </StackPanel>
                    </DataTemplate>
                </ListBox.ItemTemplate>

                <local:Customer FirstName="John" LastName="Doe"/>
                <local:Customer FirstName="Someone" LastName="Else"/>
            </ListBox>
        </StackPanel>
   
9. Inside the Grid we are now going to create the form. Add a TextBlock for the label place the
   label in the first column and the TextBox in the second. Then create another TextBlock / TextBox
   pair and set the row to 1 for these and update the binding to be bound to the LastName.
   
        <TextBlock Text="FirstName" 
                    Grid.Row="0" Grid.Column="0"/>
        <TextBox Text="{Binding SelectedItem.FirstName, ElementName=listBox}"
                    Grid.Row="0" Grid.Column="1"/>
        <TextBlock Text="LastName" 
                    Grid.Row="1" Grid.Column="0"/>
        <TextBox Text="{Binding SelectedItem.LastName, ElementName=listBox}"
                    Grid.Row="1" Grid.Column="1"/>
   
10. We can make our bindings a bit more maintainable at this stage, so we don't have to repeat the
    ElementName for every element being bound to the same element. We do this by setting the DataContext
    on the Grid representing the form and in fact, we can make the DataContext the SelectedItem and
    then everything is relative bindings to this. On the Grid element we are going to set the DataContext
    with a Binding expression:
    
        <Grid DataContext="{Binding SelectedItem, ElementName=listBox}">
        
11. On the bindings inside the form, we can now simplify by removing the SelectedItem part of the path
    and also removing the ElementName property:
    
        <TextBlock Text="FirstName" 
                    Grid.Row="0" Grid.Column="0"/>
        <TextBox Text="{Binding FirstName}"
                    Grid.Row="0" Grid.Column="1"/>
        <TextBlock Text="LastName" 
                    Grid.Row="1" Grid.Column="0"/>
        <TextBox Text="{Binding LastName}"
                    Grid.Row="1" Grid.Column="1"/>
    
12. By default all bindings are in a mode called OneWay. This means that it will take the content from the
    source and bind into the target but any changes will not go back. To enable a TwoWay binding you need
    to add Mode=TwoWay into the binding expression    
    
        <TextBlock Text="FirstName" 
                    Grid.Row="0" Grid.Column="0"/>
        <TextBox Text="{Binding FirstName, Mode=TwoWay}"
                    Grid.Row="0" Grid.Column="1"/>
        <TextBlock Text="LastName" 
                    Grid.Row="1" Grid.Column="0"/>
        <TextBox Text="{Binding LastName, Mode=TwoWay}"
                    Grid.Row="1" Grid.Column="1"/>

