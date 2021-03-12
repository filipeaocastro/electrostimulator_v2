using Eletroestimulador_v02.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

/*
 *  This form class controls the test protocol in software. 
 *  After the user load the textures and connect to the uC in the SpikesForm, this protocol may be started.
 *  
 *  The first step is click on the Start Button. 
 *  This click cause an event that triggers a countdown and loads the spike texture into the uC.
 *  After the countdown, it is shown the texture image while stimulating the patient according to this texture. After the 
 *  stimualtion is over, it is made an interval before the next texture. This process repeats 3 times (one for each texture).
 *  Then, after a bigger interval, a cross appears on the screen with a stimulation in parallel. This process is repeated 15
 *  times with an interval of 5 to 7 seconds between each one. Each stimulation is cessed when the user press a key (1, 2 or 3)
 *  or after 7.3 seconds (the duration of each texture).
 *  At the end is generated a file with all information about the relation of the texture order, key pressed, duration, etc.
 *  
 * 
 * 
 */

namespace Eletroestimulador_v02
{
    public partial class TestProtocol : Form
    {
        SpikesForm telaSpikes;
        private int[] texSequence = new int[15];
        private long[] elapedTime = new long[15];
        private string[] arrowPressedArr = new string[15];
        private int textureSeqIndex = 0;    // Controls the 15 texture sequence
        private int sequenceIndex = 0;      // Controls the texture image sequence during the protocol
        private int countdown = 0;
        private string pressedKey;

        private bool activeProgBar = false;
        

        // Stopwatch to compute the elapsed time
        Stopwatch interval = new Stopwatch();
        private long nanosecPerTick = (1000L * 1000L * 1000L) / Stopwatch.Frequency;
        private long tempoDecorrido = 0;

        StreamWriter myStream;
        SaveFileDialog saveFileDialog1;

        // Enums for defining the system's possible states
        #region enums defining the system states

        // States of the screen
        private enum screen_states
        {
            COUNTDOWN,  // In countdown (black screen)
            IMAGE,      // Showing the texture image
            CROSS       // Showing a cross
        }
        screen_states screenState;

        // Defines the states of the test protocol
        private enum test_step
        {
            STOPPED,            // Initial state
            TEXTURE_IMAGES,     // Showing the texture images
            WAITING_FOR_START,  // Interval among stimulations
            STIMULATION_ON      // Stimulating
        }
        test_step testStep;

        // States of the serial communication
        private enum serial_states
        {
            STOPPED = 0,    // Not stimulating
            INITIATED,  // Stimulation started
            UPDATED,    // uC updated and ready to start stimulation
            IDLE    // Initial state
        }
        serial_states serial_States = serial_states.IDLE;

        #endregion


        #region Constructors

        // Default constructor
        public TestProtocol()
        {
            InitializeComponent();
        }

        // Constructor to pass the previous form as a parameter to use its thread for serial communication
        public TestProtocol(SpikesForm _telaSpikes)
        {
            InitializeComponent();
            telaSpikes = _telaSpikes;

            // Generate the 15 texture sequence
            generateSequence();

            testStep = test_step.STOPPED;
            Console.WriteLine(telaSpikes.th.IsAlive.ToString());
            telaSpikes.testProtocolOn = true;
        }
        #endregion

        // Event called when the Start button is pressed
        private void button_start_Click(object sender, EventArgs e)
        {
            if (testStep == test_step.STOPPED)
            {
                button_start.Visible = false;   // Hide the button
                testStep = test_step.TEXTURE_IMAGES;    // Change the stimulation state
                screenState = screen_states.COUNTDOWN;  // Change the screen state
                sequenceIndex = 1;  // Set the first texture of the sequence
                updateTexture(sequenceIndex);   // Transfer the spike data to uC
                showCountDown(5);   // Start the countdown

                button_status.Focus();  // Focus on a hidden button
            }
        }

