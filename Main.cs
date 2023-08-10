using Rage;
using LSPD_First_Response.Mod.API;
using ArthurCallouts.Callouts;
using ArthurCallouts.Events;
using System.Reflection;
using ArthurCallouts.VersionChecker;
using System.Runtime;
using System;
using ArthurCallouts.Server;

namespace ArthurCallouts
{
    internal class Main : Plugin
    {
        private WebSocketServer _webSocketServer;
        private HttpServer _HttpServer;
        public override void Finally() 
        {
            _webSocketServer.Dispose();
            _HttpServer.Dispose();
        }

        public override void Initialize()
        {
            Functions.OnOnDutyStateChanged += Functions_OnOnDutyStateChanged;
            Settings.LoadSettings();

            _HttpServer = new HttpServer();
            _HttpServer.Start();
           _webSocketServer = new WebSocketServer();
           _webSocketServer.Start();
        }
        static void Functions_OnOnDutyStateChanged(bool onDuty)
        {
            if (onDuty)
            {
                try
                {
                GameFiber.StartNew(delegate
                {
                    RegisterCallouts();
                    Game.Console.Print();
                    Game.Console.Print("=============================================== Chamadas Brasil por Arthur Ropke ================================================");
                    Game.Console.Print();
                    Game.Console.Print("[LOG]: Chamada e configurações foram carregadas com sucesso");
                    Game.Console.Print("[LOG]: O arquivo de configuração carregou com sucesso");
                    Game.Console.Print("[VERSION]: Detected Version: " + Assembly.GetExecutingAssembly().GetName().Version.ToString());
                    Game.Console.Print("[LOG]: Verificando uma nova versão do ArthurCallouts...");
                    Game.Console.Print();
                    Game.Console.Print("=============================================== Chamadas Brasil por Arthur Ropke ================================================");
                    Game.Console.Print();

                    // You can find all textures/images in OpenIV
                    Game.DisplayNotification("ArthurCallouts", "web_lossantospolicedept", "ArthurCallouts", "~y~v" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + " ~o~por Arthur Ropke", "~b~Carregou com sucesso!");
                    // Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "UnitedCallouts", "~y~Unstable Build", "This is an ~r~unstable build~w~ of UnitedCallouts for testing. You may notice bugs while playing the unstable build.");

                    PluginCheck.isUpdateAvailable();
                    GameFiber.Wait(300);
                    /*if (Settings.HelpMessages)
                    {
                        Game.DisplayHelp("You can change all ~y~keys~w~ in the ~g~UnitedCallouts.ini~w~. Press ~b~" + Settings.EndCall + "~w~ to end a callout.", 5000);
                    }
                    else { Settings.HelpMessages = false; } */
                });
                } catch (Exception ex)
                {
                    // Captura o erro e exibe uma mensagem ou faz o tratamento necessário
                    Game.Console.Print();
                    Game.Console.Print("=============================================== Chamadas Brasil por Arthur Ropke ================================================");
                    Game.Console.Print();
                    Game.Console.Print("[ERRO]: Ocorreu um erro ao carregar as chamadas do ArthurCallouts:");
                    Game.Console.Print(ex.ToString());
                    Game.Console.Print();
                    Game.Console.Print("=============================================== Chamadas Brasil por Arthur Ropke ================================================");
                    Game.Console.Print();
                }
            }
        }
        private static void RegisterCallouts() //Register all your callouts here
        {
            Game.Console.Print();
            Game.Console.Print("================================================== Chamadas Brasil ===================================================");
            Game.Console.Print();
            if (Settings.SuspiciousPerson) { Functions.RegisterCallout(typeof(SuspiciousPerson)); }
            //if (Settings.SuspiciousPerson) { Functions.RegisterCallout(typeof(SuspiciousPerson)); }
            Game.Console.Print("[LOG]: Todas as chamadas do ArthurCallouts.ini foram carregadas com sucesso.");
            Game.Console.Print();
            Game.Console.Print();
            //if (Settings.Suspects) { Functions.RegisterCallout(typeof(Suspects)); }
            Game.Console.Print("[LOG]: Todos os eventos do ArthurCallouts.ini foram carregadas com sucesso.");
            Game.Console.Print();
            Game.Console.Print("================================================== Chamadas Brasil ===================================================");
            Game.Console.Print();
        }
    }
}
