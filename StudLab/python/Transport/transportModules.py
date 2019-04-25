# -*- coding: utf-8 -*-
from modules import *


def print_matrix(matrx, ai, bi, summ, isPrint = False):
    '''
      Вывод матрицы транспортной задачи
      ...
      Attributes
      ----------
      matrix : ndarray
          Матрица Cij элементов
      a : ndarray
          Ограничения на запасы
      b : ndarray
          Ограничения на потребности
      summ : int
        Суммарная потребность груза / запасы груза
    '''
    #   print("Shape: ", matrx.shape, ai.shape, bi.shape)
    a, b = np.reshape(ai, (-1, 1)), np.concatenate((np.reshape(bi, (1, -1)), [[summ]]), axis=1)
    matrix = np.copy(matrx)
    matrix = np.concatenate((matrix, a), axis=1)
    matrix = np.concatenate((matrix, b), axis=0)
    matrix = matrix.astype(str)
    matrix[matrix == "-99999"] = "x"
    matrix[matrix == "99999"] = "x"
    df = pd.DataFrame(matrix,
                      index=[f"a{i+1}" for i in range(matrix.shape[0] - 1)] + ["Потребности"],
                      columns=[f'b{j+1}' for j in range(matrix.shape[1] - 1)] + ["Запасы"])
    if isPrint:
        display(df)
    return df.to_html()


def fillLineOrCol(matrix, line=-1, col=-1, flag=0):
    '''
      Функция для заполнения строки, столбца или строки и столбца матрицы.
      Returns: fill matrix
      ...
      Attributes
      ----------
      matrix : ndarray
          Матрица
      line : int
          Индекс строки
      col : int
          Индекс столбца
      flag = 0 : int
          0 - Заполнить строку
          1 - Заполнить столбец
          2 - Заполнить строку и столбец
          3 - Заполняет полностью строку и/или столбец, если указаны
    '''
    nmatrix = np.copy(matrix)
    if flag in (0, 2):
        nmatrix[line, col + 1:] = -99999
    if flag in (1, 2):
        nmatrix[line + 1:, col] = -99999

    if flag == 3:
        if line != -1:
            nmatrix[line, :] = 99999
        if col != -1:
            nmatrix[:, col] = 99999

    return nmatrix


def IndMinEl(matrix):
    '''
      Возвращает координаты минимального элемента
      ...
      Attributes
      ----------
      matrix : ndarray
          Матрица
    '''
    return np.unravel_index(np.argmin(matrix, axis=None), matrix.shape)


def putElInMatrix(matrix, original_matrix, indexes, weights):
    '''
      Возвращает координаты минимального элемента
      ...
      Attributes
      ----------
      matrix : ndarray
          Матрица
      original_matrix : ndarray
          Исходная матрица
      indexes : ndarray
          Массив индексов минимальных элементов
      weights : ndarray
          Массив весов элементов
    '''
    mmatrix = np.copy(original_matrix).astype(str)
    weights = np.array(weights).astype(int);
    for i in range(len(indexes)):
        mmatrix[indexes[i]] = str(original_matrix[indexes[i]]) + f"[{weights[i]}]"
    return mmatrix


