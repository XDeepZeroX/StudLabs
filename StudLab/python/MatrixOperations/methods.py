import pandas as pd
import numpy as np


def MUL(matrixOne, matrixTwo = None, value = None):
    matrixOne = matrixOne.astype(float)
    if type(matrixTwo) != type(None):
        matrixTwo = matrixTwo.astype(float)
        try:
            result =  np.matmul(matrixOne, matrixTwo)
            return pd.DataFrame(result).to_html(index=False,header=False)
        except ValueError:
            return "<h2>Неверный размер матриц, попробуйте снова.</h2>"
    elif value != None:
        result =  matrixOne*value
        return pd.DataFrame(result).to_html(index=False,header=False)
    return  "<h3>Операция завершилась с ошибкой, попробуйте снова.</h3>"

def SUB(matrixOne, matrixTwo = None, value = None):

    matrixOne = matrixOne.astype(float)
    if type(matrixTwo) != type(None):
        matrixTwo = matrixTwo.astype(float)

        if matrixOne.shape != matrixTwo.shape:
            return  '<h2>Размеры матриц не совпадают, попробуйте снова.</h2>'
        result = matrixOne - matrixTwo
        return pd.DataFrame(result).to_html(index=False,header=False)
        # return result
    elif value != None:
        result = matrixOne - value
        return pd.DataFrame(result).to_html(index=False,header=False)

def SUM(matrixOne, matrixTwo = None, value = None):
    matrixOne = matrixOne.astype(float)
    if type(matrixTwo) != type(None):
        matrixTwo = matrixTwo.astype(float)

        if matrixOne.shape != matrixTwo.shape:
            return  '<h2>Размеры матриц не совпадают, попробуйте снова.</h2>'
        result = matrixOne + matrixTwo
        return pd.DataFrame(result).to_html(index=False,header=False)
        # return result
    elif value != None:
        result = matrixOne + value
        return pd.DataFrame(result).to_html(index=False,header=False)