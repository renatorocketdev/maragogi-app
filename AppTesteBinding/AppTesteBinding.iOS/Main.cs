using ImageButton.iOS;
using ImageCircle.Forms.Plugin.iOS;
using UIKit;
using Xamarin.Forms;

namespace AppTesteBinding.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            Forms.SetFlags("CollectionView_Experimental");
            UIApplication.Main(args, null, "AppDelegate");
            ImageButtonRenderer.Init();
            ImageCircleRenderer.Init();

        }
    }
}