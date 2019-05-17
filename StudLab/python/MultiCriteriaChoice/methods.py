from modules import *


def print_matrix(matrix, label_colums=None, isPrint = False):
    '''
      Вывод матрицы
      ...
      Attributes
      ----------
      matrix : ndarray
          Матрица
      label_colums : ndarray
          Название столбцов
    '''
    df = pd.DataFrame(matrix,
                      index=[f"E{i}" for i in range(1, matrix.shape[0] + 1)],
                      columns=label_colums)
    if isPrint:
        display(df)
    return df.to_html()
def concatenate(matrix, column):
    '''
      Соединение матриц
      ...
      Attributes
      ----------
      matrix : ndarray
          Матрица
      column : ndarray
          Столбец
    '''
    return np.concatenate((matrix, np.reshape(column, (-1, 1))), axis=1)
def print_name_krit(s):
    n = 70
    return "_" * n + '\n\n<br />' + ' ' * n + '\n\n' + ' ' * int((n - len(s)) / 2) + s + \
          ' ' * int((n - len(s)) / 2) + '\n\n<br>' + '_' * n +'<br /><br />'
def MM(matrix, isPrint=True):
    '''
      Минимаксный критерий
      ...
      Attributes
      ----------
      matrix : ndarray
          Матрица
      isPrint : Bool, optional
          Выводить название критерия
    '''
    res_matrix = matrix.copy().astype(str)
    minsE = []
    for i in matrix:
        minsE.append(min(i))
    res_matrix = concatenate(res_matrix, minsE)
    res = np.zeros((matrix.shape[0])).astype(str)
    res[:] = ''
    res[np.argmax(minsE)] = str(minsE[np.argmax(minsE)]) + ' <--'
    res_matrix = concatenate(res_matrix, res)

    if isPrint:
        s = 'Минимаксный критерий'
        return print_name_krit(s) +  print_matrix(res_matrix, [f"F{i}" for i in range(1, matrix.shape[1] + 1)] + ["min Eij", 'max Eir'])
def BL(matrix, q=None, isPrint=True):
    '''
      Критерий Байеса-Лапласа
      ...
      Attributes
      ----------
      matrix : ndarray
          Матрица
      q : ndarray
          Матрица вероятностей
      isPrint : Bool, optional
          Выводить название критерия
    '''
    labels_y = [f"F{i}" for i in range(1, matrix.shape[1] + 1)]
    res_matrix = matrix.copy().astype(str)
    matrix = np.array(matrix).astype(float)

    if q == None:
        q = np.zeros(matrix.shape[1])
        q[:] = 1 / matrix.shape[1]

    '''
      Умножаем каждый элемент на Q для этого столбца
    '''
    EiiQi = []
    for i in range(q.shape[0]):
        labels_y.append(f'Eii*Q{i+1}')
        column = matrix[:, i] * q[i]
        EiiQi.append(column)
        res_matrix = concatenate(res_matrix, column)
    '''
      Считаем Eir - сумма(Eij*Qj)
    '''
    Eir = []
    EiiQi = np.array(EiiQi)
    for i in range(EiiQi.shape[1]):
        Eir.append(sum(EiiQi[:, i]))
    labels_y.append('Eir = sum(Eii*Qi)')
    res_matrix = concatenate(res_matrix, Eir)

    '''
      Находим ответ - max(сумма(Eij*Qj))
    '''
    res = np.zeros(matrix.shape[0]).astype(str)
    res[:] = ''
    res[np.argmax(Eir)] = str(Eir[np.argmax(Eir)]) + ' <--'
    res_matrix = concatenate(res_matrix, res)
    labels_y.append('max Eir')

    if isPrint:
        s = 'Критерий Байеса-Лапласа'
        return print_name_krit(s) + print_matrix(res_matrix, labels_y)  # Вывод результатов
