import numpy as np
import pandas as pd

from PrepareTable import StrToNPMatrix
from modules import *
matrixOne= '-1;2;-3;0#5;4;-2;1#-8;11;-10;-5'
matrixTwo= '-9;3#6;20#7;0#12;-4'

matrixOne = StrToNPMatrix(matrixOne, ';', '#').astype(float)
matrixTwo = StrToNPMatrix(matrixTwo, ';', '#').astype(float)

print(matrixOne.shape, matrixTwo.shape, matrixOne.shape[1] != matrixTwo.shape[0])

print(np.matmul(matrixOne, matrixTwo))