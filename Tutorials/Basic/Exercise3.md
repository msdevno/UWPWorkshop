## Objective

The purpose of this exercise is to look at ItemsControl. ItemsControls are used to display multiple items 
and some implementations of an ItemsControl lets you select items as well.

1. Open the MainPag.xaml file
2. Change the Grid definition to only have to rows and lets have the last row fill the remaining space

        <Grid.RowDefinitions>
            <RowDefinition Height="70"/>
            <RowDefinition Height="*"/>
        </Grid.RowDefinitions>

3. Remove the TextBox and Button.
4. Add an ItemsControl with 3 items and put it inside row 1 of the grid

        <ItemsControl Grid.Row="1">
            <ContentControl Content="Item1"/>
            <ContentControl Content="Item2"/>
            <ContentControl Content="Item3"/>
        </ItemsControl>
        
5. We can change how the items within the ItemsControl flows by changing what is called the ItemsPanel.
   Put in an ItemsPanelTemplate with a StackPanel that will wrap the items within the ItemsControl
   
        <ItemsControl Grid.Row="1">
            <ItemsControl.ItemsPanel>
                <ItemsPanelTemplate>
                    <StackPanel Orientation="Horizontal"/>
                </ItemsPanelTemplate>
            </ItemsControl.ItemsPanel>
            
            <ContentControl Content="Item1"/>
            <ContentControl Content="Item2"/>
            <ContentControl Content="Item3"/>
        </ItemsControl>
   
   You can change the orientation to Vertical or use other containers as panels as well. 
   
6. Since ItemsControl is the root for all controls that holds items you can't select any of the items.
   Lets change ItemsControl to be a ListBox and all the items to be ListBoxItem:
   
        <ListBox Grid.Row="1">
            <ListBox.ItemsPanel>
                <ItemsPanelTemplate>
                    <StackPanel Orientation="Vertical"/>
                </ItemsPanelTemplate>
            </ListBox.ItemsPanel>
            
            <ListBoxItem Content="Item1"/>
            <ListBoxItem Content="Item2"/>
            <ListBoxItem Content="Item3"/>
        </ListBox>
   
7. Lets hook up an event handler when an item is selected. Give the ListBox a name, so that we can access
   it from the code-behind. Hook up the SelectionChanged event.

        <ListBox x:Name="listBox" Grid.Row="1" SelectionChanged="listBox_SelectionChanged">
            <ListBox.ItemsPanel>
                <ItemsPanelTemplate>
                    <StackPanel Orientation="Vertical"/>
                </ItemsPanelTemplate>
            </ListBox.ItemsPanel>
            
            <ListBoxItem Content="Item1"/>
            <ListBoxItem Content="Item2"/>
            <ListBoxItem Content="Item3"/>
        </ListBox>

8. In the code-behind (MainPage.xaml.cs) file - add the following event handler to the class:

        private async void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dialog = new MessageDialog(((ListBoxItem)listBox.SelectedItem).Content.ToString());
            await dialog.ShowAsync();
        }
   
