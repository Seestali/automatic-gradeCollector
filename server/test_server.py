from types import NoneType
import unittest
import hashlib
from database import Database


# To Do: Test variablen einfügen damit werte überprüfbar sind
#        Datenbank kopieren auf Test.db und dahin connecten und danach beenden
# Test anpassen benennung und mehr Fälle


class TestServerFunc(unittest.TestCase):
    '''
    This Class tests the functions of the Database Class.
    '''

    def __init__(self, methodName='runTest'):
        '''
        This function initializes the enhanced unittest and some variables.
        It also duplicates the database for testing.
        '''
        super(TestServerFunc, self).__init__(methodName)

        # copy database.db to test.db
        with open('database.db', 'rb') as f:
            with open('test.db', 'wb') as f2:
                f2.write(f.read())
        self.db = Database('test.db')
        self.studentMailTest = 'MM@web.de'
        self.db.addStudent('TINF19', 'Max', 'Müller',
                           self.studentMailTest, '123meins')
        self.studentIdTest = self.db.getStudentId(self.studentMailTest)
        self.modulNameTest = 'Testmodul'
        self.db.addModule((self.modulNameTest), 'Test', 'keine', 0)
        self.modulIdTest = self.db.getModuleId(self.modulNameTest)

    # studiengang, vorname, nachname, email, password
    def test_addStudent_StringInput_ReturnsSame(self):
        self.assertTrue(self.db.addStudent('TINF19', 'Max',
                        'Müller', 'self.studentMailTest', '123meins'))

    # studiengang, vorname, nachname, email, password
    def test_addStudent_ExistingMailInput_DenyInput(self):
        self.assertFalse(self.db.addStudent(
            'TINF19', 'Max', 'Müller', self.studentMailTest, '123meins'))

    def test_validateStudent_StringInput_ReturnsTrue(self):  # email, password
        self.assertTrue(self.db.validateStudent(
            self.studentMailTest, hashlib.sha256(('123meins').encode('utf-8')).hexdigest()))

    # email, password
    def test_validateStudent_WrongtringInput_ReturnsFalse(self):
        self.assertFalse(self.db.validateStudent(
            self.studentMailTest, hashlib.sha256(('12meins').encode('utf-8')).hexdigest()))

    # email, password
    def test_validateStudent_EmptyStringInput_ReturnsFalse(self):
        self.assertFalse(self.db.validateStudent(
            self.studentMailTest, hashlib.sha256(('').encode('utf-8')).hexdigest()))

    def test_getStudentId_StringInput_ReturnsTypeIntIsSame(self):  # email
        self.assertEqual(
            type((self.db.getStudentId(self.studentMailTest))), int)

    def test_getStudentId_WrongStringInput_ReturnsNone(self):  # email
        self.assertFalse(self.db.getStudentId('wrong@Mail.com'))

    # studiengang, vorname, nachname, email, password
    def test_getStudentMail_IntInput_ReturnsSame(self):
        self.assertEqual(self.db.getStudentMail(
            self.studentIdTest), self.studentMailTest)

    # studiengang, vorname, nachname, email, password
    def test_getStudentMail_WrongIntInput_ReturnsSame(self):
        self.assertEqual(self.db.getStudentMail(99), False)

    # studiengang, vorname, nachname, email, password
    def test_getStudentMail_WrongStringInput_ReturnsSame(self):
        self.assertEqual(self.db.getStudentMail('self.studentIdTest'), False)

    def test_getStudentData_StringInput_TypesEqual(self):  # email
        student1 = self.db.getStudentData(self.studentMailTest)
        self.assertEqual(type(student1[0]), int)
        self.assertEqual(type(student1[1]), str)
        self.assertEqual(type(student1[2]), str)
        self.assertEqual(type(student1[3]), str)
        self.assertEqual(type(student1[4]), str)
        self.assertEqual(type(student1[5]), str)

    def test_getStudentData_WrongStringInput_ReturnsFalse(self):  # email
        self.assertEqual(self.db.getStudentData('self.studentIdTest'), False)

    # studiengang, vorname, nachname, password, email
    def test_editStudent_StringInput_ReturnsTrue(self):
        self.assertTrue(self.db.editStudent('TINF19', 'Moritz',
                        'Müller', 'Password123', self.studentMailTest), True)

    # studiengang, vorname, nachname, password, email
    def test_editStudent_WrongStringInput_ReturnsFalse(self):
        self.assertFalse(self.db.editStudent(
            'TINF19', 'Moritz', 'Müller', 'Password123', 'self.studentMailTest'), False)

    def test_getTableStudents_(self):  # id
        # stimmt der erste eintrag vom typ her (anzahl elemente und ihre typen)
        studenttable = self.db.getTableStudents()
        self.assertEqual(type(studenttable[0][0]), int)
        self.assertEqual(type(studenttable[0][1]), str)
        self.assertEqual(type(studenttable[0][2]), str)
        self.assertEqual(type(studenttable[0][3]), str)
        self.assertEqual(type(studenttable[0][4]), str)
        self.assertEqual(type(studenttable[0][5]), str)

    # name, beschreibung, leistung, ects
    def test_addModule_InputString_ReturnsTrue(self):
        self.assertTrue(self.db.addModule('Testmodul2', 'Test2', 'keine', '0'))

    # name, beschreibung, leistung, ects
    def test_addModule_DuplicatedInputString_ReturnsFalse(self):
        self.assertFalse(self.db.addModule('Testmodul', 'Test', 'keine', '0'))

    def test_getModuleId_StringInput_TypeEqualInt(self):  # name
        self.assertEqual(type(self.db.getModuleId(self.modulNameTest)), int)

    def test_getModuleId_WrongStringInput_TypeEqualInt_False(self):  # name
        self.assertFalse(self.db.getModuleId('self.modulNameTest'))

    def test_getModule_StringInput_TypesArgumentsEqual(self):  # name
        modul = self.db.getModule(self.modulIdTest)
        self.assertEqual(type(modul[0]), int)
        self.assertEqual(type(modul[1]), str)
        self.assertEqual(type(modul[2]), str)
        self.assertEqual(type(modul[3]), str)
        self.assertEqual(type(modul[4]), float)

    def test_getModule_WrongStringInput_TypeEqual_False(self):  # name
        self.assertFalse(self.db.getModuleId('self.modulNameTest'))

    def test_getModule(self):  # id
        student1 = self.db.getModule(1)
        self.assertEqual(student1[0], 1)

    def test_getModules(self):  # studentID, semester
        modules1 = self.db.getModules(2, 1)
        a = modules1.count
        self.assertEqual(a, 0)  # replace?

    def test_editModule(self):  # id
        module1 = self.db.editModule('11', '11', '11', '11', '34')
        self.assertEqual(module1, True)

    def test_addStudentModule(self):  # studentID, moduleID, semester
        studentmodule1 = self.db.addStudentModule(
            'studentID', 'moduleID', 'semester')
        self.assertEqual(studentmodule1, True)

    def test_getModulesByStudentID(self):  # name, beschreibung, leistung, ects
        module1 = self.db.getModulesByStudentID('Studentid')
        self.assertEqual(module1, True)

    def test_editStudentModule(self):  # studentID, moduleID, note
        studentmodule1 = self.db.editStudentModule(
            'studentID', 'moduleID', 'note')
        self.assertEqual(studentmodule1, True)

    def test_removeStudent(self):  # id
        student1 = self.db.removeStudent(5)  # gibt immer true zurück
        self.assertEqual(student1, True)

    def test_removeModule(self):  # id
        module1 = self.db.removeModule('1')
        self.assertEqual(module1, True)

    def test_removeStudentModule(self):  # studentID, moduleID
        studentmodule1 = self.db.removeStudentModule('studentID', 'moduleID')
        self.assertEqual(studentmodule1, True)
