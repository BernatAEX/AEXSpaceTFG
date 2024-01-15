import random
from mavsdk import System
import paho.mqtt.publish as publish
import re
import time

topic = "Detections/"
hostname = "172.21.71.16"
port = 1883

lat_max, lon_max = 38.18893844215707, -122.43750356451336
lat_min, lon_min = 38.12916412596828, -122.47615929881682

#====== SONOMA RACEWAY ======#
x1, y1 = 38.16653384404812, -122.4624655813465
x2, y2 = 38.16245074639574, -122.45588528283304

#====== SWITZERLAND ======#
x1, y1 = 47.398948, 8.550376
x2, y2 = 47.397031, 8.542836

def run():
    while True:
        time.sleep(3600);
        
        lat = random.uniform(x2, x1)
        lon = random.uniform(y2, y1)
        
        type_detection = random.randint(0, 4)

        detection = f"{round(lat, 6)} {round(lon, 7)} {type_detection}"
        message = re.sub(r'\.', ',', detection)
        print(message)

        publish.single(topic, str(message), hostname=hostname, port=port)
        time.sleep(1)



if __name__ == "__main__":
    run()