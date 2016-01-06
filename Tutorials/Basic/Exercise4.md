## Objective

The purpose of this exercise is to look at how we can change how the look of a control and
how we can make the look reusable for other instances of the same control.

1. Start by taking out the ListBox from exercise 3
2. Add a button sitting row 1 of the grid:

        <Button Width="100" Height="30" Grid.Row="1">
            <Button.Content>Click me</Button.Content>
        </Button>
        
3. Lets change the template of the control, make it a round simple button

        <Button Width="100" Height="30" Grid.Row="1">
            <Button.Content>Click me</Button.Content>

            <Button.Template>
                <ControlTemplate>
                    <Border x:Name ="border" BorderBrush="Black" Background="#a7a7a7" CornerRadius="4" BorderThickness="2">
                        <ContentPresenter HorizontalAlignment="Center" VerticalAlignment="Center"/>
                    </Border>
                </ControlTemplate>
            </Button.Template>
        </Button>

    Notice the ControlTemplate instance - its content will define how the button looks like. We just added a border with some
    corner radius to it. Running the solution now and you should have a button with rounded corners.
    
### Static Resources

The template can be made reusable as a resource in a resource dictionary at any level of the solution. 

1. Lets start by moving the ControlTemplate and its content to the Page level resources, making it available
   for everything within the Page. Once put into a ResourceDictionary, it needs a key. At the top of the file,
   underneath the Page begin tag, add the following:
    
        <Page.Resources>
            <ControlTemplate x:Key="buttonStyle">
                <Border x:Name ="border" BorderBrush="Black" Background="#a7a7a7" CornerRadius="4" BorderThickness="2">
                    <ContentPresenter HorizontalAlignment="Center" VerticalAlignment="Center"/>
                </Border>
            </ControlTemplate>
        </Page.Resources>

2. Remove the Button.Template and its content from the button
3. Add a template attribute and lets bring in the resource by using a markup extension called StaticResource

        <Button Width="100" Height="30" Grid.Row="1" Template="{StaticResource buttonStyle}">
            <Button.Content>Click me</Button.Content>
        </Button>
        

With this we now have a static resource that can used across multiple buttons - all you need to do is refer to the
template on the button. 


### Styles

We can also create styles that take the control template and applies it. 
Styles can be referred to by its name or they can be implicitly applied to whatever target type they 
have as target. Lets build one that implicitly targets all buttons on the page.

1. Remove the key attribute on the ControlTemplate inside the resources element, we don't need it anymore.
2. Introduce a style that targets the Button type inside the resources

        <Style TargetType="Button">
            <Style.Setters>
                <Setter Property="Template">
                    <Setter.Value>
                        <!-- We want the control template in here -->
                    </Setter.Value>
                </Setter>
            </Style.Setters>
        </Style>
        
    The style targets the button and defines property setters. These setters can set any of the properties
    on the target type.
    
    
3. Move the ControlTemplate into the Setter.Value element:
 
        <Style TargetType="Button">
            <Style.Setters>
                <Setter Property="Template">
                    <Setter.Value>
                        <ControlTemplate>
                            <Border x:Name ="border" BorderBrush="Black" Background="#a7a7a7" CornerRadius="4" BorderThickness="2">
                                <ContentPresenter HorizontalAlignment="Center" VerticalAlignment="Center"/>
                            </Border>
                        </ControlTemplate>
                    </Setter.Value>
                </Setter>
            </Style.Setters>
        </Style>

4. Remove the Template attribute on the Button

The button should now implicitly be styled. The style can be moved the App.xaml file as global styles if one wants to
apply it to the entire application.
