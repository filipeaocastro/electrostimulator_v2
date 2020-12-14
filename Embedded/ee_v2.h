#ifndef Eletroestimulador_h
#define Eletroestimulador_h

#include "Arduino.h"

#define BUF_LENGTH 64       // Buffer para os comandos vindos da serial
#define BUF_LENGTH_SMALL 5  // Buffer para receber comandos menores da serial

#define CANAL_PWM 0         // Canal de pwm utilizado (0 a 15)
#define PWM_RESOLUTION 12   // Resolução (1 a 16 bits) (Quanto maior a resolução, menor a freq máxima)

#define SQUARE_WAVE_RES 50
#define SPIKE_DATA_LENGTH 8000

// Estados do sistema
typedef enum{
  STAND_BY, EE_SPK, EE_WF, EE_SQUARE
} estados;

typedef enum{
    QUADRADA, TRIANGULAR, SENOIDE, DENTESERRA, SPIKE
} ondas;

typedef enum{
    ANODICA, CATODICA, BIDIRECIONAL
} i_direction;

class Eletroestimulador
{
  private:
    ondas onda = QUADRADA;
    i_direction direcao_corrente = ANODICA;

    // Variáveis definidas pelo usuário
    uint32_t i_amp = 500;                //uA               
    uint32_t freq = 100000;                //Hz
    uint32_t bandwidth = 10;                 //us
    uint32_t total_duration = 10000;       //ms
    uint32_t random_bti_min = 500;       //ms
    uint32_t random_bti_max = 3000;      //ms


    uint32_t period = 100000; // (em 0,01 ns)
    uint32_t duty = 0;  // %

    uint16_t valor_DAC = 0;

    bool ondaQ[SQUARE_WAVE_RES] = {0}; 

    float step;

    uint8_t buf_spk[3] = {'f'};
    uint8_t spk_on = 127;
    uint8_t spk_off = 127;
    volatile bool spike_data[SPIKE_DATA_LENGTH] = {0};
    uint16_t total_spikes = 0;
    uint8_t intrrpts_spk_on = 0; 

    
    //Variáveis do timer
    
    volatile uint64_t nTicks = 0;
    
    uint8_t pino_dac = 0;
    volatile uint32_t countTotal = 0;  // Controla o tempo total de estimulação
    volatile uint8_t indexWave = 0;   // Controla o index do vetor que guarda a onda 
    volatile uint8_t DAC_out = 0;     // Valor de saída do DAC usado pela timerISR

    volatile bool interrompeu = false;
    portMUX_TYPE timerMux = portMUX_INITIALIZER_UNLOCKED;
    hw_timer_t * timer_INT = NULL;

    volatile bool timer_on = false;
    

    void setupOndaQuad();
    void setupSpike();
    

  public:
    Eletroestimulador(uint8_t _pino_dac);

    void checkSerial(estados *estadoAtual);
    void checkSerial_Fast(estados *estadoAtual);
    void geraOndaQuad(estados *estadoAtual);
    void geraSpike(estados *estadoAtual);
    void geraFormaDeOnda();

    void configTimer(hw_timer_t * _timer);
    void IRQtimer();

    void interromp(volatile bool * _int, portMUX_TYPE * _timerMux);

    
    
};












#endif
