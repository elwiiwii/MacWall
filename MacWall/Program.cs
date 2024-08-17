using System;
using MonoMac.AppKit;
using MonoMac.Foundation;
using Medallion.Shell;

namespace HotkeyAppLauncher
{
    class Program
    {
        static void Main(string[] args)
        {
            NSApplication.Init();
            NSApplication.SharedApplication.Delegate = new AppDelegate();
            NSApplication.Main(args);
        }
    }

    class AppDelegate : NSApplicationDelegate
    {
        public override void DidFinishLaunching(NSNotification notification)
        {
            NSEvent.AddGlobalMonitorForEventsMatchingMask(NSEventMask.KeyDown, (NSEvent theEvent) =>
            {
                if (theEvent.KeyCode == (ushort)NSKey.LeftBracket && theEvent.ModifierFlags.HasFlag(NSEventModifierMask.CommandKeyMask))
                {
                    Console.WriteLine("Command + A pressed. Launching application...");
                    LaunchApplication();
                }
            });
        }

        private void LaunchApplication()
        {
            var command = Command.Run("/usr/bin/open", "-a", "speedcraft");
            command.Wait();
            if (!command.Result.Success)
            {
                Console.WriteLine($"Failed to launch application: {command.Result.StandardError}");
            }
        }
    }
}