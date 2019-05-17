import argparse
from MatrixOperations.methods import *
from PrepareTable import StrToNPMatrix

METHODS = {
    "MUL": MUL,
    "SUB": SUB,
    "SUM": SUM,
}
def MatrixOperations(data):
    parser = argparse.ArgumentParser()
    parser.add_argument("--method", type=str)
    parser.add_argument("--matrixOne", type=str)
    parser.add_argument("--matrixTwo", type=str, default=None)
    parser.add_argument("--value", type=float, default=None)
    parser.add_argument("--sep", type=str, default=';')
    parser.add_argument("--endRow", type=str, default='#')

    args, unknown = parser.parse_known_args(data.split())

    method = args.method
    sep = args.sep
    endRow = args.endRow

    matrixOne = StrToNPMatrix(args.matrixOne, sep, endRow)
    matrixTwo = StrToNPMatrix(args.matrixTwo, sep, endRow)
    value = args.value

    if method not in METHODS:
        print("Кто то пытался подделать метод ОПЕРАЦИЙ НАД МАТРИЦЕЙ\n"
              f"{{{method}}}")
        return "<h1>Ошибка</h1><br />В данный момент, выбранный вами метод не доступен, повторите попытку."
    return METHODS[method](matrixOne, matrixTwo, value)