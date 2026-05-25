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

        // Throttle reconnect: không thử lại trong vòng 3 giây kể từ lần thất bại trước
        private DateTime _lastReconnectAttempt = DateTime.MinValue;
        private const int RECONNECT_COOLDOWN_MS = 3000;

        public bool IsConnected
        {
            get
            {
                try
                {
                    // TcpClient.Client (Socket) có thể null sau Close() trong .NET Framework 4.8
                    return _tcpClient != null && _tcpClient.Client != null && _tcpClient.Connected;
                }
                catch
                {
                    return false;
                }
            }
        }

        public PLCModbusManager(string ipAddress, int port = 502)
        {
            _ipAddress = ipAddress;
            _port = port;
        }

        /// <summary>
        /// Đảm bảo kết nối luôn sẵn sàng. Nếu mất kết nối sẽ thử lại.
        /// Có cooldown 3 giây để tránh block vòng lặp đọc dữ liệu.
        /// </summary>
        public bool EnsureConnection()
        {
            if (IsConnected) return true;

            // Không thử reconnect nếu vừa thất bại trong vòng RECONNECT_COOLDOWN_MS
            if ((DateTime.Now - _lastReconnectAttempt).TotalMilliseconds < RECONNECT_COOLDOWN_MS)
                return false;

            _lastReconnectAttempt = DateTime.Now;

            try
            {
                Console.WriteLine($"Try to reconnect to {_ipAddress}:{_port}...");

                _modbusMaster?.Dispose();
                _modbusMaster = null;

                _tcpClient?.Close();
                _tcpClient = new TcpClient();

                // ConnectAsync().Wait(timeout) — đơn giản, không cần BeginConnect/EndConnect
                var connectTask = _tcpClient.ConnectAsync(_ipAddress, _port);
                if (!connectTask.Wait(TimeSpan.FromSeconds(2)))
                {
                    _tcpClient.Close();
                    Console.WriteLine("Timeout connecting.");
                    return false;
                }

                if (connectTask.IsFaulted || !_tcpClient.Connected)
                {
                    _tcpClient.Close();
                    Console.WriteLine($"Connect failed: {connectTask.Exception?.InnerException?.Message}");
                    return false;
                }

                _modbusMaster = ModbusIpMaster.CreateIp(_tcpClient);
                _modbusMaster.Transport.Retries = 2;
                _modbusMaster.Transport.ReadTimeout = 1500;
                _modbusMaster.Transport.WriteTimeout = 1500;

                // Reset cooldown khi kết nối thành công
                _lastReconnectAttempt = DateTime.MinValue;
                Console.WriteLine("Connected!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Reconnect: {ex.Message}");
                return false;
            }
        }

        // Đánh dấu mất kết nối khi Modbus operation throw — TcpClient.Connected không tự cập nhật
        private void MarkDisconnected()
        {
            try { _tcpClient?.Close(); } catch { }
            _tcpClient = null;
            _modbusMaster = null;
        }

        // --- 1. COILS (Boolean - Read/Write) ---
        public bool[] ReadCoilsSafe(ushort startAddress, ushort numberOfPoints, byte slaveId = 1)
        {
            try
            {
                if (EnsureConnection()) return _modbusMaster.ReadCoils(slaveId, startAddress, numberOfPoints);
            }
            catch { MarkDisconnected(); }
            return null;
        }

        public bool WriteCoilSafe(ushort address, bool value, byte slaveId = 1)
        {
            try
            {
                if (EnsureConnection())
                {
                    _modbusMaster.WriteSingleCoil(slaveId, address, value);
                    return true;
                }
            }
            catch { MarkDisconnected(); }
            return false;
        }

        // --- 2. DISCRETE INPUTS (Boolean - Read Only) ---
        public bool[] ReadInputsSafe(ushort startAddress, ushort numberOfPoints, byte slaveId = 1)
        {
            try
            {
                if (EnsureConnection()) return _modbusMaster.ReadInputs(slaveId, startAddress, numberOfPoints);
            }
            catch { MarkDisconnected(); }
            return null;
        }

        // --- 3. HOLDING REGISTERS (16-bit - Read/Write) ---
        public ushort[] ReadHoldingRegistersSafe(ushort startAddress, ushort numberOfPoints, byte slaveId = 1)
        {
            try
            {
                if (EnsureConnection()) return _modbusMaster.ReadHoldingRegisters(slaveId, startAddress, numberOfPoints);
            }
            catch { MarkDisconnected(); }
            return null;
        }

        public bool WriteRegisterSafe(ushort address, ushort value, byte slaveId = 1)
        {
            try
            {
                if (EnsureConnection())
                {
                    _modbusMaster.WriteSingleRegister(slaveId, address, value);
                    return true;
                }
            }
            catch { MarkDisconnected(); }
            return false;
        }

        // --- 4. INPUT REGISTERS (16-bit - Read Only) ---
        public ushort[] ReadInputRegistersSafe(ushort startAddress, ushort numberOfPoints, byte slaveId = 1)
        {
            try
            {
                if (EnsureConnection()) return _modbusMaster.ReadInputRegisters(slaveId, startAddress, numberOfPoints);
            }
            catch { MarkDisconnected(); }
            return null;
        }

        public void Dispose()
        {
            _isDisposing = true;
            _tcpClient?.Close();
            _modbusMaster?.Dispose();
        }
    }
}