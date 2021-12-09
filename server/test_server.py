import unittest
import functions
import hashlib
import sqlite3

connection = sqlite3.connect('database.db')
cursor = connection.cursor()
print("SQLite Database Version is: ", cursor.fetchall())
class TestServerFunc(unittest.TestCase):

	def test_addition(self):
		self.assertEqual(10*10,100)

	def test_addStudent(self):#studiengang, vorname, nachname, email, password
			
		student1=functions.addStudent('TINF19', 'Mix', 'Müzler', 'MO@web.de', '123meins')
		self.assertEqual(student1,True)

	def test_validateStudent(self):#email, password
		validate1=functions.validateStudent('MO@web.de',hashlib.sha256(('123meins').encode('utf-8')).hexdigest())
		self.assertEqual(validate1,True)

	def test_getStudentId(self):#email
		student1=functions.getStudentId('MO@web.de')
		self.assertEqual(student1,5)
		
	def test_getStudentData(self):#email
		student1=functions.getStudentData('MO@web.de')
		self.assertEqual(student1[4],'MO@web.de')

	def test_editStudent(self):#studiengang, vorname, nachname, password, email
		student1=functions.editStudent('TINF19', 'Max', 'Müller', 'Password123', 'MO@web.de')
		self.assertEqual(student1,True)

	def test_removeStudent(self):#id
		student1=functions.removeStudent(5)###### gibt immer true zurück
		self.assertEqual(student1,True)

	def test_getModuleId(self):#name
		student1=functions.getModuleId('Analysis')######
		self.assertEqual(student1,1)

	def test_getModule(self):#id
		student1=functions.getModule(1)
		self.assertEqual(student1[0],1)

	def test_getModules(self):#studentID, semester
		modules1=functions.getModules(2, 1)#########
		a=modules1.count
		self.assertEqual(a,-1)#############

	def test_addModule(self):#name, beschreibung, leistung, ects
		module1=functions.addModule('name', 'beschreibung', 'leistung', 'ects', 'semester')########
		self.assertEqual(module1,True)############

	def test_removeModule(self):#id
		module1=functions.removeModule('id')########
		self.assertEqual(module1,True)#########

	def test_addStudentModule(self):#studentID, moduleID, semester
		studentmodule1=functions.addStudentModule('studentID', 'moduleID', 'semester')#########
		self.assertEqual(studentmodule1,True)########

	def test_editStudentModule(self):#studentID, moduleID, note
		studentmodule1=functions.editStudentModule('studentID', 'moduleID', 'note')###########
		self.assertEqual(studentmodule1,True)#########

	def test_removeStudentModule(self):#studentID, moduleID
		studentmodule1=functions.removeStudentModule('studentID', 'moduleID')########
		self.assertEqual(studentmodule1,True)
		
