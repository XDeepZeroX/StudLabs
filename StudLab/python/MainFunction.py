from Transport.NorthwestCorner import *
from Transport.MSE import *
import argparse

parser = argparse.ArgumentParser()
parser.add_argument("--name_func", type = str , default=None)
# parser.add_argument("--matrix", type = str , default=None )
# parser.add_argument("--avector", type = str, default=None)
# parser.add_argument("--bvector", type = str, default=None)
# parser.add_argument("--sep", type = str, default = ';' )
# parser.add_argument("--endRow", type = str, default = '#' )

FUNCTIONS = {
    #Транспортная задача
    "NorthwestCorner": NorthwestCorner, #Метод Северо-Западного угла
    "MSE": MSE, # Метод наименьшего элемента
}
def MAIN(data):
    args, unknown  = parser.parse_known_args(data.split())
    name_func = args.name_func
    # matrix = args.matrix
    # avector = args.avector
    # bvector = args.bvector
    # sep = args.sep
    # endRow = args.endRow
    try:
        return FUNCTIONS[name_func](data)
    except KeyError:
        print("Кто то пытался подделать ТРАНСПОРТНУЮ функцию")
        return "<h1>У вас нет прав на это действие !</h1>"









