using System;
using Rage;
using System.Net;
using System.Runtime;

namespace ArthurCallouts.VersionChecker
{
    public class PluginCheck
    {
        public static bool isUpdateAvailable()
        {
            string curVersion = Settings.PluginVersion;
            Uri latestVersionUri = new Uri("https://www.lcpdfr.com/applications/downloadsng/interface/api.php?do=checkForUpdates&fileId=20730&textOnly=1");
            WebClient webClient = new WebClient();
            string receivedData = string.Empty;
            try
            {
                //receivedData = webClient.DownloadString(latestVersionUri).Trim();
                receivedData = "0.01".Trim();
            }
            catch (WebException)
            {
                // Game.DisplayNotification("commonmenu", "mp_alerttriangle", "~w~UnitedCallouts Warning", "~r~Failed to check for an update", "Please make sure you are ~y~connected~w~ to the internet or try to ~y~reload~w~ the plugin.");
                Game.Console.Print();
                Game.Console.Print("================================================== ArthurCallouts ===================================================");
                Game.Console.Print();
                Game.Console.Print("[Aviso]: Falha na verificação da atualização.");
                Game.Console.Print("[LOG]: Verifique se você está conectado à Internet ou tente recarregar o plug-in.");
                Game.Console.Print();
                Game.Console.Print("================================================== ArthurCallouts ===================================================");
                Game.Console.Print();
                return false;
            }
            if (receivedData != Settings.PluginVersion)
            {
                //Game.DisplayNotification("commonmenu", "mp_alerttriangle", "~w~UnitedCallouts Warning", "~y~A new Update is available!", "Current Version: ~r~" + curVersion + "~w~<br>New Version: ~o~" + receivedData + "<br>~r~Please update to the latest build!");
                Game.Console.Print();
                Game.Console.Print("================================================== ArthurCallouts ===================================================");
                Game.Console.Print();
                Game.Console.Print("[WARNING]: Uma nova versão do UnitedCallouts está disponível! Atualize para a versão mais recente ou jogue por sua conta e risco.");
                Game.Console.Print("[LOG]: Versão atual:  " + curVersion);
                Game.Console.Print("[LOG]: Nova versão:  " + receivedData);
                Game.Console.Print();
                Game.Console.Print("================================================== ArthurCallouts ===================================================");
                Game.Console.Print();
                return true;
            }
            else
            {
                //Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~UnitedCallouts", "", "Detected the ~g~latest~w~ build of ~y~UnitedCallouts~w~!");
                return false;
            }
        }
    }
}