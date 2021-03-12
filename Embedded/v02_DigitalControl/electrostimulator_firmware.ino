 /**
  Autor: Filipe Oliveira
  Laboratório de Engenharia Biomédica (Biolab)
  Universidade Federal de Uberlândia

  Software embarcado para controle do eletroestimulador
**/

/*
  Por enquanto, apenas a função de estimulação por spikes será implementada. Posteriormente vou colocar as outras formas de onda.

  Neste software, é implementada uma máquina de estados para controle do eletroestimulador. Os estados são: STAND_BY, onde não há eletroestimulação e 
  ele aceita comandos via serial. Há também os estados começados por EE, que são os modos onde a estimulação está ativa, sendo eles EE_SPK (via spikes),
  EE_WF (via formas de onda) e EE_SQUARE (via onda quadrada).
  * STAND_BY: Nesse estado são definidos os parâmetros da estimulação que será feita, como amplitude, frequência, largura de pulso e duração. 
  * EE_SPK: EletroEstimulação por Spikes. Sempre que o software manda um comando '1' nesse estado, um spike acontece. A amplitude e duração dos mesmos
  são definidas em STAND_BY.
  * EE_WF: EletroEstimulação via formas de onda (Wave Forms). Aqui a estimulação ocorre por meio de onda senoidal, triangular ou dente de serra, onde
  a frequência e amplitude são definidas em STAND_BY.
  * EE_SQUARE: EletroEstimulação via onda quadrada. Aqui a estimulação ocorre por meio de onda quadrada, onde a amplitude, frequência e largura de pulso
  são definidas em STAND_BY.
*/

#include "Electrostimulator.h"

#define TiposDeOnda 3   // Necessary?
#define WAVE_RESOLUTION 100
#define DAC_REF_PIN 25       // Saída para a fonte de corrente
#define PWM_OSC_PIN 33  // PWM oscilator output
#define SD_PIN 32 // Shut Down pin - Active high
#define ADC_CURRENT_PIN 34
#define ADC_SETPOINT_PIN 35
#define SWITCH_PIN 15

estados estado = STAND_BY;

hw_timer_t * timer = NULL;
hw_timer_t * timer_int = NULL;
Electrostimulator stimulator(DAC_REF_PIN, PWM_OSC_PIN, SD_PIN, ADC_CURRENT_PIN, ADC_SETPOINT_PIN, SWITCH_PIN);

//static byte formasDeOnda[TiposDeOnda][QteAmostras] = {0};

bool mudouDAC = false;




/**
 Funciona assim:
 Fiz a matriz de [tipo de onda] por [amostra], onde a qte de amostra define o comprimento horizontal da onda.
 Dentro de cada valor de amostra vai estar o valor que vai jogar pro DAC, mas pegando apenas metade da resolução (0 a 128)
 Assim, eu posso escolher a direção de onda (uma direção de 0 a 1,65 V e outra de 1,65 a 3,3V) somando 128 aos valores,
 e caso eu queira usar toda a resolução (direção alternada de corrente) basta multiplicar por 2.
 */ 

void setup() 
{
  stimulator.begin();

  mudouDAC = false;
  Serial.begin(115200);
  

  timer = timerBegin(0, 8, true);  // inicia com o passo de 0.1 us
  timerAttachInterrupt(timer, &timerISR, true);

  timer_int = timerBegin(1, 80, true); // inicia com o passo de 1 us

  stimulator.configTimer(timer, timer_int);
  //stimulator.interromp(&_interrompeu, &timerMux);

}

void loop() 
{
  switch (estado)
  {
    case STAND_BY:
      stimulator.checkSerial(&estado);
      break;

    case EE_SPK:
      stimulator.geraSpike(&estado);
      break;

    case EE_WF:
      //geraFormaDeOnda();
      break;

    case EE_SQUARE:
      stimulator.geraOndaQuad(&estado);
      break;
  }

}

// void ondaSeno(int direcao, int amp)
// {
//   /*
//     ********** COLOCAR RECURSO DE ALTERAR A AMPLITUDE ***************
//   */

//   // Senoide
//   float step = 360.0 / (float)QteAmostras;
//   float stepSum = 0;
//   for(int i = 0; i < QteAmostras; i ++)
//   {
//     formasDeOnda[0][i] = (byte) (((sin((stepSum * PI) / 180.0) / 2) + 0.5) * 128);

//     stepSum += step;
//   }
// }

// void ondaTriang(int direcao, int amp)
// {
//   /*
//     ********** COLOCAR RECURSO DE ALTERAR A AMPLITUDE ***************
//   */

//   // Onda triangular
//   float step = 128 / (QteAmostras / 2);
//   float stepSum = 0;
//   for(int i = 0; i < (QteAmostras / 2); i ++)
//   {
//     formasDeOnda[1][i] =  stepSum;
//     stepSum += step;
//   }

//   stepSum = 128;
//   for(int i = (QteAmostras / 2); i < QteAmostras; i ++)
//   {
//     formasDeOnda[1][i] = stepSum;
//     stepSum -= step;
//   }
// }


void timerISR()
{
    stimulator.IRQtimer();
}
