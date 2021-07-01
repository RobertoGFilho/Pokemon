
using Foundation;
using System.IO;
using UIKit;

namespace Core.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            SQLitePCL.Batteries_V2.Init();
            global::Xamarin.Forms.Forms.Init();

            var libPath = Path.Combine(
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments),
                "..", "Library", "data");

            if (!Directory.Exists(libPath))
            {
                Directory.CreateDirectory(libPath);
            }

            var dbPath = Path.Combine(libPath, "core.sqlite");
            LoadApplication(new App(dbPath));
            return base.FinishedLaunching(app, options);
        }
    }
}
