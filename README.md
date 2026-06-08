# 20260501_Aldila_GiamSatMayCatSheet
IP PLC: 192.168.1.220
IP Raspberry: LAN: 192.168.1.10
pi - raspberry

pass login to config: admin - Admin@123456

có log data lên dynamic 365 thông qua API
URL: https://192.168.96.10/aldila-portlet/service/savePrepregCuttingValidator
Body:{
    "stationName": "PPG-01",
    "prepregItemId": "M02006",
    "prepregItemName": "TR350C150S-150/25, 39.4'"
    "prepregOrderItemId": "M02006",
    "prepregOrderItemName": "TR350C150S-150/25, 39.4'",
    "scannedDateTime": "27/05/2026 09:26 AM",
    "result": "PASSED"
}
result có 2 giá trị PASSED và FAILED
