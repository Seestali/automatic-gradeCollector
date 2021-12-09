import random
import socket
import time
import sqlite3
import hashlib
from database import *
from threading import Timer
import zlib
import binascii

# ToDo clear connected sockets
connectedSockets = {5: [0]}

bufferSize = 16384
port = 42069
resendInterval = 5

server_socket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
server_socket.bind(('', port))
print('The server is ready to receive')


def resendMessage(message, address):
    server_socket.sendto(message, address)

    # Timer(resendInterval, resendMessage, [messageList, address]).start()


while True:
    message, address = server_socket.recvfrom(bufferSize)
    # print the address of the client
    print('Client connected from: ', address)
    print('Message received: ', message)

    # read first 4 bytes of message and convert to int
    packageNumber = int.from_bytes(message[:4], byteorder='big')
    # read 5th byte of message and convert to int
    userID = int.from_bytes(message[4:5], byteorder='big')
    # read 6th byte of message and convert to int
    opCode = int.from_bytes(message[5:6], byteorder='big')
    # read 7th to 11th byte of message and convert to int
    crc32 = int.from_bytes(message[6:10], byteorder='big')
    # read 12th to 16th byte of message and convert to int
    payloadLength = int.from_bytes(message[10:14], byteorder='big')
    # read 17th to end of message
    payload = message[14:]

    # check if payload length is correct and crc32 is correct
    if len(payload) != payloadLength or message[6:10] != zlib.crc32(message[:6] + message[10:]).to_bytes(4,
                                                                                                         byteorder='big'):
        answer = bytearray()
        answer.extend(packageNumber.to_bytes(4, byteorder='big'))
        answer.extend(userID.to_bytes(1, byteorder='big'))
        answer.extend(b'\x00')
        answer.extend(b'\x07')
        answer.extend(packageNumber.to_bytes(4, byteorder='big'))
        answer.extend(b'::')
        answer.extend(b'\x02')
        crc = zlib.crc32(answer)
        answer[6:6] = crc.to_bytes(4, byteorder='big')
        print('Payload length is incorrect or crc32 is incorrect')
        server_socket.sendto(answer, address)
        continue

    print('Payload length is correct and crc32 is correct')
    answer = bytearray()
    answer.extend(packageNumber.to_bytes(4, byteorder='big'))
    answer.extend(userID.to_bytes(1, byteorder='big'))
    answer.extend(b'\x01')
    answer.extend(b'\x04')
    answer.extend(packageNumber.to_bytes(4, byteorder='big'))
    crc = zlib.crc32(answer)
    answer[6:6] = crc.to_bytes(4, byteorder='big')
    # sending acknowledgement
    server_socket.sendto(answer, address)

    if opCode == 1:
        # check if userId in connectedSockets
        if userID in connectedSockets:
            connectedSockets[userID].remove(packageNumber)
            print(f'got an acknowledgement from {userID} for package {packageNumber}')

    elif opCode == 2:
        # check if login is correct
        email, password = payload.split(b'::')
        print(email, password)
        if validateStudent(email.decode(), password.decode()):
            print('Login successful')
            # create a userId between 1 and 255 and check if it is already in connectedSockets
            while True:
                userID = random.randint(1, 255)
                if userID not in connectedSockets:
                    break
            print('UserID: ', userID)
            connectedSockets[userID] = [0]
            answer = bytearray()
            answer.extend(b'\x00\x00\x00\x00')
            answer.extend(userID.to_bytes(1, byteorder='big'))
            answer.extend(b'\x03')
            answer.extend(b'\x00')
            crc = zlib.crc32(answer)
            answer[6:6] = crc.to_bytes(4, byteorder='big')
            server_socket.sendto(answer, address)
            # ToDo setup a timer to resend the message if it is not received
        else:
            print('Login failed')
            answer = bytearray()
            answer.extend(packageNumber.to_bytes(4, byteorder='big'))
            answer.extend(userID.to_bytes(1, byteorder='big'))
            answer.extend(b'\x00')
            answer.extend(b'\x07')
            answer.extend(packageNumber.to_bytes(4, byteorder='big'))
            answer.extend(b'::')
            answer.extend(b'\x01')
            crc = zlib.crc32(answer)
            answer[6:6] = crc.to_bytes(4, byteorder='big')
            print('login failed')
            server_socket.sendto(answer, address)

    elif opCode == 4:
        # check if userID is in connectedSockets
        if userID in connectedSockets:
            print(payload)
            email, password, semester = payload.split(b'::')
            semester= int.from_bytes(semester, byteorder='big')
            print(email.decode(), password.decode(), semester)
            if validateStudent(email.decode(), password.decode()):
                rawClasses = getModules(getStudentId(email.decode()), semester)
                print(rawClasses)
                classes={}
                for x in rawClasses:
                    print(x, x[0], x[1],x[2],x[3])
                    classes[getModule(x[1])[1]]=x[3]
                print(classes)
            else:
                print('Login failed')
                answer = bytearray()
                answer.extend(packageNumber.to_bytes(4, byteorder='big'))
                answer.extend(userID.to_bytes(1, byteorder='big'))
                answer.extend(b'\x00')
                answer.extend(b'\x07')
                answer.extend(packageNumber.to_bytes(4, byteorder='big'))
                answer.extend(b'::')
                answer.extend(b'\x01')
                crc = zlib.crc32(answer)
                answer[6:6] = crc.to_bytes(4, byteorder='big')
                print('login failed')
                server_socket.sendto(answer, address)

        else:
            answer = bytearray()
            answer.extend(packageNumber.to_bytes(4, byteorder='big'))
            answer.extend(userID.to_bytes(1, byteorder='big'))
            answer.extend(b'\x00')
            answer.extend(b'\x07')
            answer.extend(packageNumber.to_bytes(4, byteorder='big'))
            answer.extend(b'::')
            answer.extend(b'\x02')
            crc = zlib.crc32(answer)
            answer[6:6] = crc.to_bytes(4, byteorder='big')
            print('UserID is not in connectedSockets')
            server_socket.sendto(answer, address)
            continue
