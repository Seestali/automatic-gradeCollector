import sqlite3
import hashlib

connection = sqlite3.connect('database.db')
cursor = connection.cursor()


# add student to student table and check if email already exists
def addStudent(studiengang, vorname, nachname, email, password):
    cursor.execute("SELECT * FROM students WHERE Email = ?", (email,))
    student = cursor.fetchone()
    if student is None:
        cursor.execute("INSERT INTO students (Studiengang, Vorname, Nachname, Email, Passwort) VALUES (?, ?, ?, ?, ?)",
                       (studiengang, vorname, nachname, email,
                        hashlib.sha256(hashlib.sha256(password.encode('utf-8')).hexdigest().encode('utf-8')).hexdigest()))
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


# get student id from email
def getStudentId(email):
    cursor.execute("SELECT * FROM students WHERE Email = ?", (email,))
    student = cursor.fetchone()
    return student[0]


# get student data from email
def getStudentData(email):
    cursor.execute("SELECT * FROM students WHERE Email = ?", (email,))
    student = cursor.fetchone()
    return student


# edit student
def editStudent(studiengang, vorname, nachname, password, email):
    cursor.execute("UPDATE students SET Studiengang = ?, Vorname = ?, Nachname = ?, Passwort = ? WHERE Email = ?",
                   (studiengang, vorname, nachname, hashlib.sha256(password.encode('utf-8')).hexdigest(), email))
    connection.commit()
    return True


# remove student by id and all entries in student module combination
def removeStudent(id):
    cursor.execute("DELETE FROM students WHERE ID = ?", (id,))
    cursor.execute("DELETE FROM studentModul WHERE studentID = ?", (id,))
    connection.commit()
    return True


# get module id from module name
def getModuleId(name):
    cursor.execute("SELECT * FROM modules WHERE Name = ?", (name,))
    module = cursor.fetchone()
    return module[0]


# get module by id
def getModule(id):
    cursor.execute("SELECT * FROM modules WHERE ID = ?", (id,))
    module = cursor.fetchone()
    return module


# get modules of student by student id and semester
def getModules(studentID, semester):
    cursor.execute("SELECT * FROM studentModul WHERE studentID = ? AND Semester = ?", (studentID, semester))
    modules = cursor.fetchall()
    return modules

    # add module to modules table and check if module already exists


def addModule(name, beschreibung, leistung, ects):
    cursor.execute("SELECT * FROM modules WHERE Name = ?", (name,))
    module = cursor.fetchone()
    if module is None:
        cursor.execute("INSERT INTO modules (Name, Beschreibung, Prüfungsleistung, ECTS) VALUES (?, ?, ?, ?)",
                       (name, beschreibung, leistung, ects))
        connection.commit()
        return True
    else:
        return False


# remove module from modules table amd delete all entries in studentModul table
def removeModule(id):
    cursor.execute("DELETE FROM modules WHERE ID = ?", (id,))
    cursor.execute("DELETE FROM studentModul WHERE moduleID = ?", (id,))
    connection.commit()
    return True


# add student to module and check if it already exists
def addStudentModule(studentID, moduleID, semester):
    cursor.execute("SELECT * FROM studentModul WHERE studentID = ? AND moduleID = ? AND Semester = ?",
                   (studentID, moduleID, semester))
    studentModule = cursor.fetchone()
    if studentModule is None:
        cursor.execute("INSERT INTO studentModul (studentID, moduleID, Semester) VALUES (?, ?, ?)",
                       (studentID, moduleID, semester))
        connection.commit()
        return True
    else:
        return False


# edit student module combination
def editStudentModule(studentID, moduleID, note):
    cursor.execute("UPDATE studentModul SET Note = ? WHERE studentID = ? AND moduleID = ?",
                   (note, studentID, moduleID))
    connection.commit()
    return True


# remove student from module
def removeStudentModule(studentID, moduleID):
    cursor.execute("DELETE FROM studentModul WHERE studentID = ? AND moduleID = ?", (studentID, moduleID))
    connection.commit()
    return True
