import random
import socket
import time
import sqlite3

connection = sqlite3.connect('database.db')
cursor = connection.cursor()

bufferSize = 1024
port = 42069

server_socket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
server_socket.bind(('', port))
print('The server is ready to receive')

while True:
    message, address = server_socket.recvfrom(bufferSize)
    message = message.upper()
    # returns message and address to client
    server_socket.sendto(message, address)
