#include <ESP8266WiFi.h>
#include <PubSubClient.h>

const char* ssid = "xxx";
const char* password = "xxx";
const char* mqtt_server = "xxx";

const char* mqttUser = "xxx";
const char* mqttPassword = "xxx";


int pwm = D3;
int dir = D5;
int dir2 = D6; 


int pwmb = D7;
int dirb = D8;
int dirb2 = D0; 

WiFiClient espClient;
PubSubClient client(espClient);
long lastMsg = 0;
char msg[50];
int value = 0;



void stopmovement() {
      digitalWrite(dir, LOW);
  digitalWrite(dir2, LOW);
  digitalWrite(dirb2, LOW);
  digitalWrite(dirb, LOW);
}

void forward(){
    digitalWrite(dir, HIGH);
  digitalWrite(dir2, LOW);
  digitalWrite(dirb2, HIGH);
  digitalWrite(dirb, LOW);
}

void backwards(){
 digitalWrite(dir2, HIGH);
  digitalWrite(dir, LOW);
  digitalWrite(dirb, HIGH);
  digitalWrite(dirb2, LOW);
 
}

void right(){
   digitalWrite(dir, HIGH);
  digitalWrite(dir2, LOW);
  digitalWrite(dirb, HIGH);
  digitalWrite(dirb2, LOW);
}

void left(){
    digitalWrite(dir2, HIGH);
  digitalWrite(dir, LOW);
  digitalWrite(dirb2, HIGH);
  digitalWrite(dirb, LOW);
}


void setup_wifi() {

  delay(10);
  // We start by connecting to a WiFi network
  Serial.println();
  Serial.print("Connecting to ");
  Serial.println(ssid);

  WiFi.begin(ssid, password);

  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }

  randomSeed(micros());

  Serial.println("");
  Serial.println("WiFi connected");
  Serial.println("IP address: ");
  Serial.println(WiFi.localIP());
}

void callback(char* topic, byte* payload, unsigned int length) {
  Serial.print("Message arrived [");
  Serial.print(topic);
  Serial.print("] ");
  Serial.print("Lengde: ");
  Serial.print(length);
  Serial.print(" ");
  for (int i = 0; i < length; i++) {
    Serial.print((char)payload[i]);
  }
  Serial.println();

  if((char)payload[1] == 'w'){
    Serial.println("Forover");
    forward();
  }
  if((char)payload[1] == 's'){
    Serial.println("bakover");
    backwards();
  }
  if((char)payload[1] == 'a'){
    Serial.println("venster");
    left();
  }
  if((char)payload[1] == 'd'){
    Serial.println("høgre");
    right();
  }
  if((char)payload[1] == 'q'){
    Serial.println("Stop");
    stopmovement();
  }
 
}

void reconnect() {
  // Loop until we're reconnected
  while (!client.connected()) {
    Serial.print("Attempting MQTT connection...");
    
    String clientId = "xxxx-";
    clientId += String(random(0xffff), HEX);
    // Attempt to connect
    if (client.connect(clientId.c_str(), mqttUser, mqttPassword)) {
      Serial.println("connected");
      client.subscribe("movement");
    } else {
      Serial.print("failed, rc=");
      Serial.print(client.state());
      Serial.println(" try again in 5 seconds");
      delay(5000);
    }
  }
}

void setup() {
  pinMode(BUILTIN_LED, OUTPUT);     // Initialize the BUILTIN_LED pin as an output
  Serial.begin(115200);
  setup_wifi();
  client.setServer(mqtt_server, 1883);
  client.setCallback(callback);

    pinMode(pwm, OUTPUT);
  pinMode(dir, OUTPUT);
  pinMode(dir2, OUTPUT);
  digitalWrite(pwm, HIGH);

  pinMode(pwmb, OUTPUT);
  pinMode(dirb, OUTPUT);
  pinMode(dirb2, OUTPUT);
  digitalWrite(pwmb, HIGH);
}

void loop() {

  if (!client.connected()) {
    reconnect();
  }
  client.loop();
}

