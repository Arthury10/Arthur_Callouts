using Rage;
using System.Windows.Forms;

namespace ArthurCallouts
{
    internal static class Settings
    {
        internal static bool SuspiciousPerson = true;
        internal static Keys EndCall = Keys.End;
        internal static Keys Dialog = Keys.Y;
        //internal static bool Persuit = true;
        //internal static bool Suspects = false;

        // internal static bool ActivateAIBackup = true;
        // internal static bool HelpMessages = true;
        // internal static Keys EndCall = Keys.End;
        // internal static Keys Dialog = Keys.Y;
        internal static void LoadSettings()
        {
            Game.LogTrivial("[LOG]: Carregando configurações do arquivo ArthurCallouts.");
            var path = "Plugins/LSPDFR/ArthurCallouts/ArthurCallouts.ini";
            var ini = new InitializationFile(path);
            ini.Create();
            SuspiciousPerson = ini.ReadBoolean("Callouts", "SuspiciousPerson", true);
            EndCall = ini.ReadEnum("Keys", "EndCall", Keys.End);
            Dialog = ini.ReadEnum("Keys", "Dialog", Keys.Y);
            // SuspiciousPerson = ini.ReadBoolean("Callouts", "Persuit", true);
            // ActivateAIBackup = ini.ReadBoolean("Settings", "ActivateAIBackup", true);
            // ActivateAIBackup = ini.ReadBoolean("Settings", "HelpMessages", true);
            // EndCall = ini.ReadEnum("Keys", "EndCall", Keys.End);
            // Dialog = ini.ReadEnum("Keys", "Dialog", Keys.Y);

            // events
            //Suspects = ini.ReadBoolean("Events", "Suspects", true);

        }
        public static readonly string PluginVersion = "0.0.1";
    }
}