def getRoad(matrix, coordinate, last, road=np.array([[]])):
    '''
      Возвращает:
      *если цикл найден
        1. ndarray - цикл
        2. ndarray - точки, где цикл меняет направление
      *иначе -1
      ...
      Attributes
      ----------
      matrix : ndarray
          Матрица
      coordinate : tuple(int, int)
          Координаты текущего положения
      road : ndarray
          Путь до текущего местоположения
      turn : ndarray
          Точки смены направления
    '''
    road_new = np.copy(road)
    road_new = np.append(road_new, [coordinate])  # axis=0

    #     turn_new = turn

    # if len(road_new) != 2 and (road_new[-2] == road_new[0] or road_new[-1] == road_new[1]):
    #     return road_new

    #     print(coordinate)
    i, j = coordinate
    c_line, c_col = matrix.shape
    # Направление: Влево
    if j != 0 and not last == 'r':
        #         print("Влево")
        next_point = getNextPoint(matrix, (i, j), 'l', (road_new[0], road_new[1]))
        # Запоминаем смену направления

        # turn_to_fun = turn_new.copy()
        # if (next_point >= 0) and (last != 'l' and last != ''):
        #     turn_to_fun = np.append(turn_to_fun, (i, next_point))

        if next_point == -2:  # Пришли к исходной  точке
            for num in range(1, int(abs(j - road_new[1]))):
                road_new = np.append(road_new, (i, j - num))
            return road_new
        elif next_point != -1:
            road_to_fun = np.copy(road_new)
            for num in range(1, j - next_point):
                road_to_fun = np.append(road_to_fun, [(i, j - num)])

            res = getRoad(matrix, (i, next_point), 'l', road_to_fun)
            if type(res) != type(-1):
                return res

    # Направление: Вправо
    if j != c_col and not last == 'l':
        #         print("Вправо")
        next_point = getNextPoint(matrix, (i, j), 'r', (road_new[0], road_new[1]))
        # turn_to_fun = turn_new.copy()
        # Запоминаем смену направления
        # if (next_point >= 0) and (last != 'r' and last != ''):
        #     turn_to_fun = np.append(turn_to_fun, (i, next_point))

        if next_point == -2:  # Пришли к исходной  точке
            for num in range(1, int(abs(j - road_new[1]))):
                road_new = np.append(road_new, (i, j + num))
            return road_new
        elif next_point != -1:
            road_to_fun = np.copy(road_new)
            for num in range(1, abs(j - next_point)):
                road_to_fun = np.append(road_to_fun, [(i, j + num)])

            res = getRoad(matrix, (i, next_point), 'r', road_to_fun)
            if type(res) != type(-1):
                return res

    # Направление: Вверх
    if i != 0 and not last == 'b':
        #         print("Вверх")
        next_point = getNextPoint(matrix, (i, j), 't', (road_new[0], road_new[1]))

        # Запоминаем смену направления
        # turn_to_fun = turn_new.copy()
        # if (next_point >= 0) and (last != 't' and last != ''):
        #     turn_to_fun = np.append(turn_to_fun, (next_point, j))

        if next_point == -2:  # Пришли к исходной  точке
            for num in range(1, int(abs(i - road_new[0]))):
                road_new = np.append(road_new, (i - num, j))
            return road_new

        elif next_point != -1:
            road_to_fun = np.copy(road_new)
            for num in range(1, abs(i - next_point)):
                road_to_fun = np.append(road_to_fun, [(i - num, j)])

            res = getRoad(matrix, (next_point, j), 't', road_to_fun)
            if type(res) != type(-1):
                return res

    # Направление: Вниз
    if c_line - 1 != i and not last == 't':
        #         print("Вниз")
        next_point = getNextPoint(matrix, (i, j), 'b', (road_new[0], road_new[1]))

        # Запоминаем смену направления
        # turn_to_fun = turn_new.copy()
        # if (next_point >= 0) and (last != 'b' or last != ''):
        #     turn_to_fun = np.append(turn_to_fun, (next_point, j))

        if next_point == -2:  # Пришли к исходной  точке
            for num in range(1, int(abs(i - road_new[0]))):
                road_new = np.append(road_new, (i + num, j))
            return road_new
        elif next_point != -1:
            road_to_fun = np.copy(road_new)
            for num in range(1, abs(i - next_point)):
                road_to_fun = np.append(road_to_fun, [(i + num, j)])

            res = getRoad(matrix, (next_point, j), 'b', road_to_fun)
            if type(res) != type(-1):
                return res
    return -1


# getRoad(test, (0,0),'').reshape((-1,2))


