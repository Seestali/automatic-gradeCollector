from database import Database


def main():
    '''
    This functionen is used for the backend in order to edit the database.
    '''
    print('what do you want to do?')
    print('1. create a new student')
    print('2. create a new module')
    print('3. add a module to a student and its semester')
    print('4. change student')
    print('5. change module')
    print('6. delete a student')
    print('7. delete a module')
    print('8. delete a module from a student')
    choice = input('> ')
    if choice == '1':
        print('create a new student')
        print('enter the student\'s firstname')
        firstname = input('> ')
        print('enter the student\'s lastname')
        lastname = input('> ')
        print('enter the student\'s email')
        email = input('> ')
        print('enter the student\'s password')
        password = input('> ')
        print('enter the student\'s course of study')
        module = input('> ')
        if db.addStudent(module, firstname, lastname, email, password):
            print('student added')
        else:
            print('student not added')
    elif choice == '2':
        print('create a new module')
        print('enter the module\'s name')
        name = input('> ')
        print('enter the module\'s description')
        description = input('> ')
        print('enter the module\'s exam performance')
        exam = input('> ')
        print('enter the module\'s ects')
        ects = input('> ')
        if db.addModule(name, description, exam, ects):
            print('module added')
        else:
            print('module already exists')
    elif choice == '3':
        print('Please select a student from the list')
        for row in db.getTableStudents():
            print(row)
        print('enter the student\'s id')
        studentID = input('> ')
        print('enter the semster (1-6)')
        semester = input('> ')
        print('Please select a module from the list (ID)')
        for row in db.getTableModules():
            print(row)
        print('enter the module\'s id')
        moduleID = input('> ')
        if db.addStudentModule(studentID, moduleID, semester):
            print('module/student added')
        else:
            print('module/student already exists')
    elif choice == '4':
        print('Please select a student from the list')
        for row in db.getTableStudents():
            print(row)
        print('enter the student\'s id')
        studentID = input('> ')
        print(db.getStudentData(db.getStudentMail(studentID)))
        print('enter the student\'s firstname')
        firstname = input('> ')
        print('enter the student\'s lastname')
        lastname = input('> ')
        print('enter the student\'s password')
        password = input('> ')
        print('enter the student\'s course of study')
        module = input('> ')
        if db.editStudent(module, firstname, lastname, password, db.getStudentMail(studentID)):
            print('student changed')
            print(db.getStudentData(db.getStudentMail(studentID)))
        else:
            print('failed to change student')
    elif choice == '5':
        print('Please select a module from the list (ID)')
        for row in db.getTableModules():
            print(row)
        print('enter the module\'s id')
        moduleID = input('> ')
        print(db.getModule(moduleID))
        print('enter the module\'s name')
        name = input('> ')
        print('enter the module\'s description')
        description = input('> ')
        print('enter the module\'s exam performance')
        exam = input('> ')
        print('enter the module\'s ects')
        ects = input('> ')
        if db.editModule(name, description, exam, ects, moduleID):
            print('module changed')
            print(db.getModule(moduleID))
        else:
            print('failed to change module')

    elif choice == '6':
        print('Please select a student from the list')
        for row in db.getTableStudents():
            print(row)
        print('enter the student\'s id')
        studentID = input('> ')
        if db.removeStudent(studentID):
            print('student deleted')
        else:
            print('student not deleted')
    elif choice == '7':
        print('Please select a module from the list (ID)')
        for row in db.getTableModules():
            print(row)
        print('enter the module\'s id')
        moduleID = input('> ')
        if db.removeModule(moduleID):
            print('module deleted')
        else:
            print('module doesnt exists')
    elif choice == '8':
        print('Please select a student from the list')
        for row in db.getTableStudents():
            print(row)
        print('enter the student\'s id')
        studentID = input('> ')  #
        print('Please select a module from the list (ID)')
        for row in db.getTableModules():
            print(row)
        print('enter the module\'s id')
        moduleID = input('> ')
        if db.removeStudentModule(studentID, moduleID):
            print('module form student deleted')
        else:
            print('module drom student doesnt exist')
    else:
        print('invalid input')
    main()


db = Database('database.db')
main()
