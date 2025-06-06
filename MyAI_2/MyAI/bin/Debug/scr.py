import random
import numpy as np

# Укажите количество случайных цифр
num_digits = 100

dataset=np.eye(10,10)


        

print(dataset)
with open('datasetLearn_y.txt', 'w') as file:
    for i in dataset:
        file.write(f"{i}\n")

print("Случайные цифры записаны в файл random_digits.txt по одной в строке.")