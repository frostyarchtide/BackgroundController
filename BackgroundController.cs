using System.Reflection;
using MSCLoader;
using UnityEngine;

namespace BackgroundController
{
    public class BackgroundController : Mod
    {
        public override string ID => "BackgroundController";
        public override string Name => "Background Controller";
        public override string Author => "Frosty";
        public override string Version => "1.0";
        public override string Description => "Control whether the game runs in the background or not.";

        public static Keybind ToggleRunInBackground { get; set; }

        public static SettingsCheckBox RunInBackground { get; set; }

        public override void ModSetup()
        {
            SetupFunction(Setup.ModSettings, Mod_Settings);
            SetupFunction(Setup.ModSettingsLoaded, Mod_ModSettingsLoaded);
            SetupFunction(Setup.Update, Mod_Update);
        }

        private void Mod_Settings()
        {
            ToggleRunInBackground = Keybind.Add(this, "toggleRunInBackground", "Toggle Run in Background", KeyCode.RightControl);

            RunInBackground = Settings.AddCheckBox("runInBackground", "Run in Background", false, () => SetRunInBackground());
        }

        private void Mod_ModSettingsLoaded()
        {
            SetRunInBackground();
        }

        private void Mod_Update()
        {
            if (ToggleRunInBackground.GetKeybindDown())
            {
                bool value = RunInBackground.GetValue();
                RunInBackground.SetValue(!value);
                SetRunInBackground();
                ModConsole.Print("Run in background " + (value ? "disabled" : "enabled") + ".");
            }
        }

        private void SetRunInBackground()
        {
            Application.runInBackground = RunInBackground.GetValue();
        }
    }
}
