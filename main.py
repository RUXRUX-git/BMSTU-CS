# from curses.ascii import BEL, isdigit
# import string


# example_string = "(123+44)-55"

# class SymNotFoundException(BaseException):
#     pass

# class CommandNotFoundException(BaseException):
#     pass

# class UndefinedSymbolException(BaseException):
#     pass

# class Commands:
#     BEGIN = 0
#     PLUS = 1
#     MINUS = 2
#     MULTIPLY = 3
#     DIVIDE = 4
#     OPEN_BRACKET = 5
#     CLOSE_BRACKET = 6

#     def is_math_operand(num):
#         return num >= 1 and num <= 4
    
#     def is_bracket(num):
#         return num == 5 or num == 6

# class Actions:
#     I = 1
#     II = 2
#     III = 3
#     IV = 4
#     EXIT = 5

# sym_and_command_to_action = {
#     '+': {
#         Commands.BEGIN: Actions.I,
#         Commands.PLUS: Actions.II,
#         Commands.MINUS: Actions.IV,
#         Commands.MULTIPLY: Actions.IV, 
#         Commands.DIVIDE: Actions.IV,
#         Commands.OPEN_BRACKET: Actions.I,
#     },
#     '-': {
#         Commands.BEGIN: Actions.I,
#         Commands.PLUS: Actions.I,
#         Commands.MINUS: Actions.II,
#         Commands.MULTIPLY: Actions.IV, 
#         Commands.DIVIDE: Actions.IV,
#         Commands.OPEN_BRACKET: Actions.I,
#     },
#     '*': {
#         Commands.BEGIN: Actions.I,
#         Commands.PLUS: Actions.I,
#         Commands.MINUS: Actions.I,
#         Commands.MULTIPLY: Actions.II, 
#         Commands.DIVIDE: Actions.IV,
#         Commands.OPEN_BRACKET: Actions.I,
#     },
#     '/': {
#         Commands.BEGIN: Actions.I,
#         Commands.PLUS: Actions.I,
#         Commands.MINUS: Actions.I,
#         Commands.MULTIPLY: Actions.I, 
#         Commands.DIVIDE: Actions.II,
#         Commands.OPEN_BRACKET: Actions.I,
#     },
#     '(': {
#         Commands.BEGIN: Actions.I,
#         Commands.PLUS: Actions.I,
#         Commands.MINUS: Actions.I,
#         Commands.MULTIPLY: Actions.I, 
#         Commands.DIVIDE: Actions.I,
#         Commands.OPEN_BRACKET: Actions.I,
#     },
#     ')': {
#         Commands.PLUS: Actions.IV,
#         Commands.MINUS: Actions.IV,
#         Commands.MULTIPLY: Actions.IV, 
#         Commands.DIVIDE: Actions.IV,
#         Commands.OPEN_BRACKET: Actions.III,
#     },
#     '': {
#         Commands.BEGIN: Actions.EXIT,
#         Commands.PLUS: Actions.IV,
#         Commands.MINUS: Actions.IV,
#         Commands.MULTIPLY: Actions.IV, 
#         Commands.DIVIDE: Actions.IV,
#     },
# }

# def get_action(command, sym) -> int:
#     commands_to_actions = sym_and_command_to_action.get(sym, None)
#     if commands_to_actions == None:
#         raise SymNotFoundException()

#     action = commands_to_actions.get(command, None)
#     if action == None:
#         raise CommandNotFoundException()

#     return action


# def symbol_to_command(sym):
#     print("symbol_to_command called")
#     print(f"CURRENT SYM: '{sym}'")
#     if sym == '+':
#         return Commands.PLUS
#     elif sym == '-':
#         return Commands.MINUS
#     elif sym == '*':
#         return Commands.MULTIPLY
#     elif sym == '/':
#         return Commands.DIVIDE
#     elif sym == '(':
#         return Commands.OPEN_BRACKET
#     elif sym == ')':
#         return Commands.CLOSE_BRACKET
#     else:
#         raise UndefinedSymbolException()

# command_priority = {
#     Commands.PLUS: 1,
#     Commands.MINUS: 2,
#     Commands.MULTIPLY: 3,
#     Commands.DIVIDE: 4,
#     Commands.OPEN_BRACKET: 10,
#     Commands.CLOSE_BRACKET: 10,
# }

# known_symbols = [
#     '+', '-', '(', ')', '*', '/', *string.digits
# ]

