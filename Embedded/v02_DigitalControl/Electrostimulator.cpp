#include "Electrostimulator.h"

Electrostimulator::Electrostimulator(uint8_t _dac_pin, uint8_t _osc_pin, uint8_t _sd_pin, uint8_t _adc_curr_pin, uint8_t _adc_sp_pin, 
    uint8_t _switch_pin)
{
    dac_pin = _dac_pin;
    osc_pin = _osc_pin;
    sd_pin = _sd_pin;
    adc_curr_pin = _adc_curr_pin;
    adc_sp_pin = _adc_sp_pin;
    switch_pin = _switch_pin;
}

void Electrostimulator::begin()
{
    pinMode(sd_pin, OUTPUT);
    pinMode(dac_pin, OUTPUT);
    pinMode(switch_pin, INPUT_PULLUP);
    pinMode(adc_curr_pin, INPUT);
    pinMode(adc_sp_pin, INPUT);

    analogReadResolution(ADC_RESOLUTION);

    ledcSetup(PWM_CHANNEL, PWM_FREQ, PWM_RESOLUTION);
    ledcAttachPin(osc_pin, PWM_CHANNEL);

    digitalWrite(sd_pin, LOW);
    ledcWrite(PWM_CHANNEL, OFF);
    dacWrite(dac_pin, OFF); // Put the output to low before starting
}