def getNextPoint(matrix, coordinate, direction, start_coord):
    '''
      Возвращает индекс следующего элемента с весом
        если не найден возвращает -1
        если мы можешь придти в исходную точку, возвращает -2
      ...
      Attributes
      ----------
      matrix : ndarray
          Матрица
      coordinate : tuple(int, int)
          Координаты текущего положения
      direction : char {'r', 'l', 't', 'b'}
          Направление двжения
      start_coord : tuple(int, int)
          Координаты самой начальной точки
    '''
    y, x = start_coord  # Координаты начала

    matrix = matrix.astype(str)

    i, j = coordinate
    c_line, c_col = matrix.shape
    # print(c_line, c_col)

    if direction == 'r':
        #     print(j,j<x , x<c_col , i == y)
        if (j < x and x < c_col and i == y):
            return -2
        j += 1
        while j != c_col:
            if '[' in matrix[i, j]:
                return j
            j += 1
        return -1

    if direction == 'l':
        if (x >= 0 and x < j and i == y):
            return -2
        j -= 1
        while j >= 0:
            if '[' in matrix[i, j]:
                return j
            j -= 1
        return -1

    if direction == 'b':
        if (x == j and i < y and y < c_line):
            return -2
        i += 1
        while i != c_line:
            if '[' in matrix[i, j]:
                return i
            i += 1
        return -1

    if direction == 't':
        if (x == j and y >= 0 and y < i):
            return -2
        i -= 1
        while i >= 0:
            #       print(i,j)
            #       print(matrix[i,j])
            if '[' in matrix[i, j]:
                return i
            i -= 1
        return -1


def getСorners(road):
    '''
      Возвращает индекс угловых элементов
      ...
      Attributes
      ----------
      road : ndarray
          Координаты точек цикла
    '''
    result = np.array([road[0]])
    for i in range(1, len(road)):  # ищем углы
        if not (road[i - 1:i + 2, 0] == road[i, 0]).all() and \
                not (road[i - 1:i + 2, 1] == road[i, 1]).all():
            result = np.append(result, road[i])
    #         print(f'Cell: {road[i]}')

    corner = np.concatenate((road[-2:], [road[0]]))
    #   print(corner)
    #   print(not (corner[:,0]==road[0,0]).all() , not (corner[:,1]==road[0,1]).all(), road[0,0], road[0,1])
    if not (corner[:, 0] == road[0, 0]).all() and not (corner[:, 1] == road[0, 1]).all():
        result = np.append(result, road[-1])

    return result.reshape((-1, 2))


# getСorners(res)
def getF(matrix):
    '''
      Считает значение целевой функции
      ...
      Attributes
      ----------
      matrix : ndarray
          Матрица
    '''
    res, string = 0, "F(x) = "
    for line in matrix:
        for el in line:
            if '[' in el:
                C, count = el.split('[')
                # print(C,count[:-1],sep=' - ')
                C, count = float(C), float(count[:-1])
                res += C * count
                string += f'{C}*{count} +'

    return string[:-1] + f'= {res}'


def getMinAorB(corners, matrix):
    '''
      Получение наименьшего количества
      ...
      Attributes
      ----------
      corners : ndarray
          Координаты угловых элементов
      matrix : ndarray
          Матрица
    '''
    #   print(corners)
    Min = 9999999
    for num in range(1, len(corners), 2):
        #     print(corners[num])
        i, j = corners[num].astype(int)
        #     print(matrix[i,j],matrix[i,j].split('['))
        # print(matrix[i, j].split('['))
        # print(matrix[i, j].split('[')[1][:-1])
        count = float(matrix[i, j].split('[')[1][:-1])
        Min = min(Min, count)
    return Min