# math_operands = ['+', '-', '*', '/']

# def is_math_operand(sym):
#     return sym in math_operands

# def is_bracket(sym):
#     return sym == '(' or sym == ')'

# class K:
#     acceptable_types = ['+', '-', '*', '/', '(', ')', 'number']
#     def __init__(self, type_string, value=None) -> None:
#         if type_string in K.acceptable_types:
#             self.type = type_string
#             self.value = value
#         else:
#             raise ValueError(f"unknown type: '{type_string}'")

#     def __str__(self):
#         return f"K(type='{self.type}',value='{self.value}')"

#     def __repr__(self):
#         return f"K(type='{self.type}',value='{self.value}')"


# commands = []
# op_stack = [Commands.BEGIN]
# current_literal = ''
# current_pos = 0
# current_sym = ''
# k = 1
# while True:
#     print('*'*30)
#     print(f'Шаг номер {k}:')
#     k += 1
#     print('*'*30)
#     print(f"Команды: {commands}")
#     print(f"Стек операций: {op_stack}")
#     print(f"Текущий литерал: {current_literal}")
#     print(f"Текущая позиция: {current_pos}")
#     # Пустой символ тут означает конец ввода
#     current_sym = example_string[current_pos] if current_pos < len(example_string) else ''
#     if current_sym.isdigit():
#         current_literal += current_sym
#         current_pos += 1
#     else:
#         if current_literal != '':
#             commands.append(K('number', current_literal))
#             current_literal = ''

#         action = get_action(op_stack[-1], current_sym)
#         print(f"ACTION: '{action}'")
#         if action == Actions.EXIT:
#             break
#         operation = symbol_to_command(current_sym)
#         if action == Actions.I:
#             op_stack.append(operation)
#             current_pos += 1
#         elif action == Actions.II:
#             op_stack.append(operation)
#             commands.append(K(current_sym))
#             current_pos += 1
#         elif action == Actions.III:
#             del op_stack[-1]
#             current_pos += 1
#         elif action == Actions.IV:
#             del op_stack[-1]
#             commands.append(K(current_sym))
#         else:
#             raise RuntimeError(f"Неизвестное действие под номером <{action}>")





# #!/usr/bin/env python
# # -*- coding: utf-8 -*-
# """
# Пример функции для вычисления выражений записанных в виде обратной
# польской нотации.
# OPERATORS - список доступных операторов
# RPNError  - класс для обработки исключений
# FailTest  - класс для проерки тест-кейсов
# OKTest    - класс для проерки тест-кейсов
# reversed_polish_notation - главная функция вычисляющая выражение
# """
 
# import re
# import unittest


# #Список доступных операторов связанных с реальными методами float
# OPERATORS = {
#     '+': float.__add__, 
#     '-': float.__sub__,
#     '*': float.__mul__,
#     '/': float.__truediv__,
#     '%': float.__mod__,
#     '^': float.__pow__,
# }


# class RPNError(Exception):
#     """Базовый класс для обработки исключений"""
    
#     def __init__(self, message):
#         """Сохранение текста исключения"""
#         self._message = u"Ошибка вычисления выражения: %s" % message
        
#     def _get_message(self):
#         """Заглушка для свойства message"""
#         return self._message
#     message = property(_get_message)
 

# class FailTest(unittest.TestCase):
#     """Тесты с ошибочными исходными данными"""
        
#     def runTest(self):
#         """Проверка кейсов"""
#         self.assertRaises(RPNError, reversed_polish_notation, "+")
#         self.assertRaises(RPNError, reversed_polish_notation, "2 +")
#         self.assertRaises(RPNError, reversed_polish_notation, "2 2")
#         self.assertRaises(RPNError, reversed_polish_notation, "+ 2 2")
#         self.assertRaises(RPNError, reversed_polish_notation, "a 2 -")
#         self.assertRaises(RPNError, reversed_polish_notation, "2 2 +-")


# class OKTest(unittest.TestCase):
#     """Тесты с корректными исходными данными"""
    