        private void generateSequence()
        {
            int[,] positions = new int[3, 5];
            int cont;

            // Positions matrix:
            // Texture 1: 0 0 0 0 0
            // Texture 2: 0 0 0 0 0
            // Texture 3: 0 0 0 0 0

            Random rand = new Random();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    cont = 0;
                    positions[i, j] = rand.Next(0, 15);


                    foreach (int o in positions)
                    {
                        if (o == positions[i, j])
                            cont++;
                    }

                    if (cont > 1)
                        j--;
                }
            }


            Console.Write("Positions = [");
            foreach (int i in positions)
            {
                Console.Write(i.ToString() + ", ");
            }
            Console.WriteLine("]");

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    texSequence[positions[i, j]] = i + 1;
                }
            }

            Console.Write("texSequence = [");
            foreach (int i in texSequence)
            {
                Console.Write(i.ToString() + ", ");
            }
            Console.WriteLine("]");



        }

        #region Countdown and timer functions

        private void showCountDown(int count)
        {
            countdown = count;
            label_countDown.Text = countdown.ToString();
            //label_countDown.Visible = true;
            screenState = screen_states.COUNTDOWN;
            timer1.Interval = 1000; // voltar pra 1000
            timer1.Enabled = true;
            timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (screenState)
            {
                case screen_states.COUNTDOWN:
                    countdown--;
                    if (countdown == 0)
                    {
                        timer1.Stop();

                        if (testStep == test_step.TEXTURE_IMAGES)
                        {
                            label_countDown.Visible = false;
                            screenState = screen_states.IMAGE;
                            startStimulation();
                            showTextures();
                        }
                        else if (testStep == test_step.WAITING_FOR_START)
                        {
                            testStep = test_step.STIMULATION_ON;
                            Console.WriteLine("Before start");
                            startStimulation();
                            Console.WriteLine("Started");
                        }
                        else if (testStep == test_step.STIMULATION_ON)
                        {
                            startStimulation();
                        }

                        break;
                    }
                    label_countDown.Text = countdown.ToString();
                    break;

                case screen_states.IMAGE:
                    endStimulation(false);
                    if (sequenceIndex == 3)
                    {
                        sequenceIndex = 0;
                        timer1.Stop();
                        testStep = test_step.WAITING_FOR_START;
                        pictureBox_textura.Image = null;
                        screenState = screen_states.COUNTDOWN;
                        showCountDown(10);
                        sequenceIndex = 0;
                        updateTexture(0);

                        break;
                    }

                    timer1.Stop();
                    timer1.Enabled = false;
                    screenState = screen_states.COUNTDOWN;
                    pictureBox_textura.Image = null;
                    updateTexture(sequenceIndex);
                    showCountDown(5);
                    sequenceIndex++;
                    break;

                case screen_states.CROSS:

                    endStimulation(false);
                    
                    break;

            }
        }

        #endregion

        #region Show textures images

        private void showTextures()
        {
            timer1.Enabled = false;
            timer1.Interval = 7000; // voltar pra 7000
            showImage(sequenceIndex);
            timer1.Enabled = true;
            timer1.Start();
        }

        private void showImage(int texNum)
        {
            pictureBox_textura.Visible = true;
            switch (texNum)
            {
                case 1:
                    pictureBox_textura.Image = Resources.tex1___NEG;
                    break;

                case 2:
                    pictureBox_textura.Image = Resources.tex2___NEG;
                    break;

                case 3:
                    pictureBox_textura.Image = Resources.tex3___NEG;
                    break;
            }
        }

        #endregion

        #region start/update/end/finish stimulation

        private void startStimulation()
        {
            timer1.Stop();
            checkSerialState();
            if (serial_States != serial_states.UPDATED)
            {
                MessageBox.Show("Something is wrong with the connection\nEstado: " + serial_States.ToString(), 
                    "Erro", MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);

                // ************************** COLOCAR QUE FUNCÃO FINALIZA O PROTOCOLO E FECHA A TELA **********

                return;
            }

            telaSpikes.ESPSerial.WriteLine(Protocolos.iniciar);

            do
            {
                checkSerialState();
            } while (serial_States != serial_states.INITIATED);

            if (testStep == test_step.STIMULATION_ON)
            {
                timer1.Interval = 7300;
                interval.Start();

                screenState = screen_states.CROSS;
                label_countDown.Visible = true;
                label_countDown.Text = "+";

                timer1.Start(); // Inicia o timer de 7.3 s

                label_counter.Text = (textureSeqIndex + 1).ToString();
            }

            

        }

        private void updateTexture(int num)
        {
            if(num == 0)
                telaSpikes.updateTexture(texSequence[textureSeqIndex]);
            else
                telaSpikes.updateTexture(num);
            telaSpikes.sendData();
            telaSpikes.spikeTransfer();
            telaSpikes.ESPSerial.WriteLine(Protocolos.wf_spike);
            
            Console.WriteLine("Updated");
        }

        private void endStimulation(bool arrowPressed)
        {
            if(screenState == screen_states.CROSS)
            {
                elapedTime[textureSeqIndex] = interval.ElapsedMilliseconds; // Save the elapsed time on this texture

                // Stop the timer and the Stopwatch
                interval.Stop();
                interval.Reset();
                timer1.Stop();
            }


            Random rand;
            telaSpikes.ESPSerial.WriteLine(Protocolos.parar);   // Send the command to stop the stimulation
            serial_States = serial_states.STOPPED;  // Chante de serial state to STOPPED

            if(screenState == screen_states.CROSS)
            {
                
                if (arrowPressed)
                    arrowPressedArr[textureSeqIndex] = pressedKey;
                else
                    arrowPressedArr[textureSeqIndex] = "NULL";

                if (textureSeqIndex >= (texSequence.Length - 1))
                {
                    label_countDown.Text = "STOP";
                    finishProtocol();
                }
                else
                {
                    label_countDown.Visible = false;
                    textureSeqIndex++;
                    rand = new Random();
                    showCountDown(rand.Next(5, 8));
                    updateTexture(0);
                }
            }
            
            

        }

        private void finishProtocol()
        {
            saveOutput();
            this.Close();
        }

        #endregion

        private void checkSerialState()
        {
            switch (telaSpikes.stateProtocol)
            {
                case 0:
                    serial_States = serial_states.STOPPED;
                    break;

                case 1:
                    serial_States = serial_states.INITIATED;
                    break;

                case 2:
                    serial_States = serial_states.UPDATED;
                    break;

                case 3:
                    serial_States = serial_states.IDLE;
                    break;
            }
        }

        private void button_status_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Console.WriteLine("Tecla apertada");

            if (serial_States == serial_states.INITIATED)
            {
                switch (e.KeyCode)
                {
                    case Keys.NumPad1:
                        pressedKey = "1";
                        break;
                    case Keys.D1:
                        pressedKey = "1";
                        break;

                    case Keys.NumPad2:
                        pressedKey = "2";
                        break;
                    case Keys.D2:
                        pressedKey = "2";
                        break;

                    case Keys.NumPad3:
                        pressedKey = "3";
                        break;
                    case Keys.D3:
                        pressedKey = "3";
                        break;

                    default:
                        return;
                }

                endStimulation(true);
            }
        }

        #region file output related functions

        private void choosePath()
        {
            try
            {
                saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    myStream = new StreamWriter(saveFileDialog1.FileName);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void saveOutput()
        {
            string[] doc = new string[16];
            doc[0] = "Trial\tTexture\tPressed Key\tElapsed Time (ms)";
            
            for(int i = 1; i < 16; i ++)
            {
                doc[i] = i.ToString() + "\t" + texSequence[i - 1].ToString() + "\t" 
                    + arrowPressedArr[i - 1] + "\t\t" + elapedTime[i - 1].ToString();
            }

            try
            {
                choosePath();
                if (myStream  != null)
                {
                    foreach(string s in doc)
                    {
                        myStream.WriteLine(s);
                    }
                        
                    myStream.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void TestProtocol_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}