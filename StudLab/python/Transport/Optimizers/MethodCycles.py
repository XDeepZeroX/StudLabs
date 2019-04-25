# -*- coding: utf-8 -*-
from Transport.transportModules import *

def MethodCycles(matrix, original_matrix, a, b):
    strResult=''
    new_matrix = matrix.copy()
    summ = sum(a)
    corners = []
    while (True):
        cell,temp_str = getEvaluationsCycles(new_matrix, original_matrix)  # Клетка с наименьшей, отрицательной стоимостью
        strResult+=temp_str+'\n'
        if cell == -1:  # Условие останова
            strResult+=f'Из приведенного расчета видно, что ни одна свободная клетка не имеет ' + \
                  'отрицательной оценки,\nследовательно, дальнейшее снижение целевой функции Fx невозможно,' + \
                  '\nпоскольку она достигла минимального значения.\nТаким образом, последний опорный план ' + \
                  f'является оптимальным.\nМинимальные затраты составят:  {getF( new_matrix)}\n'
            break
        road = getRoad(new_matrix, cell, '').reshape((-1, 2))  # Получаем цикл
        #   print(road)
        corners = getСorners(road).astype(int)  # Получаем углы
        #   print(corners)

        value = getMinAorB(corners, new_matrix)  # Получаем максимальное количество для изменения матрицы
        #     print(f'Min value: {value}')
        # print(new_matrix[corners[0]])
        # Меняем кол-во элементов для перевозки
        new_matrix[corners[0, 0], corners[0, 1]] += f"[{value}]"
        for i in range(1, len(corners)):
            old_value = new_matrix[corners[i, 0], corners[i, 1]]
            C, count = old_value.split('[')
            count = float(count[:-1])
            if not i % 2:
                count += value
            else:
                count -= value

            # Меняем значение на новое
            new_matrix[corners[i, 0], corners[i, 1]] = f'{C}' + \
                                                       (f"[{count}]" if count > 0 or getCountMN(new_matrix) == (
                                                                   sum(new_matrix.shape) - 1) else "")
        #       print(getCountMN(new_matrix), (sum(new_matrix.shape)-1))
        strResult+='Поскольку в исходном опорном плане рассматриваемой задачи имеется свободная клетка,\n' + \
              'c отрицательной оценкой, то для получения плана, обеспечивающего меньшее значение целевой функции,\n' + \
              'эту клетку следует занять возможно большей поставкой, не нарушающей при этом условий допустимости плана. \n'
        strResult+=print_matrix(new_matrix, a, b, summ)+'\n'
        strResult+=getF(new_matrix)+'\n'
    return strResult