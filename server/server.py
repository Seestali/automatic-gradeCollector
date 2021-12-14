import random
import socket
import time
import sqlite3
import hashlib
from database import Database
from threading import Thread
import zlib
import json

db = Database('database.db')

# Dictionary of all users
# ToDo clear connected sockets
connectedSockets = {5: [0]}

bufferSize = 16384
port = 42069
resendInterval = 5
maxResend = 3

# create a socket object which is used to create an udp socket
server_socket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
# bind the socket to a port
server_socket.bind(('', port))
print('The server is ready to receive')


# Function to resend a message after a certain amount of time
def resendMessage(message, address, userID, packageNumber):
    print('thread started')
    count = 0
    time.sleep(resendInterval)
    while packageNumber in connectedSockets[userID] and count < maxResend:
        print('Resending message to user ' + str(userID) + ': ' + str(message))
        server_socket.sendto(message, address)
        count += 1
        time.sleep(resendInterval)
    print('closing thread')
    return


# start listening for messages
while True:
    # save the message and the address of the sender to answer later
    message, address = server_socket.recvfrom(bufferSize)
    print('Client connected from: ', address)
    print('Message received: ', message)

    # read first 4 bytes of message and convert to int which should be the package number
    packageNumber = int.from_bytes(message[:4], byteorder='big')
    # read 5th byte of message and convert to int which should be the user id
    userID = int.from_bytes(message[4:5], byteorder='big')
    # read 6th byte of message and convert to int which should be the operation code
    opCode = int.from_bytes(message[5:6], byteorder='big')
    # read 7th to 11th byte of message and convert to int which should be the crc checksum
    crc32 = int.from_bytes(message[6:10], byteorder='big')
    # read 12th to 16th byte of message and convert to int which should be the length of the payload
    payloadLength = int.from_bytes(message[10:14], byteorder='big')
    # read 17th to end of message which should be the payload
    payload = message[14:]

    # check if payload length is correct and crc32 is correct
    if len(payload) != payloadLength or message[6:10] != zlib.crc32(message[:6] + message[10:]).to_bytes(4,
                                                                                                         byteorder='big'):
        # create an answer in order to send the client an error message
        answer = bytearray()
        answer.extend(packageNumber.to_bytes(4, byteorder='big'))
        answer.extend(userID.to_bytes(1, byteorder='big'))
        answer.extend(b'\x00')
        answer.extend(b'\x00\x00\x00\x07')
        answer.extend(packageNumber.to_bytes(4, byteorder='big'))
        answer.extend(b'::')
        answer.extend(b'\x02')
        crc = zlib.crc32(answer)
        answer[6:6] = crc.to_bytes(4, byteorder='big')
        print('Payload length is incorrect or crc32 is incorrect')
        server_socket.sendto(bytes(answer), address)
        continue

    # send the client an acknowledgment message for the package number
    print('Payload length is correct and crc32 is correct')
    answer = bytearray()
    answer.extend(packageNumber.to_bytes(4, byteorder='big'))
    answer.extend(userID.to_bytes(1, byteorder='big'))
    answer.extend(b'\x01')
    answer.extend(b'\x00\x00\x00\x04')
    answer.extend(packageNumber.to_bytes(4, byteorder='big'))
    crc = zlib.crc32(answer)
    answer[6:6] = crc.to_bytes(4, byteorder='big')
    # sending acknowledgement
    server_socket.sendto(bytes(answer), address)

    # check what the operation code is and do the corresponding action
    # operation code 1: acknowledge of a previous message
    if opCode == 1:
        # check if userId in connectedSockets
        if userID in connectedSockets:
            # check if packageNumber in connectedSockets[userID]
            if packageNumber in connectedSockets[userID]:
                # remove packageNumber from connectedSockets[userID]
                connectedSockets[userID].remove(packageNumber)
                print(f'got an acknowledgement from {userID} for package {packageNumber}')

    # operation code 2: login attempt
    elif opCode == 2:
        # split the payload into username and password
        email, password = payload.split(b'::')
        # check if login is correct
        if db.validateStudent(email.decode(), password.decode()):
            print('Login successful')
            # create a userId between 1 and 255 and check if it is already in connectedSockets
            while True:
                userID = random.randint(1, 255)
                if userID not in connectedSockets:
                    break
            print('UserID: ', userID)
            # add userId to connectedSockets
            connectedSockets[userID] = [0]
            # create an answer in order to send the client the userId and the information that the login was successful
            answer = bytearray()
            answer.extend(b'\x00\x00\x00\x00')
            answer.extend(userID.to_bytes(1, byteorder='big'))
            answer.extend(b'\x03')
            answer.extend(b'\x00\x00\x00\x00')
            crc = zlib.crc32(answer)
            answer[6:6] = crc.to_bytes(4, byteorder='big')
            server_socket.sendto(bytes(answer), address)
            # Thread to send the messages to the client if he does not acknowledge the previous message
            Thread(target=resendMessage, args=(bytes(answer), address, userID, 0)).start()
        else:
            # create an answer in order to send the client the information that the login was unsuccessful and the reason
            print('Login failed')
            answer = bytearray()
            answer.extend(packageNumber.to_bytes(4, byteorder='big'))
            answer.extend(userID.to_bytes(1, byteorder='big'))
            answer.extend(b'\x00')
            answer.extend(b'\x00\x00\x00\x07')
            answer.extend(packageNumber.to_bytes(4, byteorder='big'))
            answer.extend(b'::')
            answer.extend(b'\x01')
            crc = zlib.crc32(answer)
            answer[6:6] = crc.to_bytes(4, byteorder='big')
            server_socket.sendto(bytes(answer), address)

    # operation code 4: get grades and classes of a specific student in a specific semester
    elif opCode == 4:
        # check if userID is in connectedSockets
        if userID in connectedSockets:
            # split the payload
            email, password, semester = payload.split(b'::')
            semester = int.from_bytes(semester, byteorder='big')
            # check if student data are valid
            if db.validateStudent(email.decode(), password.decode()):
                # get all classes and grades of the student
                rawClasses = db.getModules(db.getStudentId(email.decode()), semester)
                classes = {}
                # reformat the data in order to send it to the client
                for x in rawClasses:
                    module = db.getModule(x[1])
                    classes[module[1]] = {'ID': module[0], 'Note': x[3], 'beschreibung': module[2],
                                          'leistung': module[3], 'ects': module[4]}
                # encode the data in order to send it to the client
                classes = json.dumps(classes, separators=(',', ':')).encode('utf-8')
                print(classes)
                # create an answer in order to send the client the information about the data
                answer = bytearray()
                connectedSockets[userID].append(connectedSockets[userID][-1] + 1)
                answer.extend(connectedSockets[userID][-1].to_bytes(4, byteorder='big'))
                answer.extend(userID.to_bytes(1, byteorder='big'))
                answer.extend(b'\x05')
                answer.extend(len(classes).to_bytes(4, byteorder='big'))
                answer.extend(classes)
                crc = zlib.crc32(answer)
                answer[6:6] = crc.to_bytes(4, byteorder='big')
                server_socket.sendto(bytes(answer), address)
                # Thread to send the messages to the client if he does not acknowledge the previous message
                Thread(target=resendMessage,
                       args=(bytes(answer), address, userID, connectedSockets[userID][-1])).start()
            else:
                # create an answer in order to send the client the information that the login was unsuccessful and the reason
                print('Login failed')
                answer = bytearray()
                answer.extend(packageNumber.to_bytes(4, byteorder='big'))
                answer.extend(userID.to_bytes(1, byteorder='big'))
                answer.extend(b'\x00')
                answer.extend(b'\x00\x00\x00\x07')
                answer.extend(packageNumber.to_bytes(4, byteorder='big'))
                answer.extend(b'::')
                answer.extend(b'\x01')
                crc = zlib.crc32(answer)
                answer[6:6] = crc.to_bytes(4, byteorder='big')
                print('login failed')
                server_socket.sendto(bytes(answer), address)

        else:
            # create an answer in order to send the client the information that he has to login first
            answer = bytearray()
            answer.extend(packageNumber.to_bytes(4, byteorder='big'))
            answer.extend(userID.to_bytes(1, byteorder='big'))
            answer.extend(b'\x00')
            answer.extend(b'\x00\x00\x00\x07')
            answer.extend(packageNumber.to_bytes(4, byteorder='big'))
            answer.extend(b'::')
            answer.extend(b'\x02')
            crc = zlib.crc32(answer)
            answer[6:6] = crc.to_bytes(4, byteorder='big')
            print('UserID is not in connectedSockets')
            server_socket.sendto(bytes(answer), address)
            continue

    # operation code 6: set grades
    elif opCode == 6:
        # check if userID is in connectedSockets
        if userID in connectedSockets:
            # split the payload
            email, password, grades = payload.split(b'::')
            # decode the grades
            grades = json.loads(grades.decode('utf-8'))
            # check if student data are valid
            if db.validateStudent(email.decode(), password.decode()):
                studentId = db.getStudentId(email.decode())
                modules = db.getModulesByStudentID(studentId)
                # loop through the changed grades
                for x in grades:
                    db.editStudentModule(studentId, x, grades[x])
                print('noten gespeichert')
                # create an answer in order to send the client the information that the login was successful and grades are saved
                answer = bytearray()
                connectedSockets[userID].append(connectedSockets[userID][-1] + 1)
                answer.extend(connectedSockets[userID][-1].to_bytes(4, byteorder='big'))
                answer.extend(userID.to_bytes(1, byteorder='big'))
                answer.extend(b'\x07')
                answer.extend(b'\x00\x00\x00\x00')
                crc = zlib.crc32(answer)
                answer[6:6] = crc.to_bytes(4, byteorder='big')
                server_socket.sendto(bytes(answer), address)
                # Thread to send the messages to the client if he does not acknowledge the previous message
                Thread(target=resendMessage,
                       args=(bytes(answer), address, userID, connectedSockets[userID][-1])).start()
            else:
                print('Login failed')
                # create an answer in order to send the client the information that the login was unsuccessful and the reason
                answer = bytearray()
                answer.extend(packageNumber.to_bytes(4, byteorder='big'))
                answer.extend(userID.to_bytes(1, byteorder='big'))
                answer.extend(b'\x00')
                answer.extend(b'\x00\x00\x00\x07')
                answer.extend(packageNumber.to_bytes(4, byteorder='big'))
                answer.extend(b'::')
                answer.extend(b'\x01')
                crc = zlib.crc32(answer)
                answer[6:6] = crc.to_bytes(4, byteorder='big')
                print('login failed')
                server_socket.sendto(bytes(answer), address)

        else:
            # create an answer in order to send the client the information that he has to login first
            answer = bytearray()
            answer.extend(packageNumber.to_bytes(4, byteorder='big'))
            answer.extend(userID.to_bytes(1, byteorder='big'))
            answer.extend(b'\x00')
            answer.extend(b'\x00\x00\x00\x07')
            answer.extend(packageNumber.to_bytes(4, byteorder='big'))
            answer.extend(b'::')
            answer.extend(b'\x02')
            crc = zlib.crc32(answer)
            answer[6:6] = crc.to_bytes(4, byteorder='big')
            print('UserID is not in connectedSockets')
            server_socket.sendto(bytes(answer), address)
            continue
