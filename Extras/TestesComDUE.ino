#include <DuePWM.h>

#define PWM_FREQ1  60000
#define ON 1
#define OFF 0
#define N_MED 5
#define RESOLUTION_AD          8     //Resoulção do AD
#define STEPS_AD 255              

const int PWM_TRAFO = 6;
const int SW = 8;
const int SD_L293 = 53;
const int POTCURR = A0;
const int PWM_PIN = 2;
const long int deltaT = 5; //milissegundos
const int deltaV = 2; // valor na faixa de 0 a 1023
bool CTL;

int vAD;

uint32_t status,duty,period;

DuePWM pwm(PWM_FREQ1,PWM_FREQ1);

void setup() 
{
  pwm.setFreq1(PWM_FREQ1);
  pwm.pinFreq1(PWM_TRAFO); 
  pwm.pinDuty(PWM_TRAFO,0);
  pinMode(SW,INPUT_PULLUP);
  pinMode(SD_L293,OUTPUT);
  digitalWrite(SD_L293,OFF);
  analogReadResolution(RESOLUTION_AD);
  Serial.begin(115200);
  CTL = 0;
}

void loop() 
{ 
  if(!digitalRead(SW)) 
  {
    pwm.pinDuty(PWM_TRAFO,128);
    digitalWrite(SD_L293,ON);
  }
  else
  {
    pwm.pinDuty(PWM_TRAFO,0);
    digitalWrite(SD_L293,OFF); 
    CTL = 1;
  }   
  vAD = 0;
  for (int i=0;i<N_MED;i++)
  {
    vAD = analogRead(POTCURR) + vAD;
  }
  vAD = vAD/N_MED;
  if(!CTL) 
  {
    CTL = 1;
    analogWrite(DAC1,vAD);
    digitalWrite(SD_L293,ON); //Desativa SD
    //delayMicroseconds(5);    
  }
  else
  {
    CTL = 0;
    analogWrite(DAC1,0);    
    digitalWrite(SD_L293,OFF); //Ativa SD
    //delayMicroseconds(5);    
  }  
  delayMicroseconds(1000);
}
