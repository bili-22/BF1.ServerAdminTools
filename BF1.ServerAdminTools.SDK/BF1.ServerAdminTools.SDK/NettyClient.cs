using BF1.ServerAdminTools.SDK.Objs;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BF1.ServerAdminTools.SDK;

public class NettyClient
{
    private static MultithreadEventLoopGroup group = new ();
    private static Dictionary<int, Semaphore> CallBack = new();
    private static Dictionary<int, object?> ResBack = new();

    public static IChannel ClientChannel { get; private set; }
    public static bool IsConnect { get; private set; }
    public static long Key { get; set; }
    
    public static async Task Start(string ip, int port) 
    {
        CallBack.Clear();
        for (int a = 0; a < 3; a++)
        {
            CallBack.Add(a, new(0, 5));
        }
        for (int a = 0; a < 3; a++)
        {
            ResBack.Add(a, null);
        }
        var bootstrap = new Bootstrap();
        bootstrap
            .Group(group)
            .Channel<TcpSocketChannel>()
            .Option(ChannelOption.SoBacklog, 100)
            .Handler(new LoggingHandler("BF1.Boot"))
            .Handler(new ActionChannelInitializer<ISocketChannel>(channel =>
            {
                IChannelPipeline pipeline = channel.Pipeline;
                pipeline.AddLast(new LoggingHandler("BF1.Pipe"));
                pipeline.AddLast(new LengthFieldBasedFrameDecoder(1024 * 2000000, 0, 4, 0, 4));
                pipeline.AddLast(new ClientHandler());
            }));
        ClientChannel = await bootstrap.ConnectAsync(new IPEndPoint(IPAddress.Parse(ip), port));
        IsConnect = true;
    }

    public static async Task<StateObj?> GetState()
    {
        if (!IsConnect)
            return null;
        var pack = Unpooled.Buffer()
            .WriteLong(Key)
            .WriteByte(0);
        await ClientChannel.WriteAndFlushAsync(pack);
        return await Task.Run(() =>
        {
            CallBack[0].WaitOne();
            return ResBack[0] as StateObj;
        });
    }

    public static async Task<StateObj?> CheckState()
    {
        if (!IsConnect)
            return null;
        var pack = Unpooled.Buffer()
            .WriteLong(Key)
            .WriteByte(1);
        await ClientChannel.WriteAndFlushAsync(pack);
        return await Task.Run(() =>
        {
            CallBack[1].WaitOne();
            return ResBack[1] as StateObj;
        });
    }

    public static async Task<IdObj?> GetId() 
    {
        if (!IsConnect)
            return null;
        var pack = Unpooled.Buffer()
            .WriteLong(Key)
            .WriteByte(2);
        await ClientChannel.WriteAndFlushAsync(pack);
        return await Task.Run(() =>
        {
            CallBack[2].WaitOne();
            return ResBack[2] as IdObj;
        });
    }

    public static async Task<ServerInfoObj?> GetServerInfo()
    {
        if (!IsConnect)
            return null;
        var pack = Unpooled.Buffer()
            .WriteLong(Key)
            .WriteByte(3);
        await ClientChannel.WriteAndFlushAsync(pack);
        return await Task.Run(() =>
        {
            CallBack[3].WaitOne();
            return ResBack[3] as ServerInfoObj;
        });
    }

    public class ClientHandler : SimpleChannelInboundHandler<IByteBuffer>
    {
        protected override void ChannelRead0(IChannelHandlerContext ctx, IByteBuffer buff)
        {
            if (buff != null)
            {
                var res = buff.ReadByte();
                if (res == 70)
                {
                    Console.WriteLine("Server key error");
                    return;
                }
                switch (res)
                {
                    case 0:
                        ResBack[0] = DecodePack.State(buff);
                        CallBack[0].Release();
                        break;
                    case 1:
                        ResBack[1] = DecodePack.State(buff);
                        CallBack[1].Release();
                        break;
                    case 2:
                        ResBack[2] = DecodePack.ServerInfo(buff);
                        CallBack[2].Release();
                        break;
                }
            }
        }
        public override void ChannelReadComplete(IChannelHandlerContext context)
            => context.Flush();
        public override void HandlerRemoved(IChannelHandlerContext context)
        {
            base.HandlerRemoved(context);
            IsConnect = false;
        }

        public override void ChannelActive(IChannelHandlerContext context)
        {

        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            context.CloseAsync();
        }
    }
}

