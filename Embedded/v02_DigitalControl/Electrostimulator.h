#ifndef Electrostimulator_h
#define Electrostimulator_h

#include "Arduino.h"

#define BUF_LENGTH 64       // Buffer para os comandos vindos da serial
#define BUF_LENGTH_SMALL 5  // Buffer para receber comandos menores da serial

#define PWM_CHANNEL 0         // Canal de pwm utilizado (0 a 15)
#define PWM_RESOLUTION 8   // Resolução de duty cicle (1 a 16 bits)
#define PWM_FREQ 80000      // Oscilator frequency
#define PWM_DUTYCICLE 127   // 50% duty cicle

#define ADC_RESOLUTION 8
#define ADC_AVG 3

#define TICKS_INT 100

#define SQUARE_WAVE_RES 255 // Antes era 50
#define SPIKE_DATA_LENGTH 8000

#define CORRENTE_MAX 5000 // Valor máximo de corrente, em uA

#define ON 1
#define OFF 0

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

class Electrostimulator
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

    uint8_t dac_pin;
    uint8_t osc_pin;
    uint8_t sd_pin;
    uint8_t adc_curr_pin;
    uint8_t adc_sp_pin;
    uint8_t switch_pin;


    uint32_t period = 100000; // (em 0,01 ns)
    uint32_t duty = 0;  // %

    uint16_t valor_DAC = 0;
    uint16_t set_current = 0;

    const uint16_t std_resolution = pow(2, ADC_RESOLUTION) - 1;

    bool ondaQ[SQUARE_WAVE_RES] = {0}; 

    float step;

    uint8_t buf_spk[3] = {'f'};
    uint8_t spk_on = 127;
    uint8_t spk_off = 127;
    volatile bool spike_data[SPIKE_DATA_LENGTH] = {0};
    uint16_t total_spikes = 0;
    uint8_t intrrpts_spk_on = 0; 
    bool spike_off = true;

    
    //Variáveis do timer
    
    volatile uint64_t nTicks = 0;
    
    uint8_t pino_dac = 0;
    volatile uint32_t countTotal = 0;  // Controla o tempo total de estimulação
    volatile uint8_t waveIndex = 0;   // Controla o index do vetor que guarda a onda 
    volatile uint8_t DAC_out = 0;     // Valor de saída do DAC usado pela timerISR

    volatile bool interrompeu = false;
    portMUX_TYPE timerMux = portMUX_INITIALIZER_UNLOCKED;
    hw_timer_t * timer_Onda = NULL;
    hw_timer_t * timer_Int = NULL;

    volatile bool timer_on = false;

    // Variáveis do controle PI
    const float KInt = 0.1, KProp = 0.2;
    const uint8_t iKInt = (uint8_t)(1/KInt);
    const uint8_t iKProp = (uint8_t)(1/KProp);
    int16_t mInt = 0, mProp = 0;
    uint8_t controle = 0;


    const uint8_t dacMin = 0, dacMax = 255;

    void calc_controle();
    int16_t readADC(uint8_t adc_pin);
    uint8_t setBound(int16_t val);
    

    void setupOndaQuad();
    void setupSpike();
    void stimulatorState(bool turnON);
    void enableTimers(bool enable);
    

  public:
    Electrostimulator(uint8_t _dac_pin, uint8_t _osc_pin, uint8_t _sd_pin, uint8_t _adc_curr_pin, uint8_t _adc_sp_pin, uint8_t _switch_pin);

    void begin();
    void checkSerial(estados *estadoAtual);
    void checkSerial_Fast(estados *estadoAtual);
    void geraOndaQuad(estados *estadoAtual);
    void geraSpike(estados *estadoAtual);
    void geraFormaDeOnda();

    void configTimer(hw_timer_t * _timer, hw_timer_t * _timer_int);
    void IRQtimer();
    void IRQ_timer_int();

    void interromp(volatile bool * _int, portMUX_TYPE * _timerMux);

    
    
};












#endif
