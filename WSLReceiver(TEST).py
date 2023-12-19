"""# server.py (Ejecutar en Windows)

import subprocess
import time

# Configura y ejecuta el servidor Mosquitto en el puerto 1883
mosquitto_process = subprocess.Popen(["mosquitto", "-p", "1883"])

# Espera unos segundos para que el servidor se inicie completamente
time.sleep(2)

# Haces cualquier otra cosa que necesitas con el servidor ejecutándose

# Cuando hayas terminado, cierra el servidor Mosquitto
mosquitto_process.terminate()"""

import paho.mqtt.client as mqtt

def on_connect(client, userdata, flags, rc):
    print(f"Conectado con el código de resultado {rc}")
    client.subscribe("Aexspace/")

def on_message(client, userdata, msg):
    print(f"Datos recibidos: {msg.payload.decode()}")

client = mqtt.Client()
client.on_connect = on_connect
client.on_message = on_message

client.connect("172.21.71.16", 1883, 60)  # Utiliza la dirección IP de WSL, el puerto 1883

client.loop_forever()