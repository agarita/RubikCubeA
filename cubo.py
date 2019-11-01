import random
import enum

cubo = [0, 1, 2, 3, 4, 5, 6, 7, 8,  # Cara frontal
        9, 10, 11, 12, 13, 14, 15, 16, 17,  # Cara arriba
        18, 19, 20, 21, 22, 23, 24, 25, 26,  # Cara atras
        27, 28, 29, 30, 31, 32, 33, 34, 35,  # Cara abajo
        36, 37, 38, 39, 40, 41, 42, 43, 44,  # Cara izquierda
        45, 46, 47, 48, 49, 50, 51, 52, 53]  # Cara derecha

esquinas =

bordes = [[1, 16], [3, 43], [5, 52], [7, 28],
          [10, 25], [12, 41], [14, 48], [19, 34],
          [21, 37], [23, 46], [30, 39], [32, 50]]

rotIzq = [2, 4, 6, -2, 0, 2, -6, -4, -2]
rotDer = [6, 2, -2, 4, 0, -4, 2, -2, -6]

"""
    5
    3
  2 0 1
    4
"""
class cara(enum.Enum):
    F = 0
    U = 1
    B = 2
    D = 3
    L = 4
    R = 5

class color(enum.Enum):
    azul = 0
    rojo = 1
    amarillo = 2
    naranja = 3
    verde = 4
    blanco = 5

def imprimirCubo():
    print("\t ", cubo[27:30])
    print("\t ", cubo[30:33])
    print("\t ", cubo[33:36])
    print("\t ", cubo[18:21])
    print("\t ", cubo[21:24])
    print("\t ", cubo[24:27])
    print(cubo[36:39], cubo[9:12], cubo[45:48])
    print(cubo[39:42], cubo[12:15], cubo[48:51])
    print(cubo[42:45], cubo[15:18], cubo[51:54])
    print("\t ", cubo[0:3])
    print("\t ", cubo[3:6])
    print("\t ", cubo[6:9])

"""
def revolverCubo():
    disponible= [8, 8, 8, 8, 8, 8]
    for pieza in range(len(cubo)):
        if (pieza%9==4):
            continue
        else:
            rand = random.randint(0,5)
            while(disponible[rand] == 0):
                rand = random.randint(0,5)
            cubo[pieza] = rand
            disponible[rand] -= 1
"""

def rotarDer(cara):
    anterior = [0, 0, 0, 0, 0, 0, 0, 0, 0]
    for i in range(9):
        celda = 9*cara+i
        anterior[i] = cubo[celda]
    for i in range(9):
        celda = 9*cara+i
        cubo[celda] = anterior[i+rotDer[i]]

def rotarIzq(cara):
    anterior = [0, 0, 0, 0, 0, 0, 0, 0, 0]
    for i in range(9):
        celda = 9*cara+i
        anterior[i] = cubo[celda]
    for i in range(9):
        celda = 9*cara+i
        cubo[celda] = anterior[i+rotIzq[i]]

# X - rotar la cara X 90° según las agujas del reloj
# X* - rotar la cara X 90° contra las agujas del reloj
# X2 - rotar la cara X 180°
def movimiento(cara, extra=''):
    if extra == '*':
        rotarIzq(cara)
    else:
        if extra == '2':
            rotarDer(cara)
        rotarDer(cara)
