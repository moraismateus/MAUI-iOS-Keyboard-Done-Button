# Done Button For IOS Keyboard

This repository demonstrates how to add a tool bar on the bottom of the screen so users can unfocus the softkeyboard on iOS using a .NET MAUI handler(you can read more about .NET MAUI handler here: https://learn.microsoft.com/en-us/dotnet/maui/user-interface/handlers/customize?view=net-maui-7.0).

(This is a temporary workaround until this is fixed in .NET MAUI for some other controls, you can apply the same principle to other controls like the SearchBar or the EntryCell).

 - First you need to create a class to be your MAUI handler, as this class will only be used by iOS I saved in the platform specific folder:

<img src="https://user-images.githubusercontent.com/58345161/211709827-5efdd49c-3891-4e65-a423-f68a1e7c66dc.png" width="400" >

```csharp
//this guarantees that this handler will only be used by the iOS application
#if IOS
using Microsoft.Maui.Platform;
using UIKit;
using System.Drawing;

namespace DoneButtonForIOSKeyboard;

public class EntryHandler : Microsoft.Maui.Handlers.EntryHandler
{
    protected override void ConnectHandler(MauiTextField platformView)
    {
        base.ConnectHandler(platformView);

        //Create a new toolbar.
        var toolbar = new UIToolbar(new RectangleF(0.0f, 0.0f, 50.0f, 44.0f));

        //Create a new UIBarButton with a delegate to clear the focus on the Entry by calling a method that forces the text to stop being edited.
        var doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate
        {
            this.PlatformView.EndEditing(true);
        });

        //Add the button to the toolbar previosly created
        toolbar.Items = new UIBarButtonItem[]
        {
                new UIBarButtonItem (UIBarButtonSystemItem.FlexibleSpace),
                doneButton
        };
        this.PlatformView.InputAccessoryView = toolbar;
    }
}
#endif
```
 - Second you just need to call the handler on the class **MauiProgram.cs**  using the method **.ConfigureMauiHandlers**:
```csharp
using Microsoft.Extensions.Logging;

namespace DoneButtonForIOSKeyboard;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			}).ConfigureMauiHandlers(handlers =>
            {
//The handler will only be called if the target platform is iOS
#if IOS
                handlers.AddHandler<Entry, EntryHandler>();
#endif
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
```
