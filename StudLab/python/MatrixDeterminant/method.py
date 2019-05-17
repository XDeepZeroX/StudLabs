def GetDeterminant(matrix):
    resStr = []
    if(matrix.shape[0] != matrix.shape[1]):
        return f"<h2>Матрица не квадратная, попробуйте снова</h2>"

    if(matrix.shape[0] == 2):