#     def runTest(self):
#         """Проверка кейсов"""
#         self.assertEqual(666, reversed_polish_notation("666"))
#         self.assertEqual(2*3+4, reversed_polish_notation("2 3 * 4 +"))
#         self.assertEqual(2*(3+4), reversed_polish_notation("2 3 4 + *"))
#         self.assertEqual((7.0/2).__pow__(4), reversed_polish_notation("7 2 / 4 ^"))
#         self.assertEqual((2**3)**4, reversed_polish_notation("2 3 ^ 4 ^"))
#         self.assertEqual(2.0+3.5-6, reversed_polish_notation("2.0 3.5 + 6 -"))
#         self.assertEqual(3**4, reversed_polish_notation("3   3  *   3  *  3 *"))
#         self.assertEqual(5+((1+2)*4)-3, \
#             reversed_polish_notation("5 1 2 + 4 * 3 -+"))


# def reversed_polish_notation(expr):
#     """
#     Возвращает результат вычисленного выражения записанного в виде обратной
#     польской нотации
#     expr = string
#     """
#     ops = OPERATORS.keys()
#     stack = [] 

#     for atom in re.split(r"\s+", expr):
#         try:
#             atom = float(atom)
#             stack.append(atom)
#         except ValueError:
#             for oper in atom:
#                 if oper not in ops: 
#                     continue
#                 try:
#                     oper2 = stack.pop()
#                     oper1 = stack.pop()
#                 except IndexError:
#                     raise RPNError(u"Маловато операндов")

#                 try:
#                     oper = OPERATORS[oper](oper1, oper2)
#                 except ZeroDivisionError:
#                     raise RPNError(u"Нельзя делить на 0")

#                 stack.append(oper)

#     if len(stack) != 1:
#         raise RPNError(u"Многовато операндов")

#     return stack.pop()



# if __name__ == "__main__":
#     unittest.main()

import logging
logging.basicConfig(level=logging.INFO)


class SymNotFoundException(BaseException):
    pass

class OperationNotFoundException(BaseException):
    pass

class UndefinedSymbolException(BaseException):
    pass

class Operations:
    BEGIN = 0
    PLUS = 1
    MINUS = 2
    MULTIPLY = 3
    DIVIDE = 4
    OPEN_BRACKET = 5
    EXIT = 6

class Actions:
    I = 1
    II = 2
    III = 3
    IV = 4
    EXIT = 5

class K:
    acceptable_types = ['+', '-', '*', '/', '(', ')', 'number']

    def __init__(self, type_string, value=None) -> None:
        if type_string in K.acceptable_types:
            self.type = type_string
            self.value = value
        else:
            raise ValueError(f"unknown type: '{type_string}'")

    def __str__(self):
        return f"K(type='{self.type}',value='{self.value}')"

    def __repr__(self):
        return f"K(type='{self.type}',value='{self.value}')"

sym_and_operation_to_action = {
    '+': {
        Operations.BEGIN: Actions.I,
        Operations.PLUS: Actions.II,
        Operations.MINUS: Actions.IV,
        Operations.MULTIPLY: Actions.IV,
        Operations.DIVIDE: Actions.IV,
        Operations.OPEN_BRACKET: Actions.I,
    },
    '-': {
        Operations.BEGIN: Actions.I,
        Operations.PLUS: Actions.I,
        Operations.MINUS: Actions.II,
        Operations.MULTIPLY: Actions.IV,
        Operations.DIVIDE: Actions.IV,
        Operations.OPEN_BRACKET: Actions.I,
    },
    '*': {
        Operations.BEGIN: Actions.I,
        Operations.PLUS: Actions.I,
        Operations.MINUS: Actions.I,
        Operations.MULTIPLY: Actions.II,
        Operations.DIVIDE: Actions.IV,
        Operations.OPEN_BRACKET: Actions.I,
    },
    '/': {
        Operations.BEGIN: Actions.I,
        Operations.PLUS: Actions.I,
        Operations.MINUS: Actions.I,
        Operations.MULTIPLY: Actions.I,
        Operations.DIVIDE: Actions.II,
        Operations.OPEN_BRACKET: Actions.I,
    },
    '(': {
        Operations.BEGIN: Actions.I,
        Operations.PLUS: Actions.I,
        Operations.MINUS: Actions.I,
        Operations.MULTIPLY: Actions.I,
        Operations.DIVIDE: Actions.I,
        Operations.OPEN_BRACKET: Actions.I,
    },
    ')': {
        Operations.PLUS: Actions.IV,
        Operations.MINUS: Actions.IV,
        Operations.MULTIPLY: Actions.IV,
        Operations.DIVIDE: Actions.IV,
        Operations.OPEN_BRACKET: Actions.III,
    },
    '': {
        Operations.BEGIN: Actions.EXIT,
        Operations.PLUS: Actions.IV,
        Operations.MINUS: Actions.IV,
        Operations.MULTIPLY: Actions.IV,
        Operations.DIVIDE: Actions.IV,
    },
}

