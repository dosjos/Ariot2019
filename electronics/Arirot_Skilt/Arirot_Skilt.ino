#include <Adafruit_NeoPixel.h>

int pin = 13;
const int trigPin = 11;
const int echoPin = 8;

long duration;
int distance;

Adafruit_NeoPixel strip = Adafruit_NeoPixel(6, pin, NEO_GRB + NEO_KHZ800);

void setup() {
  pinMode(trigPin, OUTPUT); 
  pinMode(echoPin, INPUT); 
  strip.begin();
  strip.setBrightness(128);
  strip.show();
}

void loop() {
  digitalWrite(trigPin, LOW);
  delayMicroseconds(2);
 
  digitalWrite(trigPin, HIGH);
  delayMicroseconds(10);
  digitalWrite(trigPin, LOW);
 
  duration = pulseIn(echoPin, HIGH);

  distance = duration * 0.034 / 2;

  if (distance <= 50) {
  
    for (int i = 0; i < 6; i++) {
      strip.setPixelColor(i, 255, 0, 0);
    }
    strip.show();
  } else {

    for (int i = 0; i < 6; i++) {
      strip.setPixelColor(i, 0, 255, 0);
    }
    strip.show();
  }
}
