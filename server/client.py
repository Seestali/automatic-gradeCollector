import socket
import hashlib

msgFromClient = "12::2::crc::payloadlength::test@test.de::" + hashlib.sha256('test'.encode('utf-8')).hexdigest()
bytesToSend = str.encode(msgFromClient)
serverAddressPort = ("vollsm.art", 42069)
serverAddressPort = ("127.0.0.1", 42069)
bufferSize = 16384

# Create a UDP socket at client side
UDPClientSocket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
# Send to server using created UDP socket
UDPClientSocket.sendto(bytesToSend, serverAddressPort)
msgFromServer = UDPClientSocket.recvfrom(bufferSize)
msg = "Message from Server {}".format(msgFromServer[0])

print(msg)

