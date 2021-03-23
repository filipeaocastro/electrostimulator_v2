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
 *  or after 6 seconds (the duration of each texture).
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

        private readonly int texture_duration = 6000;
        

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

        /*
         * Event called whrn the button 'Start' is pressed. 
         * It changes the system's states in order to start the protocol.
         */
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

        // Function to fetch the first three textures and distribute them equally in a 15 positions sequence
        private void generateSequence()
        {
            int[,] positions = new int[3, 5];   // Matrix to store the sequence (lines = texture number, 
                                                    // columns = its position in the 15 positions vector)
            int cont;

            // Fill the array with '15' in every position (since the numbers to be placed in the array vary 
            //  from 0 to 14)
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 5; j++)
                    positions[i, j] = 15;

            // Positions matrix:
            // Texture 1: 15 15 15 15
            // Texture 2: 15 15 15 15
            // Texture 3: 15 15 15 15

            // Iterate through the matrix, generating random numbers (0-14) and checking for repetitions
            Random rand = new Random();
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 5; j++)
                {
                    positions[i, j] = rand.Next(0, 15); // Fill the [i,j] position with a random number from 0 to 14
                    cont = 0;   // Set the counter to zero
                    
                    // Check the matrix for repetitions of the number stored in [i, j]
                    foreach (int o in positions)
                        if (o == positions[i, j])   // There is a repetition
                            cont++;

                    // Go back one iteration
                    if (cont > 1)   
                        j--;     
                }

            // Prints in the console the matrix
            Console.Write("Positions = [");
            foreach (int i in positions)
                Console.Write(i.ToString() + ", ");
            Console.WriteLine("]");

            // Fill the texSequence vector in a way that the lines indicate the texture (1-3) and the 
            //  value in the matrix define the position of each one
            // Example: positions[0, 4] = 5, that is: line 0 = texture 1 and the number 5 indicates its index in texSequence
            // Therefore: texSequence[5] = 1
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 5; j++)
                    texSequence[positions[i, j]] = i + 1;

            // Prints the resulting sequence in the console
            Console.Write("texSequence = [");
            foreach (int i in texSequence)
                Console.Write(i.ToString() + ", ");
            
            Console.WriteLine("]");
        }

        #region Countdown and timer functions

        /*
         *  Function that runs a countdown between stimulations. It doesn't really show the countdown to the user
         *  
         *  count = number of seconds of the countdown
         */
        private void showCountDown(int count)
        {
            countdown = count;  // Set the global variable 'countdown'
            screenState = screen_states.COUNTDOWN;  // Set the screen to countdown mode
            timer1.Interval = 1000; // Interval between each tick (ms)
            timer1.Enabled = true;  // Enable the timer
            timer1.Start(); // Start the countdown
        }

        /*
         * The function called each time a tick occurs in the timer1
         * 
         * If the screen is in COUNTDOWN state, it decreases the 'countdown' variable each tick.
         * When it reached 0, it starts an stimulation according to the 'test_step' state
         * 
         * If the screen is in IMAGE state, it removes the image from the screen and restart the countdown
         */
        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (screenState)
            {
                // COUNTDOWN is the screen state where the screen is black
                case screen_states.COUNTDOWN:
                    countdown--;
                    if (countdown == 0)
                    {
                        timer1.Stop();
                        if (testStep == test_step.TEXTURE_IMAGES) // Step of showing the textures with images
                        {
                            screenState = screen_states.IMAGE;  // Change the scren state
                            startStimulation(); // Start the stimulation with the texture image
                            showTextures(); // Show the texture image
                        }

                        // After showing images and before starting the 15 stimualtions sequence
                        else if (testStep == test_step.WAITING_FOR_START) 
                        {
                            testStep = test_step.STIMULATION_ON;    // Change test step
                            Console.WriteLine("Before start");
                            startStimulation(); // Start the first stimulation of the 15 sequence
                            Console.WriteLine("Started");
                        }

                        // The 15 sequence has already started
                        else if (testStep == test_step.STIMULATION_ON)
                        {
                            startStimulation();
                        }
                        break;
                    }
                    break;
                
                case screen_states.IMAGE:   // When showing the texture images
                    endStimulation(false);  // Stop the stimulation

                    // If it is the last image, pass to the next step of the protocol
                    if (sequenceIndex == 3)
                    {
                        sequenceIndex = 0;  // Reset the sequence
                        timer1.Stop();
                        testStep = test_step.WAITING_FOR_START; // Chenge the protocol step
                        pictureBox_textura.Image = null;    // Removes the image from the screen
                        screenState = screen_states.COUNTDOWN;  // Updates the screen state
                        showCountDown(10);  // Start a 10 seconds countdown
                        sequenceIndex = 0;
                        updateTexture(0);   // Load the texture into the uC
                        break;
                    }

                    // If its not the last image, set a  seconds countdown before the next image
                    timer1.Stop();
                    timer1.Enabled = false;
                    screenState = screen_states.COUNTDOWN;
                    pictureBox_textura.Image = null;
                    updateTexture(sequenceIndex);
                    showCountDown(5);
                    sequenceIndex++;
                    break;

                    
                case screen_states.CROSS:   // The 15 sequence stimulations
                    endStimulation(false);  // Stop the stimulation and prepare for the next
                    break;

            }
        }

        #endregion

        #region Show textures images

        /*
         * Shows a texture's image for 'texture_duration' seconds
         */
        private void showTextures()
        {
            timer1.Enabled = false;
            timer1.Interval = texture_duration; // voltar pra 7000
            showImage(sequenceIndex);
            timer1.Enabled = true;
            timer1.Start();
        }

        /*
         * Show the image in the screen acconding to the texture number (texNum)
         */ 
        private void showImage(int texNum)
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath;
            string fileName = "";
            pictureBox_textura.Visible = true;

            // Select the image name according to the imput number
            switch (texNum)
            {
                case 1:
                    //pictureBox_textura.Image = Resources.tex1___NEG;
                    fileName = "textures\\1.png";
                    break;

                case 2:
                    //pictureBox_textura.Image = Resources.tex2___NEG;
                    fileName = "textures\\2.png";
                    break;

                case 3:
                    //pictureBox_textura.Image = Resources.tex3___NEG;
                    fileName = "textures\\3.png";
                    break;
            }
            filePath = Path.Combine(docPath, fileName);
            pictureBox_textura.Image = Image.FromFile(filePath);    // Shows the image
        }

        #endregion

        #region start/update/end/finish stimulation
        
        /*
         * Starts the Stimulation when the uC is already updated
         */
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
                endStimulation(false);
                Application.Exit();

                return;
            }

            // Set the uC to start the stimulation
            telaSpikes.ESPSerial.WriteLine(Protocolos.iniciar);

            // Wait the uC for confirming the stimulation start
            do
            {
                checkSerialState();
            } while (serial_States != serial_states.INITIATED);

            // Start the stimulation after the uC confirm
            if (testStep == test_step.STIMULATION_ON)
            {
                timer1.Interval = texture_duration; // Set the timer for the stimulation duration control
                interval.Start();   // Start the stopwatch

                screenState = screen_states.CROSS;  // Change the screen state to appear the Cross
                label_countDown.Visible = true;
                label_countDown.Text = "+";

                timer1.Start(); //Start the timer for the stimulation of 'texture_duration' seconds
            }
        }

        /*
         * Updates the uC with the desired texture. 
         * When the parameter is 0, it uploads the texture corresponding to 'texSequence[textureSeqIndex]'
         * Else, it update the texture corresponding to the parameter (needs to be from 1-3)
         */
        private void updateTexture(int num)
        {
            // When 0, it follows the sequence defined in texSequence
            if(num == 0)
                telaSpikes.updateTexture(texSequence[textureSeqIndex]);
            // Else, it selects the texture corresponding the number 'num'
            else
                telaSpikes.updateTexture(num);

            telaSpikes.sendData();  // Send stimulation data to uC
            telaSpikes.spikeTransfer(); // Send the spikes
            telaSpikes.ESPSerial.WriteLine(Protocolos.wf_spike);    // Set SPIKE as the actie waveform
            
            Console.WriteLine("Updated");
        }

        /*
         * Stop the stimulation.
         * If it is in the CROSS state, it records the arrow pressed (if there was any) and in the last stimulation
         * of the sequece it calls the function that generates the results file
         */
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
            serial_States = serial_states.STOPPED;  // Change the serial state to STOPPED
            label_countDown.Visible = false;

            if(screenState == screen_states.CROSS)
            {
                // Records the pressed key in the arrowPressedArr array
                if (arrowPressed)
                    arrowPressedArr[textureSeqIndex] = pressedKey;
                else
                    arrowPressedArr[textureSeqIndex] = "NULL";

                // In the last position of the sequence, it generates the results file
                if (textureSeqIndex >= (texSequence.Length - 1))
                {
                    label_countDown.Text = "STOP";
                    finishProtocol();
                }
                else // Set a new countdown before the next stimulation
                {
                    textureSeqIndex++;
                    rand = new Random();
                    showCountDown(rand.Next(5, 8));
                    updateTexture(0);   // Update the texture in the uC
                }
            }
        }

        // Terminates the protocol by saving the results file and closing the Application
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

/* TO DO
 * Ver a fonte das imagens de textura
 * Ver se tem como fazer um jeito pra Ana upar as imagens dela
 */