def get_action(operation, sym) -> int:
    logging.info(f"get_action called with operation: '{operation}' and symbol: '{sym}'")

    operations_to_actions = sym_and_operation_to_action.get(sym, None)
    if operations_to_actions == None:
        raise SymNotFoundException(f"Symbol not found: '{sym}'")

    action = operations_to_actions.get(operation, None)
    if action == None:
        raise OperationNotFoundException(f"Operation not found: '{operation}'")

    return action

def symbol_to_operation(sym):
    logging.info(f"symbol_to_operation called with symbol: '{sym}'")

    if sym == '+':
        return Operations.PLUS
    elif sym == '-':
        return Operations.MINUS
    elif sym == '*':
        return Operations.MULTIPLY
    elif sym == '/':
        return Operations.DIVIDE
    elif sym == '(':
        return Operations.OPEN_BRACKET
    elif sym == '':
        return Operations.EXIT
    else:
        raise UndefinedSymbolException(f"Undefined symbol: '{sym}'")

def operation_to_symbol(operation):
    logging.info(f"operation_to_symbol called with operation: '{operation}'")

    if operation == Operations.PLUS:
        return '+'
    elif operation == Operations.MINUS:
        return '-'
    elif operation == Operations.MULTIPLY:
        return '*'
    elif operation == Operations.DIVIDE:
        return '/'
    elif operation == Operations.OPEN_BRACKET:
        return '('
    elif operation == Operations.EXIT:
        return ''
    else:
        raise OperationNotFoundException(f"Undefined operation: '{operation}'")


def make_reverse_polish(equation: str):
    i_debug = 1

    digits = [str(i) for i in range (10)]
    op_symbols = ["+", "-", "*", "/", "(", ")", ""]


    operations = [Operations.BEGIN]
    commands = []

    pos = 0
    literal = ''
    while operations:
        logging.info(f"Step number {i_debug}")
        logging.info(f"operations: {operations}")
        logging.info(f"commands: {commands}")
        logging.info(f"literal: '{literal}'")

        sym = ''
        if pos < len(equation):
            sym = equation[pos]

        logging.info(f"Current symbol: '{sym}'")

        if sym in digits:
            literal += sym
            pos += 1
        elif sym in op_symbols:
            if literal != '':
                commands.append(K('number', literal))
                literal = ''

            action = get_action(operations[-1], sym)
            logging.info(f"Current action: '{action}'")
            if action == Actions.I:
                operation = symbol_to_operation(sym)
                operations.append(operation)
                pos += 1
            elif action == Actions.II:
                operation = symbol_to_operation(sym)
                operations.append(operation)
                commands.append(K(sym))
                pos += 1
            elif action == Actions.III:
                del operations[-1]
                pos += 1
            elif action == Actions.IV:
                op_symbol = operation_to_symbol(operations[-1])
                commands.append(K(op_symbol))
                del operations[-1]
            elif action == Actions.EXIT:
                break
            else:
                raise RuntimeError(f"Неизвестное действие под номером <{action}>")
        else:
            raise UndefinedSymbolException(f"Unknown symbol: '{sym}'")

        i_debug += 1

    return commands

def interpret_reverse_polish(commands):
    operands = []
    while commands:
        if commands[0].type == 'number':
            operands.append(int(commands[0].value))
        else:
            if commands[0].type == '+':
                operands[-2] = operands[-2] + operands[-1]
            elif commands[0].type == '-':
                operands[-2] = operands[-2] - operands[-1]
            elif commands[0].type == '*':
                operands[-2] = operands[-2] * operands[-1]
            elif commands[0].type == '/':
                operands[-2] = operands[-2] / operands[-1]
            else:
                raise RuntimeError(f"Unknown command: {commands[0]}")
            del operands[-1]
        del commands[0]

    return operands[0]

eq = input("Введите математическое выражение: ")
commands = []
try:
    commands = make_reverse_polish(eq)
except Exception:
    print("Ошибка построения польской записи - неверный формат выражения")

print(f"Результат вычисления: {interpret_reverse_polish(commands)}")
