import random
import socket
import time
import sqlite3
import hashlib
from functions import *

bufferSize = 1024
port = 42069

# addStudent("Informatik", "Max", "Mustermann", "test@test.de", "test")
print(validateStudent("test@test.de", "test"))

server_socket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
server_socket.bind(('', port))
print('The server is ready to receive')

while True:
    message, address = server_socket.recvfrom(bufferSize)
    # print the address of the client
    print('Client connected from: ', address)
    message = message.upper()
    # returns message and address to client
    server_socket.sendto(message, address)
