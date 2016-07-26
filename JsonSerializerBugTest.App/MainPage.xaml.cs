using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using JsonSerializerBugTest.Models;


namespace JsonSerializerBugTest.App
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }


        private async void OnTestPublic(object sender, RoutedEventArgs e)
        {
            await TestDeserialization(new WidgetPublic
            {
                Description = "I have a public constructor."
            });
        }

        private async void OnTestInternal(object sender, RoutedEventArgs e)
        {
            await TestDeserialization(WidgetInternal.Create("I have an internal constructor."));
        }

        private async Task TestDeserialization<T>(T item)
        {
            DeserializationResult.Text = "";

            try
            {
                var serializer = new DataContractJsonSerializer(typeof(T));

                var stream = new MemoryStream();
                serializer.WriteObject(stream, item);

                stream.Seek(0, SeekOrigin.Begin);
                dynamic clone = serializer.ReadObject(stream);

                DeserializationResult.Text = clone?.Description ?? "";
            }
            catch (Exception ex)
            {
                var dialog = new MessageDialog(ex.ToString());
                await dialog.ShowAsync();
            }
        }
    }
}