void Electrostimulator::checkSerial(estados *estadoAtual)
{
    while(Serial.available() > 2)
    {
        uint8_t buf_length = 0;
        char codigo[4];
        char valor_buf[BUF_LENGTH] = {'f'};
        uint32_t valor = 0;
        uint8_t buf[BUF_LENGTH] = {'f'};

        // Lê até a quebra de linha ('\n')
        buf_length = (uint8_t)Serial.readBytesUntil('\n', buf, BUF_LENGTH);

        // Sai do loop caso a leitura tenha tamanho 0 (para evitar quaisquer erros de leitura)
        if(buf_length == 0)
            break;

        // Faz uma string com os 3 primeiros caracteres e salva em 'cod'
        for(int i = 0; i < 3; i++) codigo[i] = buf[i];
        codigo[3] = '\0';
        String cod = String(codigo);

        // Caso o comando tenha mais que 3 posições, o traço é ignorado e os valores posteriores são gravados em valor_buf
        if(buf_length > 3)
        {
            for(int i = 4; i < buf_length; i++) valor_buf[i - 4] = buf[i];
            valor = atoi(valor_buf);    // Converte valor em inteiro
        }
        
        
        // Compara qual código que foi recebido (switch-case não funciona com string em C++)
        if(cod.equals(String("STA")))
        {
            switch(onda)
            {
                case QUADRADA:
                    *estadoAtual = EE_SQUARE;
                    break;
                
                case SPIKE:
                    *estadoAtual = EE_SPK;
                    break;

                default:
                    *estadoAtual = EE_WF;
                    break;
            }
        }
            
        else if(cod.equals(String("STO")))
        {
            //state_changed = 1;
            *estadoAtual = STAND_BY;
        }

        // Define a corrente de saída e já define o valor de saída do DAC em bits
        else if(cod.equals(String("IAM")))
        {
            if(valor < 0) valor = 0;
            if(valor > CORRENTE_MAX) valor = CORRENTE_MAX;
            i_amp = valor;  // Valor da corrente em uA

            // tensão = 1k5 * corrente
            //float tensao = 1500 * (i_amp * 0.000001); // Converte de uA pra A
            //tensao *= 100; // Transforma de 1,5 pra 150
            //valor_DAC = map((uint16_t)tensao, 54, 274, 0, 4095);    // Converte pra saida do DAC em 8 bits de resolução
            //valor_DAC = map(i_amp, 0, CORRENTE_MAX, 0, 255);
            set_current = (uint16_t)(map(i_amp, 0, CORRENTE_MAX, 0, std_resolution));
            //Serial.println(valor_DAC);

            //analogWrite(SAIDA_DAC, saida_DAC);

            // 1/6*V ~ 5/6*V | V = 3.3V
            // 0,54 até 2,74
            // 360 uA até 1823 uA
        }

        // Define a frequência da onda e já calcula seu período em us
        else if(cod.equals(String("FRQ")))
        {
            freq = valor;
            period = (uint32_t)(1000000 / freq);   // Adquire o período em us
            //period *= 1000000;  // Transforma pra us
            //period = (uint32_t)(100000000.0 / (double)freq);    // Transforma pra unidades de 0,01 ns
        }

        // Define a lagura de pulso da onda em us
        else if(cod.equals(String("BDW")))
        {
            bandwidth = valor;
        }

        // Ajusta o sistema para gerar determinada onda
       else if (cod.equals(String("WFM")))
       {
            for(int i = 0; i < 3; i++) codigo[i] = valor_buf[i];
            cod = String(codigo);

            if(cod.equals(String("SQR")))
            {
                onda = QUADRADA;
                setupOndaQuad();    // Define os parâmetros para a geração de onda quadrada
                Serial.write("OK!\n");  // Confirma para o sistema que os dados estão atualizados
            }

            if(cod.equals(String("SPK")))
            {
                onda = SPIKE;
                setupSpike();
                Serial.write("OK!\n");  // Confirma para o sistema que os dados estão atualizados
            }

            if(cod.equals(String("SIN")))
            {
                onda = SENOIDE;
                //setupOndaQuad();
            }

            if(cod.equals(String("TRI")))
            {
                onda = TRIANGULAR;
                //setupOndaQuad();
            }

            if(cod.equals(String("DTS")))
            {
                onda = DENTESERRA;
                //setupOndaQuad();
            }
       }

        else if(cod.equals(String("TDR")))
        {
            total_duration = valor;
        }

        else if(cod.equals(String("REP")))
        {
            //printReport();
        }
        else if(cod.equals(String("ATT")))
        {
            //printReport_Simple();
        }
        else if(cod.equals(String("RND")))
        {
            /*
            if(buf_length < 6)
                break;

            //char codigo_rnd[4];
            for(int i = 0; i < 3; i++) codigo[i] = valor_buf[i];
            cod = String(codigo);

            if(cod.equals(String("OFF")))
            {
                random_bti_on = false;
                burst_train_interval = burst_train_interval_static;
            }
                
            else
            {
                if(buf_length <= 6)
                    break;

                char rnd_valor_buf[32];
                for(unsigned int i = 3; i < sizeof(valor_buf); i++) rnd_valor_buf[i - 3] = valor_buf[i];
                valor = atoi(rnd_valor_buf);

                if(cod.equals(String("MAX")))
                {
                    random_bti_on = true;
                    random_bti_max = valor;
                }
                else if(cod.equals(String("MIN")))
                {
                    random_bti_on = true;
                    random_bti_min = valor;
                }

            }*/           
        }

        else if (cod.equals(String("IDR")))
       {
            for(int i = 0; i < 3; i++) codigo[i] = valor_buf[i];
            cod = String(codigo);

            if(cod.equals(String("ANO")))
            {
                direcao_corrente = ANODICA;
            }

            if(cod.equals(String("CAT")))
            {
                direcao_corrente = CATODICA;
            }

            if(cod.equals(String("BID")))
            {
                direcao_corrente = BIDIRECIONAL;
            }
       }

       else if (cod.equals(String("BGN")))
       {
           Serial.write("Recebeu BGN");
           Serial.println(valor);
           uint16_t index_sum = 0;
           total_spikes = valor;
           while(true)
           {
               if(Serial.available() > 1)
               {
                    buf_length = (uint8_t)Serial.readBytesUntil('\n', buf, BUF_LENGTH);

                    //Serial.write("buf_length = ");
                    //Serial.println(buf_length);
                    //Serial.println(buf);


                    if((buf[0] == 'E') && (buf[1] == 'N') && (buf[2] == 'D'))
                        break;

                    for(int i = 0; i < buf_length; i++)
                    {
                        spike_data[index_sum + i] = (buf[i] == '1');
                    }
                    index_sum += buf_length;
               }
               
           }
           /*
           Serial.write("Saiu do while");
           Serial.println();
           uint8_t sum = 0;
           for(uint16_t i = 0; i < total_spikes; i++)
           {
                if(spike_data[i])
                    Serial.write("1");
                else
                    Serial.write("0");
                
                sum ++;
                if(sum >= 100)
                {
                    Serial.println();
                    sum = 0;
                }
           }*/
            
       }
    }
}

