import sqlite3
import hashlib


class Database:
    def __init__(self, file):
        self.connection = sqlite3.connect(file)
        self.cursor = self.connection.cursor()

    # add student to student table and check if email already exists
    def addStudent(self, studiengang, vorname, nachname, email, password):
        self.cursor.execute("SELECT * FROM students WHERE Email = ?", (email,))
        student = self.cursor.fetchone()
        if student is None:
            self.cursor.execute(
                "INSERT INTO students (Studiengang, Vorname, Nachname, Email, Passwort) VALUES (?, ?, ?, ?, ?)",
                (studiengang, vorname, nachname, email,
                 hashlib.sha256(
                     hashlib.sha256(password.encode('utf-8')).hexdigest().encode('utf-8')).hexdigest()))
            self.connection.commit()
            return True
        else:
            return False

    def validateStudent(self, email, password):
        self.cursor.execute("SELECT * FROM students WHERE Email = ?", (email,))
        student = self.cursor.fetchone()
        if student is None:
            return False
        else:
            if hashlib.sha256(password.encode('utf-8')).hexdigest() == student[5]:
                return True
            else:
                return False

    # get student id from email
    def getStudentId(self, email):
        self.cursor.execute("SELECT * FROM students WHERE Email = ?", (email,))
        student = self.cursor.fetchone()
        return student[0]

    # get student email from id
    def getStudentMail(self, id):
        self.cursor.execute("SELECT * FROM students WHERE ID = ?", (id,))
        student = self.cursor.fetchone()
        return student[4]

    # get student data from email
    def getStudentData(self, email):
        self.cursor.execute("SELECT * FROM students WHERE Email = ?", (email,))
        student = self.cursor.fetchone()
        return student

    # edit student and check if he exists
    def editStudent(self, studiengang, vorname, nachname, password, email):
        self.cursor.execute("SELECT * FROM students WHERE Email = ?", (email,))
        student = self.cursor.fetchone()
        if student is None:
            return False
        else:
            self.cursor.execute(
                "UPDATE students SET Studiengang = ?, Vorname = ?, Nachname = ?, Passwort = ? WHERE Email = ?",
                (studiengang, vorname, nachname, hashlib.sha256(password.encode('utf-8')).hexdigest(), email))
            self.connection.commit()
            return True

    # remove student by id and all entries in student module combination but check if student exists
    def removeStudent(self, id):
        self.cursor.execute("SELECT * FROM students WHERE ID = ?", (id,))
        student = self.cursor.fetchone()
        if student is None:
            return False
        else:
            self.cursor.execute("DELETE FROM students WHERE ID = ?", (id,))
            self.cursor.execute("DELETE FROM studentModul WHERE studentID = ?", (id,))
            self.connection.commit()
            return True

    # show table students
    def getTableStudents(self):
        self.cursor.execute("SELECT * FROM [students]")
        tableStudents = self.cursor.fetchall()
        return tableStudents

    # get module id from module name
    def getModuleId(self, name):
        self.cursor.execute("SELECT * FROM modules WHERE Name = ?", (name,))
        module = self.cursor.fetchone()
        return module[0]

    # get module by id
    def getModule(self, id):
        self.cursor.execute("SELECT * FROM modules WHERE ID = ?", (id,))
        module = self.cursor.fetchone()
        return module

    # get modules of student by student id and semester
    def getModules(self, studentID, semester):
        self.cursor.execute("SELECT * FROM studentModul WHERE studentID = ? AND Semester = ?", (studentID, semester))
        modules = self.cursor.fetchall()
        return modules

    # get modules of student by student id
    def getModulesByStudentID(self, studentID):
        self.cursor.execute("SELECT * FROM studentModul WHERE studentID = ?", (studentID,))
        modules = self.cursor.fetchall()
        return modules

    # add module to modules table and check if module already exists
    def addModule(self, name, beschreibung, leistung, ects):
        self.cursor.execute("SELECT * FROM modules WHERE Name = ?", (name,))
        module = self.cursor.fetchone()
        if module is None:
            self.cursor.execute("INSERT INTO modules (Name, Beschreibung, Prüfungsleistung, ECTS) VALUES (?, ?, ?, ?)",
                                (name, beschreibung, leistung, ects))
            self.connection.commit()
            return True
        else:
            return False

    # remove module from modules table amd delete all entries in studentModul table but check if module exists
    def removeModule(self, id):
        self.cursor.execute("SELECT * FROM modules WHERE ID = ?", (id,))
        module = self.cursor.fetchone()
        if module is None:
            return False
        else:
            self.cursor.execute("DELETE FROM modules WHERE ID = ?", (id,))
            self.cursor.execute("DELETE FROM studentModul WHERE moduleID = ?", (id,))
            self.connection.commit()
            return True

    # edit module and check if module exists
    def editModule(self, name, beschreibung, leistung, ects, id):
        self.cursor.execute("SELECT * FROM modules WHERE ID = ?", (id,))
        module = self.cursor.fetchone()
        if module is None:
            return False
        else:
            self.cursor.execute(
                "UPDATE modules SET Name = ?, Beschreibung = ?, Prüfungsleistung = ?, ECTS = ? WHERE ID = ?",
                (name, beschreibung, leistung, ects, id))
            self.connection.commit()
            return True

    # show table modules
    def getTableModules(self):
        self.cursor.execute("SELECT * FROM [modules]")
        tableModules = self.cursor.fetchall()
        return tableModules

    # add student to module and check if it already exists
    def addStudentModule(self, studentID, moduleID, semester):
        self.cursor.execute("SELECT * FROM studentModul WHERE studentID = ? AND moduleID = ? AND Semester = ?",
                            (studentID, moduleID, semester))
        studentModule = self.cursor.fetchone()
        if studentModule is None:
            self.cursor.execute("INSERT INTO studentModul (studentID, moduleID, Semester) VALUES (?, ?, ?)",
                                (studentID, moduleID, semester))
            self.connection.commit()
            return True
        else:
            return False

    # edit student module combination and check if it already exists
    def editStudentModule(self, studentID, moduleID, note):
        self.cursor.execute("SELECT * FROM studentModul WHERE studentID = ? AND moduleID = ?", (studentID, moduleID))
        studentModule = self.cursor.fetchone()
        if studentModule is None:
            return False
        else:
            self.cursor.execute("UPDATE studentModul SET Note = ? WHERE studentID = ? AND moduleID = ?",
                                (note, studentID, moduleID))
            self.connection.commit()
            return True

    # remove student from module and check if it already exists
    def removeStudentModule(self, studentID, moduleID):
        self.cursor.execute("SELECT * FROM studentModul WHERE studentID = ? AND moduleID = ?", (studentID, moduleID))
        studentModule = self.cursor.fetchone()
        if studentModule is None:
            return False
        else:
            self.cursor.execute("DELETE FROM studentModul WHERE studentID = ? AND moduleID = ?", (studentID, moduleID))
            self.connection.commit()
            return True