def S(matrix, isPrint=True):
    '''
      Критерий Севиджа
      ...
      Attributes
      ----------
      matrix : ndarray
          Матрица
      isPrint : Bool, optional
          Выводить название критерия
    '''
    labels_y = [f"F{i}" for i in range(1, matrix.shape[1] + 1)]
    res_matrix = matrix.copy().astype(str)
    matrix = np.array(matrix).astype(float)

    if isPrint:
        s = 'Критерий Севиджа'
        print_name_krit(s)
    '''
      Считаем матрицу остатков
    '''
    ost = []
    for i in range(matrix.shape[1]):
        column = matrix[:, i] * -1.0
        MAX = max(matrix[:, i])
        column += MAX
        ost.append(column)
        res_matrix = concatenate(res_matrix, column)
        labels_y.append(f'Ei{i+1}-max(Ei{i+1})')

    '''
      Находим Eir  - max(Aij), где A-матрица остатков
    '''
    Eir = []
    ost = np.array(ost)
    for i in range(ost.shape[1]):
        Eir.append(max(ost[:, i]))

    labels_y.append('Eir = max(Aij)')
    res_matrix = concatenate(res_matrix, Eir)
    '''
      Находим ответ - min(Eir)
    '''
    res = np.zeros(matrix.shape[0]).astype(str)
    res[:] = ''
    res[np.argmin(Eir)] = str(Eir[np.argmin(Eir)]) + ' <--'
    res_matrix = concatenate(res_matrix, res)
    labels_y.append('min(Eir)')
    if isPrint:
        s = 'Критерий Байеса-Лапласа'
        return print_name_krit(s) +  print_matrix(res_matrix, labels_y)
def HW(matrix, C=0.5, isPrint=True):
    '''
      Критерий Гурвица
      ...
      Attributes
      ----------
      matrix : ndarray
          Матрица
      C : float
        Степень пессимизма
        по умолчанию = 0.5
      isPrint : Bool, optional
          Выводить название критерия
    '''
    labels_y = [f"F{i}" for i in range(1, matrix.shape[1] + 1)]
    res_matrix = matrix.copy().astype(str)
    matrix = np.array(matrix).astype(float)

    if isPrint:
        s = 'Критерий Гурвица'
        print_name_krit(s)
    '''
      Считаем:
      1 - c*min(Eij)
      2 - (1-c)*max(Eij)
      3 - Eir = c*min(Eij) + (1-c)*max(Eij)
    '''
    CminEij, CmaxEij, Eir = [], [], []
    for i in matrix:
        CminEij.append(min(i) * C)
        CmaxEij.append(max(i) * (1 - C))
        Eir.append(CminEij[-1] + CmaxEij[-1])

    labels_y += ['C*minEij', '(1-c)*maxEij', 'Eir']
    res_matrix = concatenate(res_matrix, CminEij)
    res_matrix = concatenate(res_matrix, CmaxEij)
    res_matrix = concatenate(res_matrix, Eir)
    '''
      Находим ответ - max(Eir)
    '''
    res = np.zeros(matrix.shape[0]).astype(str)
    res[:] = ''
    res[np.argmax(Eir)] = str(Eir[np.argmax(Eir)]) + ' <--'
    res_matrix = concatenate(res_matrix, res)
    labels_y.append('max(Eir)')
    if isPrint:
        s = 'Критерий Байеса-Лапласа'
        return print_name_krit(s) +  print_matrix(res_matrix, labels_y)
def HL(matrix, q=None, v=0.5, isPrint=True):
    '''
      Критерий Ходжа-Лемона
      ...
      Attributes
      ----------
      matrix : ndarray
          Матрица
      q : ndarray
          Матрица вероятностей
      v : float
        Коэффициент достоверности информации,
        по умолчанию = 0.5
      isPrint : Bool, optional
          Выводить название критерия
    '''
    labels_y = [f"F{i}" for i in range(1, matrix.shape[1] + 1)]
    res_matrix = matrix.copy().astype(str)
    matrix = np.array(matrix).astype(float)

    if isPrint:
        s = 'Критерий Ходжа-Лемона'
        print_name_krit(s)
    if q == None:
        q = np.zeros(matrix.shape[1])
        q[:] = 1 / matrix.shape[1]
    '''
      Считаем: 
      1 - сумма(Eij*Qj)
      2 - V*сумма(Eij*Qj)
      3 - min(Eij)
      4 - (1-V)*min(Eij)
      5 - Eir
    '''
    SUM, V, MIN, VMIN, Eir = [], [], [], [], []
    for row in matrix:
        # сумма(Eij*Qj)
        summ = 0
        for j in range(len(row)):
            summ += row[j] * q[j]
        SUM.append(summ)
        # V*сумма(Eij*Qj)
        V.append(summ * v)

        # min(Eij)
        MIN.append(min(row))
        # (1-V)*min(Eij)
        VMIN.append((1 - v) * MIN[-1])

        # Eir
        Eir.append(V[-1] + VMIN[-1])

    labels_y += ['сумма(Eij*Qj)', 'V*сумма(Eij*Qj)', 'min(Eij)', '(1-V)*min(Eij)',
                 'Eir=V*сумма(Eij*Qj) + (1-V)*min(Eij)']
    res_matrix = concatenate(res_matrix, SUM)
    res_matrix = concatenate(res_matrix, V)
    res_matrix = concatenate(res_matrix, MIN)
    res_matrix = concatenate(res_matrix, VMIN)
    res_matrix = concatenate(res_matrix, Eir)

    '''
      Находим ответ - max(Eir)
    '''
    res = np.zeros(matrix.shape[0]).astype(str)
    res[:] = ''
    res[np.argmax(Eir)] = str(Eir[np.argmax(Eir)]) + ' <--'
    res_matrix = concatenate(res_matrix, res)
    labels_y.append('max(Eir)')

    if isPrint:
        s = 'Критерий Байеса-Лапласа'
        return print_name_krit(s) +  print_matrix(res_matrix, labels_y)
