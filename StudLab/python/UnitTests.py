from MainFunction import *

data1 = '--name_func=MatrixOperations --method=MUL --matrixOne=-1;2;-3;0#5;4;-2;1#-8;11;-10;-5 --matrixTwo=-9;3#6;20#7;0#12;-4'
data2 = '--name_func=MatrixOperations --method=MUL --matrixOne=-1;-2;1#5;9;-8 --value 3'
print(MAIN(data1))
print(MAIN(data2))
