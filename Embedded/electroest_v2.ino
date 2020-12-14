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

#include "Eletroestimulador.h"

#define TiposDeOnda 3
#define QteAmostras 100
#define PINO_SAIDA 25       // Saída para a fonte de corrente

estados estado = STAND_BY;

hw_timer_t * timer = NULL;
Eletroestimulador eest(PINO_SAIDA);

static byte formasDeOnda[TiposDeOnda][QteAmostras] = {0};

bool mudouDAC = false;

//portMUX_TYPE timerMux = portMUX_INITIALIZER_UNLOCKED;

//volatile bool _interrompeu = false;

//void IRAM_ATTR timerISR();



/**
 Funciona assim:
 Fiz a matriz de [tipo de onda] por [amostra], onde a qte de amostra define o comprimento horizontal da onda.
 Dentro de cada valor de amostra vai estar o valor que vai jogar pro DAC, mas pegando apenas metade da resolução (0 a 128)
 Assim, eu posso escolher a direção de onda (uma direção de 0 a 1,65 V e outra de 1,65 a 3,3V) somando 128 aos valores,
 e caso eu queira usar toda a resolução (direção alternada de corrente) basta multiplicar por 2.
 */ 

void setup() 
{
  dacWrite(PINO_SAIDA, 128);
  mudouDAC = false;
  Serial.begin(115200);
  pinMode(27, INPUT_PULLUP); // Botão

  timer = timerBegin(0, 8, true);  // inicia com o passo de 0.1 us
  timerAttachInterrupt(timer, &timerISR, true);
  eest.configTimer(timer);
  //eest.interromp(&_interrompeu, &timerMux);

}

void loop() 
{
  switch (estado)
  {
    case STAND_BY:
      if(mudouDAC)
      {
        dacWrite(PINO_SAIDA, 128);
        mudouDAC = false;
      }
      eest.checkSerial(&estado);
      break;

    case EE_SPK:
      mudouDAC = true;
      eest.geraSpike(&estado);
      break;

    case EE_WF:
      mudouDAC = true;
      //geraFormaDeOnda();
      break;

    case EE_SQUARE:
      mudouDAC = true;
      eest.geraOndaQuad(&estado);
      break;
  }

}

void ondaSeno(int direcao, int amp)
{
  /*
    ********** COLOCAR RECURSO DE ALTERAR A AMPLITUDE ***************
  */

  // Senoide
  float step = 360.0 / (float)QteAmostras;
  float stepSum = 0;
  for(int i = 0; i < QteAmostras; i ++)
  {
    formasDeOnda[0][i] = (byte) (((sin((stepSum * PI) / 180.0) / 2) + 0.5) * 128);

    stepSum += step;
  }
}

void ondaTriang(int direcao, int amp)
{
  /*
    ********** COLOCAR RECURSO DE ALTERAR A AMPLITUDE ***************
  */

  // Onda triangular
  float step = 128 / (QteAmostras / 2);
  float stepSum = 0;
  for(int i = 0; i < (QteAmostras / 2); i ++)
  {
    formasDeOnda[1][i] =  stepSum;
    stepSum += step;
  }

  stepSum = 128;
  for(int i = (QteAmostras / 2); i < QteAmostras; i ++)
  {
    formasDeOnda[1][i] = stepSum;
    stepSum -= step;
  }
}

void ondaQuad(int direcao, int amp, float pulseWidth)
{
  /*
    ********** COLOCAR RECURSO DE ALTERAR A AMPLITUDE ***************
  */
  int edge = int(pulseWidth * (float)QteAmostras / 100.0);
  // botar uma regra de 3 com o pulseW e a QteAmostras

  for(int i = 0; i < QteAmostras; i++)
  {
    if(i < edge)
      formasDeOnda[2][i] = 128;
    else
      formasDeOnda[2][i] = 0;
  }
}

void timerISR()
{
    eest.IRQtimer();

    //portENTER_CRITICAL_ISR(&timerMux);
   // _interrompeu = true;
    //portEXIT_CRITICAL_ISR(&timerMux);
}
