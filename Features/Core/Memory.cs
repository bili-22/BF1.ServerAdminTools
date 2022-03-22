using BF1.ServerAdminTools.Common.Helper;

namespace BF1.ServerAdminTools.Features.Core
{
    public class Memory
    {
        private static IntPtr windowHandle;
        private static IntPtr processHandle;
        private static int processId;
        private static long processBaseAddress;

        public Memory()
        {
            // ...
        }

        public static bool Initialize(string ProcessName)
        {
            try
            {
                LoggerHelper.Info($"目标程序名称 {ProcessName}");
                var pArray = Process.GetProcessesByName(ProcessName);
                if (pArray.Length > 0)
                {
                    var process = pArray[0];
                    windowHandle = process.MainWindowHandle;
                    LoggerHelper.Info($"目标程序窗口句柄 {windowHandle}");
                    processId = process.Id;
                    LoggerHelper.Info($"目标程序进程ID {processId}");
                    processHandle = WinAPI.OpenProcess(
                        ProcessAccessFlags.VirtualMemoryRead |
                        ProcessAccessFlags.VirtualMemoryWrite |
                        ProcessAccessFlags.VirtualMemoryOperation,
                        false, processId);
                    LoggerHelper.Info($"目标程序进程句柄 {processHandle}");
                    if (process.MainModule != null)
                    {
                        processBaseAddress = process.MainModule.BaseAddress.ToInt64();
                        LoggerHelper.Info($"目标程序主模块基址 0x{processBaseAddress:x}");
                        return true;
                    }
                    else
                    {
                        LoggerHelper.Error($"发生错误，目标程序主模块基址为空");
                        return false;
                    }
                }
                else
                {
                    LoggerHelper.Error($"发生错误，未发现目标进程");
                    return false;
                }
            }
            catch (Exception ex)
            {
                LoggerHelper.Error($"战地1内存模块初始化异常", ex);
                return false;
            }
        }

        public static void CloseHandle()
        {
            if (processHandle != IntPtr.Zero)
                WinAPI.CloseHandle(processHandle);
        }

        public static IntPtr GetWindowHandle()
        {
            return windowHandle;
        }

        public static IntPtr GetHandle()
        {
            return processHandle;
        }

        public static long GetBaseAddress()
        {
            return processBaseAddress;
        }

        public static bool IsTopMostWindow()
        {
            return windowHandle == WinAPI.GetForegroundWindow();
        }

        public static void SetForegroundWindow()
        {
            WinAPI.SetForegroundWindow(windowHandle);
        }

        public static int GetProcessId()
        {
            return processId;
        }

        private static long GetPtrAddress(long pointer, int[] offset)
        {
            if (offset != null)
            {
                byte[] buffer = new byte[8];
                WinAPI.ReadProcessMemory(processHandle, pointer, buffer, buffer.Length, out _);

                for (int i = 0; i < (offset.Length - 1); i++)
                {
                    pointer = BitConverter.ToInt64(buffer, 0) + offset[i];
                    WinAPI.ReadProcessMemory(processHandle, pointer, buffer, buffer.Length, out _);
                }

                pointer = BitConverter.ToInt64(buffer, 0) + offset[offset.Length - 1];
            }

            return pointer;
        }

        public static T Read<T>(long basePtr, int[] offsets) where T : struct
        {
            byte[] buffer = new byte[Marshal.SizeOf(typeof(T))];
            WinAPI.ReadProcessMemory(processHandle, GetPtrAddress(basePtr, offsets), buffer, buffer.Length, out _);
            return ByteArrayToStructure<T>(buffer);
        }

        public static T Read<T>(long address) where T : struct
        {
            byte[] buffer = new byte[Marshal.SizeOf(typeof(T))];
            WinAPI.ReadProcessMemory(processHandle, address, buffer, buffer.Length, out _);
            return ByteArrayToStructure<T>(buffer);
        }

        public static void Write<T>(long basePtr, int[] offsets, T value) where T : struct
        {
            byte[] buffer = StructureToByteArray(value);
            WinAPI.WriteProcessMemory(processHandle, GetPtrAddress(basePtr, offsets), buffer, buffer.Length, out _);
        }

        public static void Write<T>(long address, T value) where T : struct
        {
            byte[] buffer = StructureToByteArray(value);
            WinAPI.WriteProcessMemory(processHandle, address, buffer, buffer.Length, out _);
        }

        public static string ReadString(long address, int size)
        {
            byte[] buffer = new byte[size];
            WinAPI.ReadProcessMemory(processHandle, address, buffer, size, out _);

            for (int i = 0; i < buffer.Length; i++)
            {
                if (buffer[i] == 0)
                {
                    byte[] _buffer = new byte[i];
                    Buffer.BlockCopy(buffer, 0, _buffer, 0, i);
                    return Encoding.ASCII.GetString(_buffer);
                }
            }

            return Encoding.ASCII.GetString(buffer);
        }

        public static string ReadString(long basePtr, int[] offsets, int size)
        {
            byte[] buffer = new byte[size];
            WinAPI.ReadProcessMemory(processHandle, GetPtrAddress(basePtr, offsets), buffer, size, out _);

            for (int i = 0; i < buffer.Length; i++)
            {
                if (buffer[i] == 0)
                {
                    byte[] _buffer = new byte[i];
                    Buffer.BlockCopy(buffer, 0, _buffer, 0, i);
                    return Encoding.ASCII.GetString(_buffer);
                }
            }

            return Encoding.ASCII.GetString(buffer);
        }

        public static void WriteString(long basePtr, int[] offsets, string str)
        {
            byte[] buffer = new ASCIIEncoding().GetBytes(str);
            WinAPI.WriteProcessMemory(processHandle, GetPtrAddress(basePtr, offsets), buffer, buffer.Length, out _);
        }

        public static void WriteStringUTF8(long basePtr, int[] offsets, string str)
        {
            byte[] buffer = new UTF8Encoding().GetBytes(str);
            WinAPI.WriteProcessMemory(processHandle, GetPtrAddress(basePtr, offsets), buffer, buffer.Length, out _);
        }

        //////////////////////////////////////////////////////////////////

        public static bool IsValid(long Address)
        {
            return Address >= 0x10000 && Address < 0x000F000000000000;
        }

        private static T ByteArrayToStructure<T>(byte[] bytes) where T : struct
        {
            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            try
            {
                var obj = Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
                if (obj != null)
                    return (T)obj;
                else
                    return default(T);
            }
            finally
            {
                handle.Free();
            }
        }

        private static byte[] StructureToByteArray(object obj)
        {
            int length = Marshal.SizeOf(obj);
            byte[] array = new byte[length];
            IntPtr pointer = Marshal.AllocHGlobal(length);
            Marshal.StructureToPtr(obj, pointer, true);
            Marshal.Copy(pointer, array, 0, length);
            Marshal.FreeHGlobal(pointer);
            return array;
        }
    }
}