void Electrostimulator::checkSerial_Fast(estados *estadoAtual)
{
    while(Serial.available() > 2)
    {
        uint8_t buf_length = 0;
        uint8_t buf[BUF_LENGTH_SMALL] = {'f'};
        //Serial.write("Checou\n");

        // Lê até a quebra de linha ('\n')
        buf_length = (uint8_t)Serial.readBytesUntil('\n', buf, BUF_LENGTH_SMALL);

        // Sai do loop caso a leitura tenha tamanho 0 (para evitar quaisquer erros de leitura)
        if(buf_length == 0) // Sai do loop caso seja maior que 3 (o terminador é descartado)
            break;

        // Se a mensagem for 'STO', ele volta pro STAND_BY
        if( (buf[0] == 'S') && (buf[1] == 'T') && (buf[2] == 'O'))
            *estadoAtual = STAND_BY;

        break;
    }
}

// Função chamada na maquina de estados
void Electrostimulator::geraOndaQuad(estados *estadoAtual)
{
    long tempo_on_total = millis();
    long tempo_teste = millis();
    uint32_t tempo_step = 0;
    uint8_t valor_saida = 127;
    uint8_t multiplier = 1;
    uint8_t sum = 0;
    uint8_t DAC_On = valor_DAC;
    uint8_t DAC_Off = OFF;

    
    
    uint16_t newStep = (uint16_t)step;
    

    /************* timer **************/
    uint32_t tempoTotal = total_duration * 10000;    // Converte o total duration pra us pra comparar com o que o timer incrementa

    Serial.write("tempoTotal = ");
    Serial.println(tempoTotal);

    Serial.write("INITIATED\n");

    waveIndex = 0;  // Index of the square wave
    countTotal = 0; // Total count of ticks (when it reachs tempoTotal the stimulation is stopped)

    stimulatorState(ON);
    timerAlarmWrite(timer_Onda, nTicks, true);
    enableTimers(true);
    

    // Só sai do while se receber o comando STO ou atingir o tempo máximo
    while (true)
    {
        // ******************* CONTROLE POR TIMER **********************
        // Não precisa usar o critical section pra ler variável compartilhada

        if(interrompeu)
        {
            // Escreve na saída do DAC
            dacWrite(dac_pin, (ondaQ[waveIndex]) ? controle : OFF);

            // Reset na variável de interrupção
            portENTER_CRITICAL(&timerMux);
            interrompeu = false;
            portEXIT_CRITICAL(&timerMux);
            
            // Soma os ticks passados ao total de ticks da duração total
            countTotal += (uint32_t)nTicks;

            if(waveIndex == SQUARE_WAVE_RES)
                waveIndex = 0;
            else
                waveIndex++;

            Serial.write("Controle = ");
            Serial.println(controle);
        }

        if(*estadoAtual != EE_SQUARE)
        {
            enableTimers(false);
            stimulatorState(OFF);
            Serial.write("STOPPED\n");
            return;
        }

        if(countTotal >= tempoTotal)
        {
            enableTimers(false);
            stimulatorState(OFF);
            *estadoAtual = STAND_BY;
            Serial.write("STOPPED\n");
            return;
        }
        checkSerial_Fast(estadoAtual);  // Verifica se o comando STO não chegou
    }
}

// Função chamada quando chega a mensagem 'WFM-SQR'
// Regula as variáveis para que a função geraOndaQuad funcione corretamente
void Electrostimulator::setupOndaQuad()
{
    uint16_t samples;


    
    step = float(period) / float(SQUARE_WAVE_RES);  // Tempo entre uma amostra e outra
    if (step <= 0)
        step = 0.1;
    samples = uint16_t(bandwidth / step);     // Quantidade de amostras no tempo de bandwidth

    // Proteção caso o número de amostras ultrapasse a quantidade máxima
    if(samples > SQUARE_WAVE_RES)
        samples = SQUARE_WAVE_RES;
    
    // Forma a onda quadrada de acordo com a largura de pulso escolhida
    for(int i = 0; i < samples; i++)
        ondaQ[i] = 1;
    if(samples != SQUARE_WAVE_RES)
    {
        for(int i = samples; i < SQUARE_WAVE_RES; i++)
            ondaQ[i] = 0;
    }
    
    Serial.write("step = ");
    Serial.println(step);
    Serial.write("period = ");
    Serial.println(period);
    Serial.write("samples = ");
    Serial.println(samples);

    for(int i = 0; i < SQUARE_WAVE_RES; i++)
    {
        if(ondaQ[i] == 0)
            Serial.write("_");
        else
            Serial.write("-");
    }
    Serial.println();

    //********** LIGANDO O TIMER **********
    portENTER_CRITICAL(&timerMux);
    nTicks = (uint64_t)(step * 10);
    portEXIT_CRITICAL(&timerMux);

    Serial.write("nTicks = ");
    Serial.println((uint32_t)nTicks);

    //ledcSetup(CANAL_PWM, freq, resolution);

    //ledcAttachPin(PINO_SAIDA, CANAL_PWM);
}

