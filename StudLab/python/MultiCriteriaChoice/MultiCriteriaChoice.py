import argparse
from MultiCriteriaChoice.methods import *
from PrepareTable import StrToNPMatrix

METHODS = {
    "MM": MM,
    "BL":BL,
    "S":S,
    "HW":HW,
    "HL":HL,
    "G":G,
    "P":P,
}
def MultiCriteriaChoice(data):
    parser = argparse.ArgumentParser()
    parser.add_argument("--methods", type=str)
    parser.add_argument("--matrix", type=str)
    parser.add_argument("--sep", type=str, default=';')
    parser.add_argument("--endRow", type=str, default='#')

    args, unknown = parser.parse_known_args(data.split())

    matrix = args.matrix
    methods = args.methods
    sep = args.sep
    endRow = args.endRow

    matrix = StrToNPMatrix(matrix, sep, endRow)
    methods = methods.split(sep)


    resultMethods = []
    for nameMethod in methods:
        if nameMethod not in METHODS:
            print("Кто то пытался подделать метод многокритериального выбора\n"
                  f"{{{nameMethod}}}")
            continue
        resultMethods.append(METHODS[nameMethod](matrix))

    return  '\n\n\n<br>\n\n\n'.join(resultMethods)