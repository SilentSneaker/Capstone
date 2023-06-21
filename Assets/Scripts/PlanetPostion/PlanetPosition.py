import spiceypy
from datetime import datetime
import csv
import sys

object = sys.argv[1]
dateString = sys.argv[2]
if (object == 'Earth'):
    spiceypy.furnsh("Kernels/LeapSecond.tls")
    spiceypy.furnsh("Kernels/de432s.bsp")
    dateTime = datetime.datetime.strptime(dateString, "%Y-%m-%d-%H:%M:%S").date()
    dateTime = dateTime.strftime("%Y-%m-%d-%H:%M:%S")
    spiceDateTime = spiceypy.utc2et(dateTime)
    print(spiceDateTime)
    earthState, earthLightTime = spiceypy.spkgeo(targ=399,et= spiceDateTime, ref= "ECLIPJ2000", obs=10)
    with open('Earth.txt', 'w') as file:
        writer = csv.writer(file)
        writer.writerow(earthState)
    print(earthState)
