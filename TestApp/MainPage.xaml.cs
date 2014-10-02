using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Windows.Phone.Media.Capture;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TestApp.Resources;

namespace TestApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        private AudioVideoCaptureDevice audioVideoCaptureDevice;
        private bool isFlashOn;
        private BitmapImage MyBitmapImage = new BitmapImage();

        // Constructor
        public MainPage()
        {
            // reads in all the XAML files and creates UI
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            // ApplicationBar is that dumb swipe in options menu
            //BuildLocalizedApplicationBar();
        }

        async private void Button_Click(object sender, RoutedEventArgs e)
        {
            // turn flashlight on
            CameraSensorLocation location = CameraSensorLocation.Back;
            if (this.audioVideoCaptureDevice == null)
            {
                audioVideoCaptureDevice = await AudioVideoCaptureDevice.OpenAsync(location,
                        AudioVideoCaptureDevice.GetAvailableCaptureResolutions(location).First());
            }
            if (!isFlashOn)
            {
                // call the method that controls the light and tell it to turn the light on
                FlashOn(location, VideoTorchMode.On);
                // set the text of the button to say flash off
                FlashButton.Content = "Flash Off";
                // set the bitmap image to the flashlight on image
                MyBitmapImage.UriSource = new Uri("/Assets/Images/flashlight_on.png", UriKind.Relative);
                // apply the bitmap image to the image defined in xaml
                LightbulbDrawableImage.Source = MyBitmapImage;
                // set the bool isFlashOn to true so the program knows to turn the light off on next click
                isFlashOn = true;
            }
            else if (isFlashOn)
            {
                // call the method that controls the light and tell it to turn the light off
                FlashOn(location, VideoTorchMode.Off);
                // set the text of the button to say flash on
                FlashButton.Content = "Flash On";
                // set the bitmap image to the flashlight off image
                MyBitmapImage.UriSource = new Uri("/Assets/Images/flashlight_off.png", UriKind.Relative);
                // apply the bitmap image to the image defined in xaml
                LightbulbDrawableImage.Source = MyBitmapImage;
                // set the bool isFlashOn to false so the program knows to turn the light on on next click
                isFlashOn = false;
            }
        }

        public bool FlashOn(CameraSensorLocation location, VideoTorchMode mode)
        {
            // code that turns light on or off
            var supportedCameraModes = AudioVideoCaptureDevice.GetSupportedPropertyValues(location,
                KnownCameraAudioVideoProperties.VideoTorchMode);
            if ((audioVideoCaptureDevice != null) && (supportedCameraModes.ToList().Contains((UInt32)mode)))
            {
                audioVideoCaptureDevice.SetProperty(KnownCameraAudioVideoProperties.VideoTorchMode, mode);
                return true;
            }
            return false;
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}