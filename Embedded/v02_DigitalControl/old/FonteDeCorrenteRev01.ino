
#define ON 1
#define OFF 0
#define N_MED 5
#define RESOLUTION_AD          9     //Resoulção do AD
#define PWM_FREQ  80000
#define I_REF DAC1

const long TMP_INT = 5;
const int maxVal = 255;
const int minVal = 0;
float KI = 0.1, mInt = 0;
float KP = 0.2, mProp = 0;
long tmpAtual, tmpInt;
float erro, iAtual;

           
const int SW = 15;
const int SD_L293 = 32;
const int CURR = 34;
const int SET_POINT = 35;


//const int FREQ_PWM = A1;
const int PWM_TRAFO = 1;
const int PWM_PIN = 33;
const int PWM_RESOLUTION = 8;

bool CTL;

int vAD,vFreq;
long int tmr;
bool flag;

int controle = 0;

uint32_t status,duty,period;

void setup() 
{
  ledcSetup(PWM_TRAFO, PWM_FREQ, PWM_RESOLUTION);
  ledcAttachPin(PWM_PIN, PWM_TRAFO);
  pinMode(SW,INPUT_PULLUP);
  pinMode(SD_L293,OUTPUT);
  digitalWrite(SD_L293,OFF);
  ledcWrite(PWM_TRAFO,0);
  analogReadResolution(RESOLUTION_AD);
  Serial.begin(115200);
  CTL = 0;
  vAD = 0;
  tmpAtual = micros();
  tmpInt = tmpAtual + TMP_INT;
  mInt = 0;
  tmr = micros() + 500;
  flag = 0;
}

float regulaCorrente(float erro, float mInt)
{
  float mAux;
  long tmpAux;
  tmpAux = micros();
  if(tmpAux >= tmpInt)
  {
     // Serial.print("erro: ");Serial.print(erro);
      tmpInt = tmpAux + TMP_INT;
      mAux = KI*erro + mInt;
      if (mAux > maxVal) mAux = maxVal;   //Valor max. p/ mInt 
      if (mAux < minVal) mAux = minVal;   //Valor mín. p/ mInt
      return(mAux);
  }
  return(mInt);
}

float leSetPoint()
{
  float valor = 0;
  for (int i=0;i<N_MED;i++)
  {
    valor = analogRead(SET_POINT) + valor;
  }
  valor = valor/N_MED;  
 // Serial.print("oldSetPoint: ");Serial.print(oldSetPoint);
  return(valor);
}

float leCurr()
{
  float valor = 0;
  for (int i=0;i<N_MED;i++)
  {
    valor = analogRead(CURR) + valor;
  }
  valor = valor/N_MED;  
  return(valor);
}

void loop() 
{ 
  float setPoint;
  
 
 if((micros()>tmr)&&(!flag))
 {
  tmr = micros() + 500;
  tmpInt = micros() + TMP_INT;
  flag = 1;
  //mInt = 0;//controle = 0;//controle - controle*0.2;
 }
 if((micros()>tmr)&&(flag))
 {
  tmr = micros()+500;
  flag = 0;
 }
   //dacWrite(I_REF, controle);
//  flag = 1;
  if((!digitalRead(SW))&&(flag)) 
  {
    dacWrite(I_REF, controle);
    ledcWrite(PWM_TRAFO,128);
    digitalWrite(SD_L293,ON); //L293 running
    setPoint = leSetPoint();
    iAtual = leCurr();
    erro = setPoint - iAtual;
    mProp = KP*erro;
    mInt = regulaCorrente(erro, mInt);
    controle = (int) (mInt + mProp);
    if (controle > maxVal) controle = maxVal;   //Valor max. p/ mInt 
    if (controle < minVal) controle = minVal;   //Valor mín. p/ mInt
 //   Serial.print("controle: ");Serial.println(controle);
    //dacWrite(I_REF, controle);
    
  }
  else
  {
    ledcWrite(PWM_TRAFO,0);
    digitalWrite(SD_L293,OFF); 
    dacWrite(I_REF, 0);
    //controle = 0;
  }   

//  Serial.println(vAD);
//  delay(500);

}
