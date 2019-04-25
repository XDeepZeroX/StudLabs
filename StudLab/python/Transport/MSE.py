# -*- coding: utf-8 -*-
from Transport.transportModules import *
from Transport.Optimizers.optimizers import *
from PrepareTable import *
import argparse

def MSE(data):
    '''
      Метод Наименьшего элементы
      ...
      Attributes
      ----------
      matrix : ndarray
          Матрица Cij (стоимостей пути)
      a : ndarray
          Ограничения на запасы
      b : ndarray
          Ограничения на потребности
    '''

    parser = argparse.ArgumentParser()
    parser.add_argument("--optimizer", type=str)
    parser.add_argument("--matrix", type=str)
    parser.add_argument("--avector", type=str)
    parser.add_argument("--bvector", type=str)
    parser.add_argument("--sep", type=str, default=';')
    parser.add_argument("--endRow", type=str, default='#')

    args, unknown  = parser.parse_known_args(data.split())

    matrix = args.matrix
    a = args.avector
    b = args.bvector
    sep = args.sep
    endRow = args.endRow
    optimizer = None
    try:
        optimizer = OPTIMIZERS[args.optimizer]
    except KeyError:
        print("Кто то пытался подделать метод оптимизации")



    matrix = StrToNPMatrix(matrix, sep, endRow)
    a = StrToVectorNP(a, sep)
    b = StrToVectorNP(b, sep)
    a_original = a.copy()
    b_original = b.copy()

    print_matrix(matrix, a, b, '', True)

    matrix, a, b, resString = checkMatrix(matrix, a, b)
    matrix_original = matrix.copy()

    coordinate_min = []  # Координаты минимальных элементов
    weights = []
    resultStr = "F(x)="
    resultNum = 0

    c_line, c_col = matrix.shape
    Summ = sum(a)

    clone_matrix = np.copy(matrix)

    while (True):
        flag = False
        i, j = IndMinEl(clone_matrix)

        Min = min(a[i], b[j])
        if a[i] == 0 and b[j] == 0:
            break
        coordinate_min.append((i, j))
        weights.append(Min)

        resultStr += f"{matrix[i,j]}*{Min} + "
        resultNum += int(matrix[i, j]) * Min

        resString+= f"Искомый элемент равен C{i+1}{j+1}=1. Для этого элемента запасы равны {a[i]}, потребности {b[j]}.\n" + \
              f"Поскольку минимальным является {Min}, то вычитаем его.\n"+\
              f"X{i+1}{j+1} = min({a[i]},{b[j]}) = {Min}. \n"
        a[i], b[j] = a[i] - Min, b[j] - Min
        if a[i] == 0:
            clone_matrix = fillLineOrCol(clone_matrix, line=i, flag=3)
            i += 1
            flag = True
        if b[j] == 0 and not flag:
            clone_matrix = fillLineOrCol(clone_matrix, col=j, flag=3)
            j += 1

        resString+=print_matrix(putElInMatrix(clone_matrix, matrix, coordinate_min, weights), a, b, Summ)+'\n'

    #     print_matrix(clone_matrix, a,b, Summ)

    resultStr = resultStr[:-2] + f"= {resultNum}"
    resString += "\n\nЗначение целевой функции для этого опорного плана равно:\n" + \
          f"{resultStr}\n"
    #   print_matrix(matrix, a,b, Summ)
    if(optimizer != None):
        matrix = putElInMatrix(clone_matrix, matrix, coordinate_min, weights)
        resString += optimizer(matrix,matrix_original, a_original, b_original)
    return resString

