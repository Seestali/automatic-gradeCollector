import random
import socket
import time
import sqlite3
import hashlib


# add student to student table and check if email already exists
def addStudent(studiengang, vorname, nachname, email, password):
    cursor.execute("SELECT * FROM students WHERE Email = ?", (email,))
    student = cursor.fetchone()
    if student is None:
        cursor.execute("INSERT INTO students (Studiengang, Vorname, Nachname, Email, Passwort) VALUES (?, ?, ?, ?, ?)",
                       (studiengang, vorname, nachname, email, hashlib.sha256(password.encode('utf-8')).hexdigest()))
        connection.commit()
        return True
    else:
        return False


def validateStudent(email, password):
    cursor.execute("SELECT * FROM students WHERE Email = ?", (email,))
    student = cursor.fetchone()
    if student is None:
        return False
    else:
        if hashlib.sha256(password.encode('utf-8')).hexdigest() == student[5]:
            return True
        else:
            return False


connection = sqlite3.connect('database.db')
cursor = connection.cursor()
connection.commit()
bufferSize = 1024
port = 42069

# addStudent("Informatik", "Max", "Mustermann", "test@test.de", "test")
print(validateStudent("test@test.de", "test"))

server_socket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
server_socket.bind(('', port))
print('The server is ready to receive')

while True:
    message, address = server_socket.recvfrom(bufferSize)
    message = message.upper()
    # returns message and address to client
    server_socket.sendto(message, address)
