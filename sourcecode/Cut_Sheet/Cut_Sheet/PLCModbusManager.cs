using System;
using System.Net.Sockets;
using System.Threading;
using Modbus.Device;

namespace Cut_Sheet
{
    public class PLCModbusManager : IDisposable
    {
        private TcpClient _tcpClient;
        private ModbusIpMaster _modbusMaster;
        private readonly string _ipAddress;
        private readonly int _port;
        private bool _isDisposing;

        public bool IsConnected => _tcpClient != null && _tcpClient.Connected;

        public PLCModbusManager(string ipAddress, int port = 502)
        {
            _ipAddress = ipAddress;
            _port = port;
        }

        /// <summary>
        /// Đảm bảo kết nối luôn sẵn sàng. Nếu mất kết nối sẽ thử lại.
        /// </summary>
        public bool EnsureConnection()
        {
            if (IsConnected) return true;

            try
            {
                Console.WriteLine($"Đang thử kết nối lại tới {_ipAddress}...");

                // Giải phóng tài nguyên cũ trước khi tạo mới
                _tcpClient?.Close();
                _tcpClient = new TcpClient();

                // Thiết lập Timeout để không bị treo ứng dụng quá lâu
                var result = _tcpClient.BeginConnect(_ipAddress, _port, null, null);
                var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(3)); // Timeout 3s

                if (!success || !_tcpClient.Connected)
                {
                    _tcpClient.EndConnect(result);
                    return false;
                }

                _modbusMaster = ModbusIpMaster.CreateIp(_tcpClient);
                _modbusMaster.Transport.Retries = 3;
                _modbusMaster.Transport.ReadTimeout = 1000;

                Console.WriteLine("Kết nối thành công!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi Reconnect: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Đọc dữ liệu an toàn với cơ chế tự động thử lại
        /// </summary>
        public ushort[] ReadRegistersSafe(ushort startAddress, ushort numberOfPoints)
        {
            if (_isDisposing) return null;

            try
            {
                if (EnsureConnection())
                {
                    return _modbusMaster.ReadHoldingRegisters(1, startAddress, numberOfPoints);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi đọc: {ex.Message}");
            }
            return null; // Trả về null nếu mọi nỗ lực kết nối/đọc thất bại
        }

        /// <summary>
        /// Ghi dữ liệu an toàn
        /// </summary>
        public bool WriteRegisterSafe(ushort address, ushort value)
        {
            try
            {
                if (EnsureConnection())
                {
                    _modbusMaster.WriteSingleRegister(1, address, value);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi ghi: {ex.Message}");
            }
            return false;
        }

        public void Dispose()
        {
            _isDisposing = true;
            _tcpClient?.Close();
            _modbusMaster?.Dispose();
        }
    }
}