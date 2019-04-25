import socket
from MainFunction import *

PORT = 6666  #Порт
COUNT_CLIENTS = 99 #Макс кол-во соединений
COUNT_BYTES = 999999 #КОл-во передаваемых байтов за раз

sock = socket.socket()
sock.bind(("", PORT))
i = 0
cities = ''
while True:
    # try:
    print("Ожидаем клиента")
    sock.listen(COUNT_CLIENTS)
    conn, addr = sock.accept()
    print("Дождались клиента")

    data = ''
    #Получаем и преобразуем к нужной кодировке данные
    byte_data = conn.recv(COUNT_BYTES)
    data += byte_data.decode("utf-16")
    #Главная функция:
    # Парсит и выполеняет код над полученными данными
    res_str = MAIN(data)
    #Отправляем результат выполнения
    test = conn.sendall(str.encode(res_str),)

    conn.close()