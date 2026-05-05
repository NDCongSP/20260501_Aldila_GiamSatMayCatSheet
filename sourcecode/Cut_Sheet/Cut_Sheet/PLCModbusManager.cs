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
                Console.WriteLine($"Try to reconnect to {_ipAddress}...");

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

                Console.WriteLine("Connected!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Reconnect: {ex.Message}");
                return false;
            }
        }

        // --- 1. COILS (Boolean - Read/Write) ---
        public bool[] ReadCoilsSafe(ushort startAddress, ushort numberOfPoints, byte slaveId = 1)
        {
            try
            {
                if (EnsureConnection()) return _modbusMaster.ReadCoils(slaveId, startAddress, numberOfPoints);
            }
            catch { }
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
            catch { }
            return false;
        }

        // --- 2. DISCRETE INPUTS (Boolean - Read Only) ---
        public bool[] ReadInputsSafe(ushort startAddress, ushort numberOfPoints, byte slaveId = 1)
        {
            try
            {
                if (EnsureConnection()) return _modbusMaster.ReadInputs(slaveId, startAddress, numberOfPoints);
            }
            catch { }
            return null;
        }

        // --- 3. HOLDING REGISTERS (16-bit - Read/Write) ---
        public ushort[] ReadHoldingRegistersSafe(ushort startAddress, ushort numberOfPoints, byte slaveId = 1)
        {
            try
            {
                if (EnsureConnection()) return _modbusMaster.ReadHoldingRegisters(slaveId, startAddress, numberOfPoints);
            }
            catch { }
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
            catch { }
            return false;
        }

        // --- 4. INPUT REGISTERS (16-bit - Read Only) ---
        public ushort[] ReadInputRegistersSafe(ushort startAddress, ushort numberOfPoints, byte slaveId = 1)
        {
            try
            {
                if (EnsureConnection()) return _modbusMaster.ReadInputRegisters(slaveId, startAddress, numberOfPoints);
            }
            catch { }
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