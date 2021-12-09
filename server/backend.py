from database import *


def main():
    print('what do you want to do?')
    print('1. create a new student')
    print('2. create a new module')
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
        if addStudent(module, firstname, lastname, email, password):
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
        if addModule(name, description, exam, ects):
            print('module added')
        else:
            print('module already exists')
    else:
        print('invalid input')
    main()


main()
