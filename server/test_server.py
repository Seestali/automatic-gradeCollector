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
        self.semesterTest = '1'
        self.db.addStudentModule(self.studentIdTest,self.modulIdTest,self.semesterTest)
        
    '''
    This test passes if the addStudent function is executed successfully.
    '''
    def test_addStudent_StringInput_ReturnsTrue(self):
        self.assertTrue(self.db.addStudent('TINF19', 'Max',
                        'Müller', 'self.studentMailTest', '123meins'))

    '''
    This test passes if the addStudent function aborts execution on duplicates.
    '''
    def test_addStudent_ExistingMailInput_DenyInput(self):
        self.assertFalse(self.db.addStudent(
            'TINF19', 'Max', 'Müller', self.studentMailTest, '123meins'))
    
    '''
    This test passes if the validateStudent function is executed successfully.
    '''
    def test_validateStudent_StringInput_ReturnsTrue(self):  # email, password
        self.assertTrue(self.db.validateStudent(
            self.studentMailTest, hashlib.sha256(('123meins').encode('utf-8')).hexdigest()))

    '''
    This test passes if the validateStudent function aborts execution on wrong input.
    '''
    def test_validateStudent_WrongStringInput_ReturnsFalse(self):
        self.assertFalse(self.db.validateStudent(
            self.studentMailTest, hashlib.sha256(('12meins').encode('utf-8')).hexdigest()))

    '''
    This test passes if the validateStudent function aborts execution on empty input.
    '''
    def test_validateStudent_EmptyStringInput_ReturnsFalse(self):
        self.assertFalse(self.db.validateStudent(
            self.studentMailTest, hashlib.sha256(('').encode('utf-8')).hexdigest()))

    '''
    This test passes if the getStudentId function returns int type.
    '''
    def test_getStudentId_StringInput_ReturnsTypeIntIsSame(self):  
        self.assertEqual(
            type((self.db.getStudentId(self.studentMailTest))), int)

    '''
    This test passes if the getStudentId function aborts execution on wrong input.
    '''
    def test_getStudentId_WrongStringInput_ReturnsNone(self):  
        self.assertFalse(self.db.getStudentId('wrong@Mail.com'))

    '''
    This test passes if the getStudentMail function returns same mail as expected.
    '''
    def test_getStudentMail_IntInput_ReturnsSame(self):
        self.assertEqual(self.db.getStudentMail(
            self.studentIdTest), self.studentMailTest)

    '''
    This test passes if the getStudentMail function returns false on not existing id.
    '''
    def test_getStudentMail_WrongIntInput_ReturnsFalse(self):
        self.assertEqual(self.db.getStudentMail(99), False)

    '''
    This test passes if the getStudentMail function returns false on wrong input.
    '''
    def test_getStudentMail_WrongStringInput_ReturnsFalse(self):
        self.assertEqual(self.db.getStudentMail('self.studentIdTest'), False)

    '''
    This test passes if the getStudentData function returns the correct types.
    '''
    def test_getStudentData_StringInput_TypesEqual(self):  
        student1 = self.db.getStudentData(self.studentMailTest)
        self.assertEqual(type(student1[0]), int)
        self.assertEqual(type(student1[1]), str)
        self.assertEqual(type(student1[2]), str)
        self.assertEqual(type(student1[3]), str)
        self.assertEqual(type(student1[4]), str)
        self.assertEqual(type(student1[5]), str)

    '''
    This test passes if the getStudentData function returns false on wrong input.
    '''
    def test_getStudentData_WrongStringInput_ReturnsFalse(self):  
        self.assertEqual(self.db.getStudentData('self.studentIdTest'), False)

    '''
    This test passes if the editStudent function returns true on correct input.
    '''
    def test_editStudent_StringInput_ReturnsTrue(self):
        self.assertTrue(self.db.editStudent('TINF19', 'Moritz',
                        'Müller', 'Password123', self.studentMailTest), True)

    '''
    This test passes if the editStudent function returns false on wrong input.
    '''
    def test_editStudent_WrongStringInput_ReturnsFalse(self):
        self.assertFalse(self.db.editStudent(
            'TINF19', 'Moritz', 'Müller', 'Password123', 'self.studentMailTest'), False)

    '''
    This test passes if the getTableStudents function returns the correct types.
    '''
    def test_getTableStudents_(self):  
        studenttable = self.db.getTableStudents()
        self.assertEqual(type(studenttable[0][0]), int)
        self.assertEqual(type(studenttable[0][1]), str)
        self.assertEqual(type(studenttable[0][2]), str)
        self.assertEqual(type(studenttable[0][3]), str)
        self.assertEqual(type(studenttable[0][4]), str)
        self.assertEqual(type(studenttable[0][5]), str)

    '''
    This test passes if the addModule function returns true on correct input.
    '''
    def test_addModule_InputString_ReturnsTrue(self):
        self.assertTrue(self.db.addModule('Testmodul2', 'Test2', 'keine', '0'))

    '''
    This test passes if the addModule function returns false on input of existing module.
    '''
    def test_addModule_DuplicatedInputString_ReturnsFalse(self):
        self.assertFalse(self.db.addModule(self.modulNameTest, 'Test', 'keine', '0'))

    '''
    This test passes if the getModuleId function returns the correct type.
    '''
    def test_getModuleId_StringInput_TypeEqualInt(self):  
        self.assertEqual(type(self.db.getModuleId(self.modulNameTest)), int)

    '''
    This test passes if the getModuleId function returns false on wrong input.
    '''
    def test_getModuleId_WrongStringInput_TypeEqualInt_False(self):  
        self.assertFalse(self.db.getModuleId('self.modulNameTest'))

    '''
    This test passes if the getModule function returns the correct types.
    '''
    def test_getModule_StringInput_TypesArgumentsEqual(self):  
        modul = self.db.getModule(self.modulIdTest)
        self.assertEqual(type(modul[0]), int)
        self.assertEqual(type(modul[1]), str)
        self.assertEqual(type(modul[2]), str)
        self.assertEqual(type(modul[3]), str)
        self.assertEqual(type(modul[4]), float)

    '''
    This test passes if the getModules function returns the correct types.
    '''
    def test_getModules_IntInput_TypeArgumentsEqual(self):         
        modules = self.db.getModules(self.studentIdTest, self.semesterTest)  
        self.assertEqual(type(modules[0][0]), int)
        self.assertEqual(type(modules[0][1]), int)
        self.assertEqual(type(modules[0][2]), int)

    '''
    This test passes if the getModules function returns false on wrong input.
    '''
    def test_getModules_WrongIntInput_ReturnsFalse(self):          
        self.assertFalse(self.db.getModules('1', self.semesterTest)) 
            
    '''
    This test passes if the editModule function returns true on correct input.
    '''
    def test_editModule_StringInput_ReturnsTrue(self): 
        self.assertTrue(self.db.editModule( self.modulNameTest, 'test', 'Keine', 10, self.modulIdTest)) 
    
    '''
    This test passes if the editModule function returns false on wrong input.
    '''
    def test_editModule_WrongStringInput_ReturnsFalse(self):  
        self.assertFalse(self.db.editModule( self.modulNameTest, 'test', 'Keine', 10, self.modulIdTest+1)) 

    '''
    This test passes if the addStudentModule function returns true on correct input.
    '''
    def test_addStudentModule_StringInput_ReturnsTrue(self):  
        self.assertTrue(self.db.addStudentModule(self.studentIdTest, self.modulIdTest, '3')) 
    
    '''
    This test passes if the addStudentModule function returns false on input of already existing element.
    '''    
    def test_addStudentModule_DuplicatedStringInput_ReturnsFalse(self):  
        self.db.addStudentModule(self.studentIdTest, self.modulIdTest, '3')
        self.assertFalse(self.db.addStudentModule(self.studentIdTest, self.modulIdTest, '3'))
                     
    '''
    This test passes if the getModulesByStudentID function returns true on correct input.
    '''
    def test_getModulesByStudentID_IntInput_ReturnsTrue(self):  
        self.assertTrue(self.db.getModulesByStudentID(self.studentIdTest))  
    
    '''
    This test passes if the getModulesByStudentID function returns false on wrong input.
    '''    
    def test_getModulesByStudentID_WrongInput_ReturnsFalse(self):  
        self.assertFalse(self.db.getModulesByStudentID('self.studentIdTest')) 
        
    '''
    This test passes if the editStudentModule function returns true on correct input.
    '''
    def test_editStudentModule_StringInput_ReturnsTrue(self): 
        self.assertTrue(self.db.editStudentModule(self.studentIdTest, self.modulIdTest, 1.1)) 
    
    '''
    This test passes if the editStudentModule function returns false on wrong input.
    '''   
    def test_editStudentModule_WrongStringInput_ReturnsFalse(self):  
        self.assertFalse(self.db.editStudentModule('self.studentIdTest', self.modulIdTest, 1.1)) 

    '''
    This test passes if the removeStudent function returns true on correct input.
    '''    
    def test_removeStudent_StringInput_ReturnsTrue(self):  
        self.assertTrue(self.db.removeStudent(self.studentIdTest)) 

    '''
    This test passes if the removeStudent function returns false on wrong input.
    '''
    def test_removeStudent_WrongStringInput_ReturnsFalse(self):  
        self.db.removeStudent(self.studentIdTest)
        self.assertFalse(self.db.removeStudent(self.studentIdTest)) 

    '''
    This test passes if the removeModule function returns true on correct input.
    '''
    def test_removeModule_StringInput_ReturnsTrue(self):  
        self.assertTrue(self.db.removeModule(self.modulIdTest))  

    '''
    This test passes if the removeModule function returns false on wrong input.
    '''
    def test_removeModule_WrongStringInput_ReturnsFalse(self):  
        self.db.removeModule(self.modulIdTest)
        self.assertFalse(self.db.removeModule(self.modulIdTest)) 
        
    '''
    This test passes if the removeStudentModule function returns true on correct input.
    '''
    def test_z_removeStudentModule_StringInput_ReturnsTrue(self):  
        self.assertTrue(self.db.removeStudentModule(self.studentIdTest, self.modulIdTest)) 

    '''
    This test passes if the removeStudentModule function returns false on wrong input.
    '''    
    def test_removeStudentModule_WrongStringInput_ReturnsFalse(self):  
        self.db.removeStudentModule(self.studentIdTest, self.modulIdTest)
        self.assertFalse(self.db.removeStudentModule(self.studentIdTest, self.modulIdTest))
        