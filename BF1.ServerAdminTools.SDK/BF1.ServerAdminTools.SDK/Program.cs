namespace BF1.ServerAdminTools.SDK;

public class SDKTest
{
    public static async Task Main()
    {
        NettyClient.Key = 7400499997373415712;
        Console.WriteLine("测试客户端");
        Console.ReadLine();
        await NettyClient.Start("127.0.0.1", 23232);
        while (true)
        {
            try
            {
                var res = Console.ReadLine();
                if (int.TryParse(res, out var type))
                {
                    switch (type)
                    {
                        case 0:
                            Console.WriteLine(await NettyClient.GetState());
                            break;
                        case 1:
                            Console.WriteLine(await NettyClient.CheckState());
                            break;
                        case 2:
                            Console.WriteLine(await NettyClient.GetId());
                            break;
                        case 3:
                            Console.WriteLine(await NettyClient.GetServerInfo());
                            break;
                        case 4:
                            Console.WriteLine(await NettyClient.GetServerScore());
                            break;
                    }
                }
            }
            catch(Exception e)
            {

            }
        }
    }
}