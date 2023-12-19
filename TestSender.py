import socket
import time
import random
import re

host, port = "0.0.0.0", 25001
#data = "41.414282712166354, 2.0195841960766345, 60.0"
NSpeed = 6.0
ESpeed = 0
DSpeed = 0
NEDSpeed = [NSpeed, ESpeed, DSpeed]
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

try:
    sock.connect((host, port))
    while True:
        random_numbers = [round(random.uniform(-5, 5), 6) for _ in range(3)]
        random_numbers += NEDSpeed
        random_numbers.append(random.randint(0, 360))
        
        #random_numbers_string = ' '.join(map(str, random_numbers))
        random_numbers_string = ' '.join(f'{number:.6f}' for number in random_numbers)
        formatted_data = re.sub(r'\.', ',', random_numbers_string)
        print("Random Numbers:", formatted_data)
        sock.sendall(formatted_data.encode("utf-8"))
        time.sleep(1)

except KeyboardInterrupt:
    print("Caught keyboard interrupt, exiting")
finally:
    sock.close()
    print("CLOSED")

