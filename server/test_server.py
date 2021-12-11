import unittest
import hashlib
import sqlite3
from database import *



# To Do: Test variablen einfügen damit werte überprüfbar sind
#        Datenbank kopieren auf Test.db und dahin connecten und danach beenden
        #Test anpassen benennung und mehr Fälle

class TestServerFunc(unittest.TestCase):
    def __init__(self):
        self.studentidtest=''
    #self.studentidtest
    def test_addStudent(self):  # studiengang, vorname, nachname, email, password
        student1 = addStudent('TINF19', 'Mix', 'Müzler', 'MI@web.de', '123meins')
        self.assertEqual(self.studentidtest, True)
        
    def test_validateStudent(self):  # email, password
        validate1 = validateStudent('MI@web.de', hashlib.sha256(('123meins').encode('utf-8')).hexdigest())
        self.assertEqual(validate1, True)

    def test_getStudentId(self):  # email
        student1 = getStudentId('MI@web.de')
        self.assertEqual(student1, 8)

    def test_getStudentMail(self):  # studiengang, vorname, nachname, email, password
        student1 = getStudentMail(8)
        self.assertEqual(student1, 'MI@web.de')

    def test_getStudentData(self):  # email
        student1 = getStudentData('MO@web.de')
        self.assertEqual(student1[4], 'MO@web.de')

    def test_editStudent(self):  # studiengang, vorname, nachname, password, email
        student1 = editStudent('TINF19', 'Max', 'Müller', 'Password123', 'MO@web.de')
        self.assertEqual(student1, True)

    def test_removeStudent(self):  # id
        student1 = removeStudent(5)  ###### gibt immer true zurück
        self.assertEqual(student1, True)

    def test_getTableStudents(self):  # id
        studenttable = getTableStudents()  ###### stimmt der erste eintrag vom typ her (anzahl elemente und ihre typen)
        self.assertEqual(studenttable, (cursor.execute("SELECT * FROM [students]")).fetchall())

    def test_getModuleId(self):  # name
        student1 = getModuleId('Analysis')  ######
        self.assertEqual(student1, 1)

    def test_getModule(self):  # id
        student1 = getModule(1)
        self.assertEqual(student1[0], 1)

    def test_getModules(self):  # studentID, semester
        modules1 = getModules(2, 1)  #########
        a = modules1.count
        self.assertEqual(a, 0)  ############# replace?

    def test_getModulesByStudentID(self):  # name, beschreibung, leistung, ects
        module1 = getModulesByStudentID('name', 'beschreibung', 'leistung', 'ects', 'semester')  ########
        self.assertEqual(module1, True)  ############

    def test_addModule(self):  # name, beschreibung, leistung, ects
        module1 = addModule('11', '11', '11', 'e1')  ########
        self.assertEqual(module1, True)  ############

    def test_removeModule(self):  # id
        module1 = removeModule('1')  ########
        self.assertEqual(module1, True)  #########

    def test_editModule(self):  # id
        module1 = editModule('11', '11', '11', '11', '34')  ########
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
