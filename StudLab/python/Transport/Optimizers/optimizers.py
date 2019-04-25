# -*- coding: utf-8 -*-
from Transport.Optimizers.MethodPotenc import *
from Transport.Optimizers.MethodCycles import *
# Оптимизаторы для транспортной задачи
OPTIMIZERS = {
    "MethodCycles": MethodCycles,
    "MethodPotenc": MethodPotenc,
    "None": None
}

