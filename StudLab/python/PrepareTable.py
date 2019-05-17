# -*- coding: utf-8 -*-
from modules import *

def StrToNPMatrix(string_matrix, sep =';', endRow ='#'):
    if type(string_matrix) == type(None):
        return  None
    rows = string_matrix.split(endRow)
    matrixStrElements = []
    for row in rows:
        matrixStrElements.append(row.split(sep))
    return np.array(matrixStrElements).astype('U21')
def StrToVectorNP(string_matrix, sep =';'):
    return np.array([float(i) for i in string_matrix.split(sep)])