def G(matrix, q=None, isPrint=True):
    '''
      Критерий Гермейера
      ...
      Attributes
      ----------
      matrix : ndarray
          Матрица
      q : ndarray
          Матрица вероятностей
      isPrint : Bool, optional
          Выводить название критерия
    '''
    labels_y = [f"F{i}" for i in range(1, matrix.shape[1] + 1)]
    res_matrix = matrix.copy().astype(str)
    matrix = np.array(matrix).astype(float)

    if isPrint:
        s = 'Критерий Гермейера'
        print_name_krit(s)
    if type(q) == type(None):
        q = np.zeros(matrix.shape[1])
        q[:] = 1 / matrix.shape[1]
    '''
      Считаем матрицу остатков
      Aij = Eij - a , где a - это значение на единицу больше,
      чем максимальный элемент исходной матрицы
    '''
    a = np.max(matrix)
    # print("Max element: ", a)
    ost = matrix.copy()
    # Делаем её отрицательной
    if a > 0:
        for i in range(ost.shape[0]):
            for j in range(ost.shape[1]):
                ost[i, j] -= a + 1
    # Eij * Qj
    for i in range(ost.shape[0]):
        for j in range(ost.shape[1]):
            ost[i, j] *= q[j]
    for col in range(ost.shape[1]):
        res_matrix = concatenate(res_matrix, ost[:, col])
        labels_y.append(f'F\'{col+1} [F\' = (Eij-|max(Eij)|-1)*Qij]')

    '''
      Находим минималье элементы 
      в строках матрицы остатков
    '''
    Eir = []
    for row in ost:
        Eir.append(min(row))
    labels_y.append('Eir = min(E\'ij)')
    res_matrix = concatenate(res_matrix, Eir)
    '''
      Находим ответ - max(Eir)
    '''
    res = np.zeros(matrix.shape[0]).astype(str)
    res[:] = ''
    res[np.argmax(Eir)] = str(Eir[np.argmax(Eir)]) + ' <--'
    res_matrix = concatenate(res_matrix, res)
    labels_y.append('max(Eir)')
    if isPrint:
        s = 'Критерий Байеса-Лапласа'
        return print_name_krit(s) +  print_matrix(res_matrix, labels_y)
def P(matrix, isPrint=True):
    '''
      Критерий Произведения
      ...
      Attributes
      ----------
      matrix : ndarray
          Матрица
      isPrint : Bool, optional
          Выводить название критерия
    '''
    labels_y = [f"F{i}" for i in range(1, matrix.shape[1] + 1)]
    res_matrix = matrix.copy().astype(str)
    matrix = np.array(matrix).astype(float)

    if isPrint:
        s = 'Критерий Произведения'
        print_name_krit(s)
    '''
      Корректируем матрицу
      a = min(|Aij|)+1
    '''
    a = abs(np.min(matrix)) + 1
    matrix[matrix <= 0] += a
    for i in range(matrix.shape[1]):
        labels_y.append(f'F\'{i+1} [Eij-(min(|Aij|)+1)]')
        res_matrix = concatenate(res_matrix, matrix[:, i])

    '''
      Перемножаем элементы строки
    '''
    Eir = []
    for row in matrix:
        Eir.append(reduce(lambda previous, NEXT: previous * NEXT, row))
    labels_y.append('Eir')
    res_matrix = concatenate(res_matrix, Eir)
    '''
      Находим ответ - max(Eir)
    '''
    res = np.zeros(matrix.shape[0]).astype(str)
    res[:] = ''
    res[np.argmax(Eir)] = str(Eir[np.argmax(Eir)]) + ' <--'
    res_matrix = concatenate(res_matrix, res)
    labels_y.append('max(Eir)')

    if isPrint:
        s = 'Критерий Байеса-Лапласа'
        return print_name_krit(s) +  print_matrix(res_matrix, labels_y)
