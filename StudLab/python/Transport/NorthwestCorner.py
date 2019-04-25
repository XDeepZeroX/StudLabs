# -*- coding: utf-8 -*-
from Transport.transportModules import *
from Transport.Optimizers.optimizers import *
from PrepareTable import *
import argparse

def NorthwestCorner(data):
    '''
      Метод Северо-Западного угла
      ...
      Attributes
      ----------
      matrixx : ndarray
          Матрица Cij
      ai : ndarray
          Ограничения на запасы
      bi : ndarray
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

    matrixx = args.matrix
    ai = args.avector
    bi = args.bvector
    sep = args.sep
    endRow = args.endRow
    optimizer = None
    try:
        optimizer = OPTIMIZERS[args.optimizer]
    except KeyError:
        print("Кто то пытался подделать метод оптимизации")



    matrixx = StrToNPMatrix(matrixx, sep, endRow)
    ai = StrToVectorNP(ai, sep)
    bi = StrToVectorNP(bi, sep)

    print_matrix(matrixx, ai, bi, '', True)
    matrixx, ai, bi, result_string = checkMatrix(matrixx, ai, bi)
    #   print(matrixx.shape, a.shape, b.shape)
    a, b = ai.copy(), bi.copy()
    new_matrix = matrixx.copy()
    summ = sum(a)

    i, j = 0, 0
    c_line, c_col = new_matrix.shape
    resultStr = "F(x)="
    resultNum = 0

    while (True):
        flag = False
        #   for _ in range(20):
        result_string += f"Искомый элемент равен c{i+1}{j+1}={new_matrix[i,j]}. Для этого элемента запасы равны {a[i]}, потребности {b[j]}." + \
                 f"<br>Поскольку минимальным является {min(a[i], b[j])}, то вычитаем его." + \
                 f"<br>x{i+1}{j+1} = min({a[i]},{b[j]}) = {min(a[i], b[j])}."+'<br>'
        # print(string)

        Min = min(a[i], b[j])
        a[i], b[j] = a[i] - Min, b[j] - Min
        resultStr += f'{new_matrix[i,j]}*{Min} + '
        resultNum += float(new_matrix[i, j]) * Min

        new_matrix[i, j] += f"[{Min}]"

        # Закрашиваем строку и/или столбец
        temp_i, temp_j = i, j
        #     print(i,j)
        #     print(a[i],b[j])
        if a[i] == 0:
            new_matrix = fillLineOrCol(new_matrix, temp_i, temp_j)
            i += 1
            flag = True
        if b[j] == 0 and not flag:
            new_matrix = fillLineOrCol(new_matrix, temp_i, temp_j, flag=1)
            j += 1

        result_string+= print_matrix(new_matrix, a, b, summ)+'<br>'

        # Проверяем уловие останова
        #     print(i,j)
        if i == c_line or j == c_col:
            # Преобразуем и вернем матрицу
            for i in range(new_matrix.shape[0]):
                for j in range(new_matrix.shape[1]):
                    if '[' not in new_matrix[i, j]:
                        new_matrix[i, j] = str(matrixx[i, j])

            result_string+= 'Значение целевой функции для этого опорного плана равно:<br>F(x)=' + getF(new_matrix)+'<br>'
            if(optimizer != None):
                result_string += optimizer(new_matrix,matrixx, ai, bi)
            return result_string
    resultStr = resultStr[:-2] + f"= {resultNum}"
    result_string+="<br><br>Значение целевой функции для этого опорного плана равно:<br>" + \
          f"{resultStr}"+'<br>'



# Call function
# northwest_corner(matrix,avector,bvector)