import socket
import hashlib

# valid request
#msgFromClient = b'\xff\xff\xff\xff\x05\x02\xedZ\x81\xad\x00\x00\x00\x4etest@test.de::9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08'
msgFromClient = b'\xff\xff\xff\xff\x05\x04\xab\xacH\x1a\x00\x00\x00\x51test@test.de::9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08::\x04'
# invalid request
# msgFromClient = b'\xff\xff\xff\xff\x05\x02A\xee*I\x00\x00\x00\x02\xaa\xaa'
serverAddressPort = ("vollsm.art", 42069)
serverAddressPort = ("127.0.0.1", 42069)
bufferSize = 16384

# Create a UDP socket at client side
UDPClientSocket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
# Send to server using created UDP socket
UDPClientSocket.sendto(msgFromClient, serverAddressPort)
msgFromServer = UDPClientSocket.recvfrom(bufferSize)
msg = "Message from Server {}".format(msgFromServer[0])

print(msg)
