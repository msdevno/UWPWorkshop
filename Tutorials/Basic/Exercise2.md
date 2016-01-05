## Objective

The purpose of this exercise is to look at brushes and how they can be used to style elements.

## Steps

1. Open MainPage.xaml
2. Lets add another row at the top of grid we created in exercise 1 - make it 70 pixels high

    <Grid.RowDefinitions>
        <RowDefinition Height="70"/>
        <RowDefinition Height="40"/>
        <RowDefinition Height="40"/>
    </Grid.RowDefinitions>
    
3. Change the Grid.Row attributes to be 1 and 2 for the TextBox and the Button


    <TextBox x:Name="textBox" Width="100" Height="30" Grid.Row="1"/>
    <Button BorderBrush="Blue" Width="100" Height="30" Click="Button_Click" Grid.Row="2">
        <Button.Content>
            Click me
        </Button.Content>
    </Button>

4. Add a Border with a BorderBrush set to the color #a7a7a7 and BorderThickness to 7 and Height of 70. 
   We want to place the border in the new grid row:
   
   
    <Border BorderBrush="#a7a7a7" BorderThickness="1" Height="70" Grid.Row="0"></Border> 
    
5. Lets add a background to the Border. The Color we used is implicitly converted to a SolidColorBrush object
   by an internal TypeConverter that knows how to convert from the string to the specific brush type. 
   We can explicitly use a brush as well. For the background of the border, we want to have a linear gradient.
   The gradient will go from top to bottom (0,0 - 0,1 - X,Y coordinates). The coordinates range from 0 to 1, 
   relative to the size of the object. Within a gradient, we can put in stops - for simplicity, we only have
   one at the beginning and one at the end. The offsets are also in the range of 0 to 1.
   
   
    <Border BorderBrush="#a7a7a7" BorderThickness="1" Height="70" Grid.Row="0">
        <Border.Background>
            <LinearGradientBrush StartPoint="0,0" EndPoint="0,1">
                <GradientStop Offset="0" Color="#cfcfcf"/>
                <GradientStop Offset="1" Color="#a7a7a7"/>
            </LinearGradientBrush>
        </Border.Background>
    </Border>

 6. The border control is a ContentControl and can only have item as content. To give it more flexibility,
    we are going add a Grid into it and move the background into the Grid instead:

    
    <Border BorderBrush="#a7a7a7" BorderThickness="1" Height="70" Grid.Row="0">
        <Grid>
            <Grid.Background>
                <LinearGradientBrush StartPoint="0,0" EndPoint="0,1">
                    <GradientStop Offset="0" Color="#cfcfcf"/>
                    <GradientStop Offset="1" Color="#a7a7a7"/>
                </LinearGradientBrush>
            </Grid.Background>
        </Grid>
    </Border>

7. Within the Grid we want to have some text. Add a TextBlock and position it in the center both 
   horizontally and vertically:
   
   
    <TextBlock Text="Application Header" HorizontalAlignment="Center" VerticalAlignment="Center"/>
    
8. On the TextBlock we can change the font characteristics


    <TextBlock Text="Application Header"
                FontFamily="Arial"
                FontSize="24"
                HorizontalAlignment="Center" 
                VerticalAlignment="Center"/>


## Encapsulate

When building applications you don't want everything to sit inside one huge file. This applies for everything.
In Xaml we have the concept of UserControl that is a building block for doing just that. 

1. Right click the project and select Add -> New Item. Find User Control in the dialog and call it 
   ApplicationHeader.xaml.

2. Move the border and its content from the MainPage.xaml file into the new file. Put it within the grid
   of the new file.
   
3. Remove the Grid.Row attribute from the Border

4. Open the MainPage.xaml file again. Notice that on top of the file we have a xmlns:local="" attribute for the
   Page tag. This points to the namespace at the root of the project. In place of the Border we moved from
   MainPage.xaml to ApplicationHeader.xaml we will put the following:

   
    <local:ApplicationHeader Grid.Row="0"/>

    This will include the application header user control inside row 0 of the grid.
    
  