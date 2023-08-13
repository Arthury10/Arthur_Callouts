using ArthurCallouts.Server.Modules;
using LSPD_First_Response.Engine.Scripting.Entities;
using Newtonsoft.Json;
using Rage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ArthurCallouts.Server
{
    public class HttpServer : IDisposable
    {
        private HttpListener _listener;
        private readonly Random _Random = new Random();

        private UserHandler _userHandler = new UserHandler();
        private VehicleInformationHandler _vehicleInformationHandler = new VehicleInformationHandler();
        private FineHandler _fineHandler = new FineHandler();

        public HttpServer()
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add("http://localhost:9000/");
        }

        public void Start()
        {
            _listener.Start();
            _listener.BeginGetContext(OnContextReceived, null);
        }

        private void OnContextReceived(IAsyncResult ar)
        {
            try
            {
                // Seu código existente aqui...
                HttpListenerContext context = _listener.EndGetContext(ar);
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                // Iniciar a escuta novamente para a próxima solicitação
                _listener.BeginGetContext(OnContextReceived, null);

                response.Headers.Add("Access-Control-Allow-Origin", "*"); // Permitir qualquer origem
                response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE"); // Métodos permitidos
                response.Headers.Add("Access-Control-Allow-Headers", "Content-Type"); // Cabeçalhos permitidos

                object responseData = null;

                switch (request.HttpMethod)
                {
                    case "OPTIONS":
                        response.StatusCode = 200; // OK
                        break;
                    case "GET":
                        responseData = HandleGetRequest(request);
                        break;
                    case "POST":
                        responseData = HandlePostRequest(request);
                        break;
                    case "PUT":
                        responseData = HandlePutRequest(request);
                        break;
                    case "DELETE":
                        responseData = HandleDeleteRequest(request);
                        break;
                    default:
                        response.StatusCode = 405; // Método não permitido
                        break;
                }

                if (responseData != null)
                {
                    string json = JsonConvert.SerializeObject(responseData);
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(json);
                    response.ContentType = "application/json";
                    response.ContentLength64 = buffer.Length;
                    response.OutputStream.Write(buffer, 0, buffer.Length);
                }

                response.Close();
            }
            catch (Exception ex)
            {
                // Registre ou imprima a exceção para ajudar a diagnosticar o problema
                Game.Console.Print("Erro ao receber o contexto: " + ex.ToString());
            }

        }

        private object HandleGetRequest(HttpListenerRequest request)
        {
            string path = request.Url.AbsolutePath;

            Game.Console.Print("Recebido GET em " + path);


             if (path.StartsWith("/user"))
            {
                return _userHandler.HandleGet(request);

            } else if (path.StartsWith("/vehicleInformation"))
            {
                return _vehicleInformationHandler.HandleGet(request);
            }

            return new { message = "GET request received" };   
            // ...
        }

        private object HandlePostRequest(HttpListenerRequest request)
        {
            string path = request.Url.AbsolutePath;

            if (path.StartsWith("/fines"))
            {
                return _fineHandler.HandlePost(request);
            }

            return new { message = "POST request received" };
        }

        private object HandlePutRequest(HttpListenerRequest request)
        {
            // Implemente sua lógica para lidar com a solicitação PUT aqui
            return new { message = "PUT request received" };
        }

        private object HandleDeleteRequest(HttpListenerRequest request)
        {
            // Implemente sua lógica para lidar com a solicitação DELETE aqui
            return new { message = "DELETE request received" };
        }

        public void Dispose()
        {
            _listener?.Stop();
        }
    }
}
