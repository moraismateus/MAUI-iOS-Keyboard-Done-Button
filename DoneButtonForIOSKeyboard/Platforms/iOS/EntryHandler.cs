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

        var toolbar = new UIToolbar(new RectangleF(0.0f, 0.0f, 50.0f, 44.0f));

        var doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate
        {
            this.PlatformView.EndEditing(true);
        });

        toolbar.Items = new UIBarButtonItem[]
        {
                new UIBarButtonItem (UIBarButtonSystemItem.FlexibleSpace),
                doneButton
        };
        this.PlatformView.InputAccessoryView = toolbar;
    }
}
#endif
