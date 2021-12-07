import random
import socket
import time
import sqlite3
import hashlib
from functions import *

connectedSockets = {}

bufferSize = 16384
port = 42069

server_socket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
server_socket.bind(('', port))
print('The server is ready to receive')

while True:
    message, address = server_socket.recvfrom(bufferSize)
    # print the address of the client
    print('Client connected from: ', address)

    # split message by :: and save it in a list
    messageList = message.decode('utf-8').split("::")
    print(messageList)

    response = ""
    # switch case for second element of the list which is the operation code
    match messageList[2]:
        case "2":
            # check if message contains 7 elements
            if len(messageList) == 7:
                server_socket.sendto(f'{messageList[0]}::{messageList[1]}::1::crc'.encode('utf-8'), address)
                if validateStudent(messageList[5], messageList[6]):
                    #greate random number for the session id and check if it is alwready in connectedSockets
                    sessionID = random.randint(0, 255)
                    while sessionID in connectedSockets:
                        sessionID = random.randint(0, 255)
                    # add the session id to the connectedSockets
                    connectedSockets[sessionID] = [0]
                    server_socket.sendto(f'{messageList[0]}::{sessionID}::3::crc'.encode('utf-8'), address)
                else:
                    server_socket.sendto(f'{messageList[0]}::{messageList[1]}::0::crc::packageLength::1'.encode('utf-8'), address)
            else:
                server_socket.sendto(f'{messageList[0]}::{messageList[1]}::0::crc::packageLength::2'.encode('utf-8'), address)
        case "3":
            pass
        case _:
            pass

