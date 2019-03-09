#include <Servo.h>

int pin1 = 5, pin2 = 6, pin3 = 7;


int incomming = 0;
Servo servo;
void setup() {
 servo.attach(9);
 Serial.begin(9600);
 Serial.println("boot");
 pinMode(pin1, INPUT);
 pinMode(pin2, INPUT);
 pinMode(pin3, INPUT);
}



void loop() {
  if(digitalRead(pin1) == HIGH){
    servo.write(150);
  }
  if(digitalRead(pin2) == HIGH){
    servo.write(1);
  }
  if(digitalRead(pin3) == HIGH){
    servo.write(0);
    Serial.println("før");
    delay(250);
    Serial.println("etter");
    servo.write(75);
    delay(300);
  }
}
