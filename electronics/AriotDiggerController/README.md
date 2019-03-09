This code must be uploaded to an Wemos D1 mini R2.

It will run your digging direction from commands that it receives from MQTT, it sends commands to an arduino that controlls the actual servo.

In the top of the file, fill out the five fields with xxx.
Wifi name, Wifi Password, mqtt server IP, mqttUsername and mqtt password.

Conenct pin D5 to arduino pin 5, and D6 to 6 and D7 to 7.
Also remember to share ground between the devices.
