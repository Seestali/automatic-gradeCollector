import sqlite3
import hashlib


class Database:
    '''
    This Class creates a connection to the database and provides methods to add, edit and remove modules, students and grades.
    '''

    def __init__(self, file):
        '''
        This method initializes the database connection.
        :param file: the location of the database file
        '''
        self.connection = sqlite3.connect(file)
        self.cursor = self.connection.cursor()

    # add student to student table and check if email already exists
    def addStudent(self, studiengang, vorname, nachname, email, password):
        '''
        This method adds a student to the database.
        :param studiengang: the student's study program
        :param vorname: the student's first name
        :param nachname: the student's last name
        :param email: the student's email
        :param password: the student's password
        :return: True if the student was added, False if the student already exists
        '''
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
        '''
        This method validates a student.
        :param email: the student's email
        :param password: the student's password
        :return: True if the student exists and the password is correct, False if the student does not exist or the password is wrong
        '''
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
        '''
        This method gets the student id from the email.
        :param email: the student's email
        :return: the student id or False if the student does not exist
        '''
        self.cursor.execute("SELECT * FROM students WHERE Email = ?", (email,))
        student = self.cursor.fetchone()
        if student is None:
            return False
        else:
            return student[0]

    # get student email from id
    def getStudentMail(self, id):
        '''
        This method gets the student email from the id.
        :param id: the student's id
        :return: the student email or False if the student does not exist
        '''
        self.cursor.execute("SELECT * FROM students WHERE ID = ?", (id,))
        student = self.cursor.fetchone()
        if student is None:
            return False
        else:
            return student[4]

    # get student data from email
    def getStudentData(self, email):
        '''
        This method gets the student data from the email.
        :param email: the student's email
        :return: the student data or False if the student does not exist
        '''
        self.cursor.execute("SELECT * FROM students WHERE Email = ?", (email,))
        student = self.cursor.fetchone()
        if student is None:
            return False
        else:
            return student

    # edit student and check if he exists
    def editStudent(self, studiengang, vorname, nachname, password, email):
        '''
        This method edits a student.
        :param studiengang: the new student's study program
        :param vorname: the new student's first name
        :param nachname: the new student's last name
        :param password: the new student's password
        :param email: the new student's email
        :return: True if the student was edited, False if the student does not exist
        '''
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
        '''
        This method removes a student.
        :param id: the student's id
        :return: True if the student was removed, False if the student does not exist
        '''
        self.cursor.execute("SELECT * FROM students WHERE ID = ?", (id,))
        student = self.cursor.fetchone()
        if student is None:
            return False
        else:
            self.cursor.execute("DELETE FROM students WHERE ID = ?", (id,))
            self.cursor.execute(
                "DELETE FROM studentModul WHERE studentID = ?", (id,))
            self.connection.commit()
            return True

    # show table students
    def getTableStudents(self):
        '''
        This method shows the students table.
        :return: the students table
        '''
        self.cursor.execute("SELECT * FROM [students]")
        tableStudents = self.cursor.fetchall()
        return tableStudents

    # get module id from module name
    def getModuleId(self, name):
        '''
        This method gets the module id from the module name.
        :param name: the module's name
        :return: the module id or False if the module does not exist
        '''
        self.cursor.execute("SELECT * FROM modules WHERE Name = ?", (name,))
        module = self.cursor.fetchone()
        if module is None:
            return False
        else:
            return module[0]

    # get module by id
    def getModule(self, id):
        '''
        This method gets the module by the id.
        :param id: the module's id
        :return: the module or False if the module does not exist
        '''
        self.cursor.execute("SELECT * FROM modules WHERE ID = ?", (id,))
        module = self.cursor.fetchone()
        if module is None:
            return False
        else:
            return module

    # get modules of student by student id and semester
    def getModules(self, studentID, semester):
        '''
        This method gets all modules of the student by the student id and semester.
        :param studentID: the student's id
        :param semester: the semester
        :return: the modules of the student
        '''
        self.cursor.execute(
            "SELECT * FROM studentModul WHERE studentID = ? AND Semester = ?", (studentID, semester))
        modules = self.cursor.fetchall()
        return modules

    # get modules of student by student id
    def getModulesByStudentID(self, studentID):
        '''
        This method gets all modules of the student by the student id.  The semester is not needed.
        :param studentID: the student's id
        :return: all modules of the student
        '''
        self.cursor.execute(
            "SELECT * FROM studentModul WHERE studentID = ?", (studentID,))
        modules = self.cursor.fetchall()
        return modules

    # add module to modules table and check if module already exists
    def addModule(self, name, beschreibung, leistung, ects):
        '''
        This method adds a module.
        :param name: the module's name
        :param beschreibung: the module's description
        :param leistung: the type of exam for the module
        :param ects: the module's ects
        :return: True if the module was added, False if the module already exists
        '''
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
        ''''
        This method removes a module.
        :param id: the module's id
        :return: True if the module was removed, False if the module does not exist
        '''
        self.cursor.execute("SELECT * FROM modules WHERE ID = ?", (id,))
        module = self.cursor.fetchone()
        if module is None:
            return False
        else:
            self.cursor.execute("DELETE FROM modules WHERE ID = ?", (id,))
            self.cursor.execute(
                "DELETE FROM studentModul WHERE moduleID = ?", (id,))
            self.connection.commit()
            return True

    # edit module and check if module exists
    def editModule(self, name, beschreibung, leistung, ects, id):
        '''
        This method edits a module.
        :param name: the new module's name
        :param beschreibung: the new module's description
        :param leistung: the new module's type of exam
        :param ects: the new module's ects
        :param id: the module's id
        :return: True if the module was edited, False if the module does not exist
        '''
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
        ''''
        This method shows the modules table.
        :return: the modules table
        '''
        self.cursor.execute("SELECT * FROM [modules]")
        tableModules = self.cursor.fetchall()
        return tableModules

    # add student to module and check if it already exists
    def addStudentModule(self, studentID, moduleID, semester):
        '''
        This method adds a student to a module.
        :param studentID: the student's id
        :param moduleID: the module's id
        :param semester: the semester
        :return: True if the student was added, False if the student was already in the module in the same semester
        '''
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
        '''
        This method edits a student module combination.
        :param studentID: the student's id
        :param moduleID: the module's id
        :param note: the student's grade
        :return: True if the student module combination was edited, False if the student module combination does not exist
        '''
        self.cursor.execute(
            "SELECT * FROM studentModul WHERE studentID = ? AND moduleID = ?", (studentID, moduleID))
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
        '''
        This method removes a student from a module.
        :param studentID: the student's id
        :param moduleID: the module's id
        :return: True if the student was removed, False if the student module combination does not exist
        '''
        self.cursor.execute(
            "SELECT * FROM studentModul WHERE studentID = ? AND moduleID = ?", (studentID, moduleID))
        studentModule = self.cursor.fetchone()
        if studentModule is None:
            return False
        else:
            self.cursor.execute(
                "DELETE FROM studentModul WHERE studentID = ? AND moduleID = ?", (studentID, moduleID))
            self.connection.commit()
            return True
