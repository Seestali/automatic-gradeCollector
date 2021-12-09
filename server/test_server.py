import unittest
import hashlib
import sqlite3
from database import *

connection = sqlite3.connect('database.db')
cursor = connection.cursor()
print("SQLite Database Version is: ", cursor.fetchall())


class TestServerFunc(unittest.TestCase):

    def test_addition(self):
        self.assertEqual(10 * 10, 100)

    def test_addStudent(self):  # studiengang, vorname, nachname, email, password

        student1 = addStudent('TINF19', 'Mix', 'Müzler', 'MO@web.de', '123meins')
        self.assertEqual(student1, True)

    def test_validateStudent(self):  # email, password
        validate1 = validateStudent('MO@web.de', hashlib.sha256(('123meins').encode('utf-8')).hexdigest())
        self.assertEqual(validate1, True)

    def test_getStudentId(self):  # email
        student1 = getStudentId('MO@web.de')
        self.assertEqual(student1, 5)

    def test_getStudentData(self):  # email
        student1 = getStudentData('MO@web.de')
        self.assertEqual(student1[4], 'MO@web.de')

    def test_editStudent(self):  # studiengang, vorname, nachname, password, email
        student1 = editStudent('TINF19', 'Max', 'Müller', 'Password123', 'MO@web.de')
        self.assertEqual(student1, True)

    def test_removeStudent(self):  # id
        student1 = removeStudent(5)  ###### gibt immer true zurück
        self.assertEqual(student1, True)

    def test_getModuleId(self):  # name
        student1 = getModuleId('Analysis')  ######
        self.assertEqual(student1, 1)

    def test_getModule(self):  # id
        student1 = getModule(1)
        self.assertEqual(student1[0], 1)

    def test_getModules(self):  # studentID, semester
        modules1 = getModules(2, 1)  #########
        a = modules1.count
        self.assertEqual(a, -1)  #############

    def test_addModule(self):  # name, beschreibung, leistung, ects
        module1 = addModule('name', 'beschreibung', 'leistung', 'ects', 'semester')  ########
        self.assertEqual(module1, True)  ############

    def test_removeModule(self):  # id
        module1 = removeModule('id')  ########
        self.assertEqual(module1, True)  #########

    def test_addStudentModule(self):  # studentID, moduleID, semester
        studentmodule1 = addStudentModule('studentID', 'moduleID', 'semester')  #########
        self.assertEqual(studentmodule1, True)  ########

    def test_editStudentModule(self):  # studentID, moduleID, note
        studentmodule1 = editStudentModule('studentID', 'moduleID', 'note')  ###########
        self.assertEqual(studentmodule1, True)  #########

    def test_removeStudentModule(self):  # studentID, moduleID
        studentmodule1 = removeStudentModule('studentID', 'moduleID')  ########
        self.assertEqual(studentmodule1, True)