void Electrostimulator::geraFormaDeOnda()
{
    // ver qual a onda ativa
}

void Electrostimulator::IRQtimer()
{
    /*
    dacWrite(dac_pin, DAC_out);
    portENTER_CRITICAL_ISR(&timerMux);
    countTotal += nTicks;
    portEXIT_CRITICAL_ISR(&timerMux);
    if(waveIndex == SQUARE_WAVE_RES)
    {
        portENTER_CRITICAL_ISR(&timerMux);
        indexWave = 0;
        portEXIT_CRITICAL_ISR(&timerMux);
    }
    else
    {
        portENTER_CRITICAL_ISR(&timerMux);
        indexWave++;
        portEXIT_CRITICAL_ISR(&timerMux);
    } */
    portENTER_CRITICAL_ISR(&timerMux);
    interrompeu = true;
    portEXIT_CRITICAL_ISR(&timerMux);
}

void Electrostimulator::IRQ_timer_int()
{
    if(onda == SPIKE)
    {
        if(!spike_off)
        {
            calc_controle();
        }
        
    }
    else
        if(ondaQ[waveIndex]) calc_controle();

    
}

void Electrostimulator::configTimer(hw_timer_t * _timer, hw_timer_t * _timer_int)
{
    timer_Onda = _timer;
    timer_Int = _timer_int;

    timerAlarmWrite(timer_Int, TICKS_INT, true);
}

void Electrostimulator::geraSpike(estados *estadoAtual)
{
    bool nextSpike = false;
    uint8_t n_interrupts = 0;
    uint16_t spkIndex = 0;
    uint8_t spk_true = 0;
    
    Serial.write("INITIATED\n");
    timerAlarmWrite(timer_Onda, nTicks, true);
    stimulatorState(ON);
    enableTimers(true);
    
    while(true)
    {
        // Every 250us there is an interrupt. Every time it happens, the software skip to the next spike.
        // CHANGE TO 250 uS, EVERY 4 INTERRUPTS IT PASSES TO THE NEXT SPIKE.
        if(interrompeu)
        {
            portENTER_CRITICAL(&timerMux);
            interrompeu = false;
            portEXIT_CRITICAL(&timerMux);

            // Seta a flag de próximo spike a cada 1 ms (4 interrupções)
            n_interrupts ++;
            if(n_interrupts >= 4)
            {
                n_interrupts = 0;
                spkIndex ++;
                nextSpike = true;
            }
            // If there is a spike on, it updates its time on
            if(spk_true > 0)
                spk_true --;
        }

        // Volta ao início do vetor de spikes caso chegue no fim
        if(spkIndex > total_spikes)
            spkIndex = 0;

        // Enter in this area if it should pass to the next spike in spike data OR there is a spike on
        if(nextSpike || (spike_off == false) )
        {
            nextSpike = false;

            // Check if there is a spike in the spike data
            if(spike_data[spkIndex])    // An spike should be generated
            {
                // If so, the output goes high during the next (intrrpts_spk_on) interrptions
                //Serial.write("SPK\n");
                //dacWrite(dac_pin, spk_on);
                spk_true = intrrpts_spk_on;   // The time of interrupts that the spike will remain on
                spike_off = false;
            }
            // If the next spike is 0, the spike on timed out and there is a spike on, turns it off
            else if(!spike_data[spkIndex] && (spk_true == 0) && (spike_off == false) ) // There is a spike on, but it's time expired
            {
                dacWrite(dac_pin, OFF);
                spike_off = true;
            }
        }

        // Caso um spike esteja ativo, ativa DAC a cada loop para manter o valor atualizado
        if(!spike_off)
            dacWrite(dac_pin, controle);
        

        checkSerial_Fast(estadoAtual);  // Verifica se o comando STO não chegou
        if(*estadoAtual != EE_SPK)
        {
            enableTimers(false);
            stimulatorState(OFF);
            dacWrite(dac_pin, OFF);
            Serial.write("STOPPED\n");
            return;
        }
    }
}
void Electrostimulator::setupSpike()
{
    switch (direcao_corrente)
    {
        // Catodica: 0 a 1.65 V
        case CATODICA:
            //sum = 0;
            //multiplier = 1;
            spk_on = 127 - valor_DAC;
            spk_off = 127;
            break;

        // Anodica 1.65 a 3.3 V
        case ANODICA:
            //sum = 128;
            //multiplier = 1;
            spk_on = 128 + valor_DAC;
            spk_off = 127;
            break;

        // Bidirecional 0 a 3.3 V
        
        case BIDIRECIONAL:
        /*
            sum = 0;
            multiplier = 2;
            valorUp = 128 + valor_DAC;
            valorDown = 127 - valor_DAC;*/
            break;
            
    }

    Serial.write("bandwidth = ");
    Serial.println((uint32_t)bandwidth);
    intrrpts_spk_on = (uint8_t)(bandwidth/250);

    nTicks = (uint64_t)(2500);    // Cada tick = 0.1 us
    // 2500 * 0.1 us = 250 us

    Serial.write("nTicks = ");
    Serial.println((uint32_t)nTicks);

    Serial.write("intrrpts_spk_on = ");
    Serial.println((uint32_t)intrrpts_spk_on);
}