def getEvaluationsCycles(matrix, originalMatrix):
    '''
      Возвращает координаты клетки с
      наименьшим отрицательным весом цикла,
      если нет отрицательных, возвращает -1
      ...
      Attributes
      ----------
      matrix : ndarray
          Матрица полученная с помощью методов СЗУгла
          или Наименьшего элемента
      originalMatrix : ndarray
          Исходная матрица
    '''
    strResult = ''
    originalMatrix = originalMatrix.astype(int)

    import re
    indexes, ratings = [], []
    for line in range(matrix.shape[0]):
        for col in range(matrix.shape[1]):
            if '[' not in matrix[line, col]:

                strResult+= f'({line+1},{col+1}): В свободную клетку({line+1},{col+1}) поставим знак' + \
                      ' «+»,\nа в остальных вершинах многоугольника чередующиеся знаки «-», «+», «-». \n'  # Цикл от этой точки

                road = getRoad(matrix, (line, col), '').reshape((-1, 2))  # Получаем цикл
                tempMatrix = matrix.copy()

                # Закрашиваем ячейки цикла
                for vect in road:
                    y, x = vect.astype(int)
                    tempMatrix[y, x] = '.' + tempMatrix[y, x]
                df = pd.DataFrame(tempMatrix)
                styled = (df.style
                          .applymap(lambda v: 'background-color: %s' % 'green' if ('.' in v) else ''))
                # display(styled)  # Вывод цикла
                strResult += styled.render()+'\n'

                # Высчитываем оценку цикла
                corners = getСorners(road)
                rating = 0
                for i in range(len(corners)):
                    y, x = corners[i].astype(int)
                    rating += -originalMatrix[y, x] if i % 2 else originalMatrix[y, x]
                if rating < 0:
                    ratings.append(rating)  # Оценка цикла для последней точки
                    indexes.append((line, col))  # Точка с отрицательной оценкой
                strResult+=f'Оценка свободной клетки равна Δ{line+1}{col+1} = {rating}.\n\n\n'
    if len(ratings):
        return indexes[np.argmin(ratings)], strResult
    return -1, strResult


def getCountMN(matrix):
    '''
      Считает кол-во выбранных клеток
      ...
      Attributes
      ----------
      matrix : ndarray
          Матрица полученная с помощью методов СЗУгла
          или Наименьшего элемента
    '''
    #   count = sum(matrix.shape)-1
    string = ""
    for line in matrix:
        string += "".join(line)
    return string.count("[")


# Функция для сокращения кода основной функции Метода Патанциалов
def true(u, v):
    return not (u != -9999).all() or not (v != -9999).all()


def value(matrix, line, col):
    return int(matrix[line, col].split('[')[0])


def checkMatrix(matrix, ai, bi):
    '''
      Проверяет матрицу на открытость/закрытость
      Возвращает:
        matrix - Закрытую матрицу
        a - Запасы
        b - Потребности
      ...
      Attributes
      ----------
      matrix : ndarray
          Матрица
      ai : ndarray
          Ограничения на запасы
      bi : ndarray
          Ограничения на потребности
    '''
    result_string = ''
    summA, summB = sum(ai), sum(bi)
    result_string = 'Проверим необходимое и достаточное условие разрешимости задачи.<br>' + \
          f'∑a = {summA}<br>    ∑b = {summB} '+'<br>'
    if summA != summB:
        if summA > summB:
            result_string+='Как видно, суммарная потребность груза в пунктах назначения превышает запасы груза на базах.<br>' + \
                  'Следовательно, модель исходной транспортной задачи является открытой. Чтобы получить закрытую модель,<br>' + \
                  f'введем дополнительную (фиктивную) базу с запасом груза, равным {summA-summB} ({summA}-{summB}). <br>' + \
                  'Тарифы перевозки единицы груза из базы во все магазины полагаем равны нулю. '+'<br>'
            new_colon = np.zeros((ai.shape[0], 1)).astype(int)
            #       print(new_colon.shape, matrix.shape)
            matrix = np.concatenate((matrix, new_colon), axis=1)
            bi = np.append(bi, summA - summB)
        elif summA < summB:
            result_string+='Как видно, суммарная потребность груза в пунктах назначения меньше запасов груза на базах.<br>' + \
                  'Следовательно, модель исходной транспортной задачи является открытой. Чтобы получить закрытую модель,<br>' + \
                  f'введем дополнительную (фиктивную) потребность, равной {summB-summA} ({summB}-{summA}). <br>' + \
                  'Тарифы перевозки единицы груза к этому магазину полагаем равны нулю.  '+'\n'
            new_line = np.zeros(bi.shape).astype(int)
            matrix = np.concatenate((matrix, [new_line]), axis=0)
            ai = np.append(ai, summB - summA)
        result_string+='Занесем исходные данные в распределительную таблицу.'+'<br>'
        print_matrix(matrix, ai, bi, max(summA, summB))
    else:
        result_string+='Условие баланса соблюдается. Запасы равны потребностям. <br>' \
                       'Следовательно, модель транспортной задачи является закрытой.'+'<br>'
    return matrix, ai, bi, result_string
