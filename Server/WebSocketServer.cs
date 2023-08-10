using Rage;
using System;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;

namespace ArthurCallouts.Server
{
    internal class WebSocketServer : IDisposable
    {
        private HttpListener _listener;
        private CancellationTokenSource _cts;

        public WebSocketServer()
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add("http://localhost:3001/");
            _cts = new CancellationTokenSource();
        }

        public void Start()
        {
            _listener.Start();
            System.Threading.Tasks.Task.Run(() => ListenLoop(), _cts.Token);
        }

        private async System.Threading.Tasks.Task ListenLoop()
        {
            while (!_cts.IsCancellationRequested)
            {
                HttpListenerContext context = await _listener.GetContextAsync();

                if (context.Request.IsWebSocketRequest)
                {
                    HttpListenerWebSocketContext wsContext = await context.AcceptWebSocketAsync(null);
                    WebSocket webSocket = wsContext.WebSocket;

                    System.Threading.Tasks.Task.Run(() => SendTimeLoop(webSocket), _cts.Token);
                }
                else
                {
                    context.Response.StatusCode = 400;
                    context.Response.Close();
                }
            }
        }

        private async System.Threading.Tasks.Task SendTimeLoop(WebSocket webSocket)
        {
            while (webSocket.State == WebSocketState.Open && !_cts.IsCancellationRequested)
            {
                string time = "Time: " + World.DateTime;
                byte[] buffer = Encoding.UTF8.GetBytes(time);
                await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, _cts.Token);
                await System.Threading.Tasks.Task.Delay(1000);
            }

            webSocket.Dispose();
        }

        public void Dispose()
        {
            _cts.Cancel();
            _listener.Stop();
        }
    }
}
