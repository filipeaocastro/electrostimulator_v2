#ifndef Electrostimulator_h
#define Electrostimulator_h

#include "Arduino.h"

#define BUF_LENGTH 64       // Buffer para os comandos vindos da serial
#define BUF_LENGTH_SMALL 5  // Buffer para receber comandos menores da serial

#define PWM_CHANNEL 0         // Canal de pwm utilizado (0 a 15)
#define PWM_RESOLUTION 8   // Resolução de duty cicle (1 a 16 bits)
#define PWM_FREQ 80000      // Oscilator frequency
#define PWM_DUTYCICLE 127   // 50% duty cicle

#define ADC_RESOLUTION 9
#define REAL_ADC_RES (ADC_RESOLUTION - 1)

#define ADC_AVG 3

#define TICKS_INT 60 // Intervalo entre interrupções do timer de controle PI (us)
#define TICKS_TIMER_DAC 60 // (us)

#define SQUARE_WAVE_RES 150 // Antes era 50
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

    const uint16_t std_resolution = (0x0001 << REAL_ADC_RES) - 0x0001;

    bool ondaQ[SQUARE_WAVE_RES] = {0}; 

    float step;

    uint8_t buf_spk[3] = {'f'};
    uint8_t spk_on = 127;
    uint8_t spk_off = 127;
    volatile bool spike_data[SPIKE_DATA_LENGTH] = {0};
    uint16_t total_spikes = 0;
    uint8_t intrrpts_spk_on = 0; 
    bool spike_off = true;

    bool amp_mudou = false;

    
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
    hw_timer_t * timer_Dac = NULL;
    volatile bool int_dac = false;
    volatile bool timer_on = false;
    volatile bool isr_int = false;

    // Variáveis do controle PI
    const float KInt = 0.05, KProp = 0.15; //KInt = 0.09, KProp = 0.15;   // KInt = 0.1, KProp = 0.2; //Menor valor: 0.004 --> 1/0.004 = 250
    const uint8_t iKInt = (uint8_t)(1/KInt);
    const uint8_t iKProp = (uint8_t)(1/KProp);
    int16_t mInt = 0, mProp = 0;
    uint8_t controle = 0;
    int16_t erro;
    bool readPot = false;
    int16_t setPointPot = 0;
    int16_t current = 0;

    bool firstEdge = false;

    


    const uint8_t dacMin = 0, dacMax = 255;

    void calc_controle();
    void readADC(/*const adc1_channel_t& adc_pin*/);
    void setBound(int16_t val, uint8_t& newVal);
    void accquireData();
    

    void setupOndaQuad();
    void setupSpike();
    void stimulatorState(bool turnON);
    void enableTimers(bool enable);
    
    

  public:
    Electrostimulator(uint8_t _dac_pin, uint8_t _osc_pin, uint8_t _sd_pin, uint8_t _adc_curr_pin, uint8_t _adc_sp_pin, 
    uint8_t _switch_pin);

    void begin();
    void checkSerial(estados *estadoAtual);
    void checkSerial_Fast(estados *estadoAtual);
    void geraOndaQuad(estados *estadoAtual);
    void geraSpike(estados *estadoAtual);
    void geraFormaDeOnda();
    void IRQ_timer_int();
    void IRQ_timer_dac();

    void configTimer(hw_timer_t * _timer, hw_timer_t * _timer_int, hw_timer_t * _timer_dac);
    void IRQtimer();
    

    void interromp(volatile bool * _int, portMUX_TYPE * _timerMux);

    
    
};












#endif
