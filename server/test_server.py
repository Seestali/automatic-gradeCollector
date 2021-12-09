import unittest
import hashlib
import database


class TestServerFunc(unittest.TestCase):
	def test_addStudent(self):#studiengang, vorname, nachname, email, password
		student1=functions.addStudent('TINF19', 'Max', 'Müller', 'MM@web.de', 'Password123')
		student2=['0','TINF19','Max','Müller','MM@web.de', hashlib.sha256(hashlib.sha256(('Passwort123').encode('utf-8')).hexdigest()).hexdigest()]
		self.assertEqual(student1[1:5],student2[1:5])

	def test_validateStudent(self):#email, password
		validate1=functions.validateStudent('MM@web.de',hashlib.sha256(('Passwort123').encode('utf-8')).hexdigest())
		self.assertEqual(validate1,True)

	def test_getStudentId(self):#email
		student1=functions.getStudentId('MM@web.de')
		self.assertEqual(student1[4],'MM@web.de')
		
	def test_getStudentData(self):#email
		student1=functions.getStudentData('MM@web.de')
		self.assertEqual(student1[4],'MM@web.de')

	def test_editStudent(self):#studiengang, vorname, nachname, password, email
		student1=functions.editStudent('TINF19', 'Max', 'Müller', 'Password123', 'MM@web.de')
		self.assertEqual(student1,True)

	def test_removeStudent(self):#id
		student1=functions.removeStudent('id')######
		self.assertEqual(student1,True)

	def test_getModuleId(self):#name
		student1=functions.getModuleId('name')######
		self.assertEqual(student1[1],'name')

	def test_getModule(self):#id
		student1=functions.getModule('id')
		self.assertEqual(student1[0],'id')

	def test_getModules(self):#studentID, semester
		modules1=functions.getModules('studentID', 'semester')#########
		self.assertEqual(modules1.count,7)#############

	def test_addModule(self):#name, beschreibung, leistung, ects
		module1=functions.addModule('name', 'beschreibung', 'leistung', 'ects')########
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
		
