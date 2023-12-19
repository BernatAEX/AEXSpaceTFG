#!/usr/bin/env python3

import asyncio
from mavsdk import System
import paho.mqtt.publish as publish
import re

topic = "Aexspace/"
hostname = "172.21.71.16"
port = 1883

async def run():
    # Init the drone
    drone = System()
    await drone.connect(system_address="udp://:14540")
    # Start the tasks
    asyncio.ensure_future(Send_MQTT_Data(drone))

    while True:
        await asyncio.sleep(1)

async def print_all_info(drone):
    while True:
        lat, long, alt = await get_position(drone)
        Vnorth, Veast, Vdown = await get_velocities(drone)
        heading = await get_heading(drone)

        print(f"{round(lat, 6)} {round(long, 7)} {round(alt, 1)} {round(Vnorth, 1)} {round(Veast, 1)} {round(Vdown, 1)} {round(heading, 1)}")
        await asyncio.sleep(0.5)

async def Send_MQTT_Data(drone):
    while True:
        lat, long, alt = await get_position(drone)
        Vnorth, Veast, Vdown = await get_velocities(drone)
        heading = await get_heading(drone)

        telem = f"{round(lat, 6)} {round(long, 7)} {round(alt, 1)} {round(Vnorth, 1)} {round(Veast, 1)} {round(Vdown, 1)} {round(heading, 1)}"
        message = re.sub(r'\.', ',', telem)

        publish.single(topic, str(message), hostname=hostname, port=port)
        await asyncio.sleep(1/30)


async def get_position(drone):
    async for position in drone.telemetry.position():
        return position.latitude_deg, position.longitude_deg, position.relative_altitude_m

async def get_velocities(drone):
    async for velocity in drone.telemetry.velocity_ned():
        return velocity.north_m_s, velocity.east_m_s, velocity.down_m_s

async def get_heading(drone):
    async for attitude_data in drone.telemetry.attitude_euler():
        return attitude_data.yaw_deg

if __name__ == "__main__":
    # Start the main function
    asyncio.run(run())
