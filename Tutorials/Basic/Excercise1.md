## Objective

The purpose of this exercise is to get a basic feeling of Xaml and how it works by testing out 
different layout possibilities.

## Steps


1. Open MainPage.xaml
2. Add a button within the Grid tag


        <Button Width="100" Height="50">
            <Button.Content>
                Click me
            </Button.Content>
        </Button>

    Note that the Button.Content tag is not really needed. Content is the default property for a ContentControl. 
    

3. In order for us to be able to respond to the button being clicked, we need to hook up an event.
   On the button object there is a property called Click. Lets add it to the tag, making it look like the following:
   

    <Button Width="100" Height="50" Click="Button_Click">
        <Button.Content>
            Click me
        </Button.Content>
    </Button>
   
4. Open the MainPage.xaml.cs by right-clicking the MainPage.xaml in the solution explorer select "View Code"

5. Add the event handler; Button_Click() method inside the class:

    
    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new MessageDialog("Hello world");
        await dialog.ShowAsync();
    }
    
6. Add the following using statement at the top:


    using Windows.UI.Popups;
        

    Running your solution should now have a message dialog popping up when clicking it.
       
6. Add a TextBox tag before the Button


    <TextBox Width="100" Height="30"/>
    
7. Add margin to the button to push the button down (Xaml is not by default using a flow layout)


    <Button Width="100" Height="50" Click="Button_Click" Margin="0,70,0,0">
        <Button.Content>
            Click me
        </Button.Content>
    </Button>
    
8. Give the TextBox a name, so it becomes available in the "code-behind"


    <TextBox x:Name="textBox" Width="100" Height="30"/>

9. Change the event handler to take the input from the textbox into the message being shown:


    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new MessageDialog($"hello world : {textBox.Text}");
        await dialog.ShowAsync();
    }


### Canvas

We can use Canvas as a container for controlling positioning of its content in a different way than margins. 

Wrap the TextBox and the Button in a Canvas and take out the Margin property and replace it with
the Canvas.Top attached dependency property instead:
   
    <Canvas>

        <TextBox x:Name="textBox" Width="100" Height="30"/>
        <Button Width="100" Height="30" Click="Button_Click" Canvas.Top="30">
            <Button.Content>
                Click me
            </Button.Content>
        </Button>

    </Canvas>
   

### Flow layout

In order to get a more flowing layout we have a few options. The simplest being something called a StackPanel.
Change the Canvas to a StackPanel with the vertical orientation and remove the Canvas.Top property setter on the
button.

    <StackPanel Orientation="Vertical">

        <TextBox x:Name="textBox" Width="100" Height="30"/>
        <Button Width="100" Height="30" Click="Button_Click">
            <Button.Content>
                Click me
            </Button.Content>
        </Button>

    </StackPanel>

Any children of the StackPanel, which is a type of ItemsControl, will flow according to the orientation set.


### Grid

Another useful building block is the Grid. The page came with a Grid out of the box. Inside the Grid you can 
create rows and columns. From this definition you can control where you want the children to be placed.

Change the Canvas to be a grid and put in a couple of row definitions and add the Grid.Row attached dependency
property to the children:

    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="40"/>
            <RowDefinition Height="40"/>
        </Grid.RowDefinitions>

        <TextBox x:Name="textBox" Width="100" Height="30" Grid.Row="0"/>
        <Button Width="100" Height="30" Click="Button_Click" Grid.Row="1">
            <Button.Content>
                Click me
            </Button.Content>
        </Button>
    </Grid>
