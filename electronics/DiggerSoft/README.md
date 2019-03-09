This is a .Net core project that can be run on a Raspberry PI running Raspian.

In the file Ai.cs, fill out your URL and key to the cognitive services.

Then compile and upload to your raspi, when you run the code, it will controll a servo to turn the camera and take images in three directions. When it finds the objects it is looking for, it sends drive commands acordingly to the API.