void Electrostimulator::interromp(volatile bool * _int, portMUX_TYPE * _timerMux)
{
    //interrompeu = _int;
    //timerMux = _timerMux;
}

// Liga e desliga o estimulador
void Electrostimulator::stimulatorState(bool turnON)
{
    if(turnON)
    {
        ledcWrite(PWM_CHANNEL, PWM_DUTYCICLE);
        delay(2);
        digitalWrite(sd_pin, HIGH);
        delay(2);
        // The DAC signal will be controlled by other functions
    }
    else
    {
        dacWrite(dac_pin, OFF);
        delay(2);
        digitalWrite(sd_pin, LOW);
        delay(2);
        ledcWrite(PWM_CHANNEL, OFF);
    }
}

// Lê ADC_AVG vezes uma entrada analógica e faz a média desse valor
int16_t Electrostimulator::readADC(uint8_t adc_pin)
{
    int16_t sum;
    for(int i = 0; i < ADC_AVG; i++)
        sum += analogRead(adc_pin);
    sum /= ADC_AVG;
    return sum;
}

// Altera a variável de controle
void Electrostimulator::calc_controle()
{
    int16_t erro;// = (float)((!digitalRead(switch_pin)) ? (readADC(adc_sp_pin) - readADC(adc_curr_pin)) : (set_current - readADC(adc_curr_pin)));
    if(!digitalRead(switch_pin))
        erro = readADC(adc_sp_pin) - readADC(adc_curr_pin);
    else
        erro = set_current - readADC(adc_curr_pin);
    mProp = erro/iKProp;
    mInt += erro/iKInt;
    controle = setBound(mProp + mInt);

    //Serial.write("Controle = ");
    //Serial.println(controle);
}

// SE MUDAR O ADC RESOLUTION (8), TEM QUE MUDAR ESSA FUNÇÃO
// Limita o valor de entrada entre dacMin e dacMax
uint8_t Electrostimulator::setBound(int16_t val)
{
    if(val > dacMax) return dacMax;
    if(val < dacMin) return dacMin;
    return (uint8_t)val;
    //return (uint8_t)((val > dacMax) ? dacMax : ((val < dacMin) ? dacMin : val));
}

// Ativa e desativa os timers
void Electrostimulator::enableTimers(bool enable)
{
    if(enable)
    {
        timerAlarmEnable(timer_Onda);
        timerAlarmEnable(timer_Int);
    }
    else
    {
        timerAlarmDisable(timer_Onda);
        timerAlarmDisable(timer_Int);
    }
}