import unittest
import hashlib
import sqlite3
from database import Database


# To Do: Test variablen einfügen damit werte überprüfbar sind
#        Datenbank kopieren auf Test.db und dahin connecten und danach beenden
# Test anpassen benennung und mehr Fälle


class TestServerFunc(unittest.TestCase):
    def __init__(self, methodName='runTest'):
        super(TestServerFunc, self).__init__(methodName)
        self.studentidtest = ''
        # copy database.db to test.db
        with open('database.db', 'rb') as f:
            with open('test.db', 'wb') as f2:
                f2.write(f.read())
        self.db = Database('test.db')

    def test_addStudent(self):  # studiengang, vorname, nachname, email, password
        student1 = self.db.addStudent('TINF19', 'Mix', 'Müzler', 'MI@web.de', '123meins')
        self.assertEqual(self.studentidtest, True)

    def test_validateStudent(self):  # email, password
        validate1 = self.db.validateStudent('MI@web.de', hashlib.sha256(('123meins').encode('utf-8')).hexdigest())
        self.assertEqual(validate1, True)

    def test_getStudentId(self):  # email
        student1 = self.db.getStudentId('MI@web.de')
        self.assertEqual(student1, 8)

    def test_getStudentMail(self):  # studiengang, vorname, nachname, email, password
        student1 = self.db.getStudentMail(8)
        self.assertEqual(student1, 'MI@web.de')

    def test_getStudentData(self):  # email
        student1 = self.db.getStudentData('MO@web.de')
        self.assertEqual(student1[4], 'MO@web.de')

    def test_editStudent(self):  # studiengang, vorname, nachname, password, email
        student1 = self.db.editStudent('TINF19', 'Max', 'Müller', 'Password123', 'MO@web.de')
        self.assertEqual(student1, True)

    def test_removeStudent(self):  # id
        student1 = self.db.removeStudent(5)  ###### gibt immer true zurück
        self.assertEqual(student1, True)

    def test_getTableStudents(self):  # id
        studenttable = self.db.getTableStudents()  ###### stimmt der erste eintrag vom typ her (anzahl elemente und ihre typen)
        self.assertEqual(studenttable, (self.db.cursor.execute("SELECT * FROM [students]")).fetchall())

    def test_getModuleId(self):  # name
        student1 = self.db.getModuleId('Analysis')  ######
        self.assertEqual(student1, 1)

    def test_getModule(self):  # id
        student1 = self.db.getModule(1)
        self.assertEqual(student1[0], 1)

    def test_getModules(self):  # studentID, semester
        modules1 = self.db.getModules(2, 1)  #########
        a = modules1.count
        self.assertEqual(a, 0)  ############# replace?

    def test_getModulesByStudentID(self):  # name, beschreibung, leistung, ects
        module1 = self.db.getModulesByStudentID('name', 'beschreibung', 'leistung', 'ects', 'semester')  ########
        self.assertEqual(module1, True)  ############

    def test_addModule(self):  # name, beschreibung, leistung, ects
        module1 = self.db.addModule('11', '11', '11', 'e1')  ########
        self.assertEqual(module1, True)  ############

    def test_removeModule(self):  # id
        module1 = self.db.removeModule('1')  ########
        self.assertEqual(module1, True)  #########

    def test_editModule(self):  # id
        module1 = self.db.editModule('11', '11', '11', '11', '34')  ########
        self.assertEqual(module1, True)  #########

    def test_addStudentModule(self):  # studentID, moduleID, semester
        studentmodule1 = self.db.addStudentModule('studentID', 'moduleID', 'semester')  #########
        self.assertEqual(studentmodule1, True)  ########

    def test_editStudentModule(self):  # studentID, moduleID, note
        studentmodule1 = self.db.editStudentModule('studentID', 'moduleID', 'note')  ###########
        self.assertEqual(studentmodule1, True)  #########

    def test_removeStudentModule(self):  # studentID, moduleID
        studentmodule1 = self.db.removeStudentModule('studentID', 'moduleID')  ########
        self.assertEqual(studentmodule1, True)
