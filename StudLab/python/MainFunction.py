from Transport.NorthwestCorner import *
from Transport.MSE import *
import argparse
from MultiCriteriaChoice.MultiCriteriaChoice import *
from MatrixOperations.MatrixOperations import *

parser = argparse.ArgumentParser()
parser.add_argument("--name_func", type = str , default=None)

FUNCTIONS = {
    #Транспортная задача
    "NorthwestCorner": NorthwestCorner, #Метод Северо-Западного угла
    "MSE": MSE, # Метод наименьшего элемента
    #Многокритериальный выбор
    "MultiCriteriaChoice": MultiCriteriaChoice,

    "MatrixOperations": MatrixOperations,
}
def MAIN(data):
    args, unknown  = parser.parse_known_args(data.split())
    name_func = args.name_func
    try:
        return FUNCTIONS[name_func](data)
    except KeyError:
        print('-'*60+"\nКто то пытался подделать название доступных функций функцию\n"+'-'*60)
        return "<h1>У вас нет прав на это действие !</h1>"









