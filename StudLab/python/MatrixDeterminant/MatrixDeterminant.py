import argparse
from PrepareTable import StrToNPMatrix

def MatrixDeterminant(data):
    parser = argparse.ArgumentParser()
    parser.add_argument("--matrix", type=str)
    parser.add_argument("--sep", type=str, default=';')
    parser.add_argument("--endRow", type=str, default='#')

    args, unknown = parser.parse_known_args(data.split())

    matrix = args.matrix
    sep = args.sep
    endRow = args.endRow

    matrix = StrToNPMatrix(matrix, sep, endRow)

    return ///
