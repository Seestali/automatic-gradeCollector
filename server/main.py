import random
import socket
import time
import sqlite3


# add student to student table
def addStudent(studiengang, vorname, nachname, email):
    cursor.execute("INSERT INTO students (Studiengang, Vorname, Nachname, Email) VALUES (?, ?, ?, ?)",
                   (studiengang, vorname, nachname, email))
    connection.commit()


# return student from table by vorname
def getStudent(vorname):
    cursor.execute("SELECT * FROM students WHERE Vorname = ?", (vorname,))
    return cursor.fetchone()


connection = sqlite3.connect('database.db')
cursor = connection.cursor()
connection.commit()
connection.close()
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
