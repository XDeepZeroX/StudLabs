# -*- coding: utf-8 -*-
from Transport.transportModules import *

def MethodPotenc(matrix,original_matrix, a, b):
    strResult = ''
    matrix = matrix.copy()
    c_line, c_col = matrix.shape
    summ = sum(a)
    while True:
        u, v = np.full(c_line, -9999), np.full(c_col, -9999)
        u[0] = 0
        indexes, ratings = [], []
        strResult+='\n\nПроверим оптимальность опорного плана. Найдем предварительные потенциалы ui, vj.\n' + \
              'по занятым клеткам таблицы, в которых ui + vj = cij, полагая, что u1 = 0. \n'

        # Находим потенциалы
        while true(u, v):
            for line in range(c_line):
                for col in range(c_col):
                    if '[' in matrix[line, col]:
                        #         print(line, col)
                        if u[line] != -9999 and v[col] == -9999:
                            v[col] = value(matrix, line, col) - u[line]
                        elif v[col] != -9999 and u[line] == -9999:
                            u[line] = value(matrix, line, col) - v[col]

        strResult+=print_matrix(matrix, u, v, 0)+'\n'

        # Находим оценки свободных клеток
        for i in range(c_line):
            for j in range(c_col):
                if '[' not in matrix[i, j]:
                    rating = u[i] + v[j] - int(matrix[i, j])
                    if rating > 0:
                        ratings.append(rating)
                        indexes.append([i, j])

        # Нашли оптимальный план, конец !!!
        if not len(ratings):
            strResult+= '\n\n\n'
            strResult+=print_matrix(matrix, a, b, summ)+'\n'
            strResult+= 'Опорный план является оптимальным, так все оценки свободных клеток удовлетворяют условию ui + vj ≤ cij.\n' + \
                f'Минимальные затраты составят: {getF(matrix)} \n'
            break

        strResult+='Опорный план не является оптимальным, так как существуют оценки свободных клеток, для которых'+\
        ' ui + vj > cij \n'

        cell = indexes[np.argmax(ratings)]
        strResult+= f'Выбираем максимальную оценку свободной клетки {cell}: {ratings[np.argmax(ratings)]}\n' + \
              f'Для этого в перспективную клетку {cell} поставим знак «+», а в остальных вершинах\nмногоугольника чередующиеся знаки «-», «+», «-». \n'

        road = getRoad(matrix, cell, '').reshape((-1, 2))  # Получаем цикл
        corners = getСorners(road).astype(int)  # Получаем углы
        val = getMinAorB(corners, matrix)  # Получаем максимальное количество для изменения матрицы

        # Закрашиваем ячейки цикла
        tempMatrix = matrix.copy()
        for vect in road:
            y, x = vect.astype(int)
            tempMatrix[y, x] = '.' + tempMatrix[y, x]
        df = pd.DataFrame(tempMatrix)
        styled = (df.style
                  .applymap(lambda v: 'background-color: %s' % 'green' if ('.' in v) else '')).render()
        # display(styled)  # Вывод цикла
        strResult+= styled+'\n'

        # Меняем кол-во элементов для перевозки
        matrix[corners[0, 0], corners[0, 1]] += f"[{val}]"
        for i in range(1, len(corners)):
            old_value = matrix[corners[i, 0], corners[i, 1]]
            C, count = old_value.split('[')
            count = float(count[:-1])
            if not i % 2:
                count += val
            else:
                count -= val

            # Меняем значение на новое
            matrix[corners[i, 0], corners[i, 1]] = f'{C}' + \
                                                   (f"[{count}]" if count > 0 or getCountMN(matrix) == (
                                                               sum(matrix.shape) - 1) else "")
    return  strResult