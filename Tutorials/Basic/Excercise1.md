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



    
    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new MessageDialog("Hello world");
        await dialog.ShowAsync();
    }
    
   
2. asdasd