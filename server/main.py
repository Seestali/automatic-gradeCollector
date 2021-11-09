import random
import socket

bufferSize = 1024

server_socket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
server_socket.bind(('', 12000))
print('The server is ready to receive')

while True:
    message, address = server_socket.recvfrom(bufferSize)
    message = message.upper()
    # returns message and address to client
    server_socket.sendto(message, address)
