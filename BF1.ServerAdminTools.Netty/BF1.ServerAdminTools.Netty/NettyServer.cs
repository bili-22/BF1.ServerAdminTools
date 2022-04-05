using BF1.ServerAdminTools.Common;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BF1.ServerAdminTools.Netty;

internal class NettyServer
{
    private static IEventLoopGroup bossGroup;
    private static IEventLoopGroup workerGroup;
    public static async Task Start() 
    {
        bossGroup = new MultithreadEventLoopGroup(1);
        workerGroup = new MultithreadEventLoopGroup();
        var bootstrap = new ServerBootstrap();

        bootstrap
            .Group(bossGroup, workerGroup)
            .Channel<TcpServerSocketChannel>()
            .Option(ChannelOption.SoBacklog, 100)
            .Handler(new LoggingHandler("BF1.Boot"))
            .ChildHandler(new ActionChannelInitializer<IChannel>(channel =>
            {
                IChannelPipeline pipeline = channel.Pipeline;
                pipeline.AddLast(new LoggingHandler("BF1.Pipe"));
                pipeline.AddLast(new LengthFieldPrepender(4));
                pipeline.AddLast(new ServerHandler());
            }));

        IChannel boundChannel = await bootstrap.BindAsync(ConfigUtils.Config.Port);
    }
}

class ServerHandler : ChannelHandlerAdapter
{
    public override void ChannelRead(IChannelHandlerContext context, object message)
    {
        var buffer = message as IByteBuffer;
        if (buffer != null)
        {
            IByteBuffer buff = Unpooled.Buffer();
            var key = buffer.ReadLong();
            if (key != ConfigUtils.Config.ServerKey)
            {
                buff.WriteByte(70);
                context.WriteAndFlushAsync(buff);
            }
            var type = buff.ReadByte();
            switch (type)
            {
                //获取状态
                case 0:
                    buff.WriteByte(Globals.IsGameRun ? 0xff : 0x00);
                    buff.WriteByte(Globals.IsToolInit ? 0xff : 0x00);
                    break;
                //刷新状态
                case 1:
                    buff.WriteByte(Core.IsGameRun() ? 0xff : 0x00);
                    buff.WriteByte(Core.HookInit() ? 0xff : 0x00);
                    break;
                //获取用户信息
                case 2:
                    buff.WriteInt(Globals.Config.Remid.Length);
                    buff.WriteString(Globals.Config.Remid, Encoding.UTF8);
                    buff.WriteInt(Globals.Config.Sid.Length);
                    buff.WriteString(Globals.Config.Sid, Encoding.UTF8);
                    buff.WriteInt(Globals.Config.SessionId.Length);
                    buff.WriteString(Globals.Config.SessionId, Encoding.UTF8);
                    buff.WriteInt(Globals.Config.GameId.Length);
                    buff.WriteString(Globals.Config.GameId, Encoding.UTF8);
                    buff.WriteInt(Globals.Config.ServerId.Length);
                    buff.WriteString(Globals.Config.ServerId, Encoding.UTF8);
                    buff.WriteInt(Globals.Config.PersistedGameId.Length);
                    buff.WriteString(Globals.Config.PersistedGameId, Encoding.UTF8);
                    break;
            }
        }
    }

    public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();

    public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
    {
        Console.WriteLine("Exception: " + exception);
        context.CloseAsync();
    }
}
