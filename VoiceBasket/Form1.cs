using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.IO.Ports;
//using namespace std;

namespace VoiceBasket
{
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        public Form1()
        {
            InitializeComponent();
            serialPort1.PortName = "COM3";
            serialPort1.BaudRate = 9600;
            try
            {
                if(!serialPort1.IsOpen)
                serialPort1.Open();
            }
            catch (Exception ex)
            {               MessageBox.Show(ex.Message);
            }
            serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(DataReceived);
        }

        private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort spl = (SerialPort)sender;
                Console.Write("Data" + " " + spl.ReadLine() + "\n");
                //count++;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            recEngine.RecognizeAsync(RecognizeMode.Multiple);
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            recEngine.RecognizeAsyncStop();
            button2.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            serialPort1.Write("fwd");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            serialPort1.Write("left");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            serialPort1.Write("bwd");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            serialPort1.Write("right");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            serialPort1.Write("s");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("\n Still Working, :( So its nothing to help here.. ");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("\n  No About. :(");
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Choices commands = new Choices();
            commands.Add(new string[] { "hello", "who am i", "Go","forward", "left", "right", "stop", "back","danee","bamee","strip" });
            GrammarBuilder gBuilder = new GrammarBuilder();
            gBuilder.Append(commands);
            Grammar grammar = new Grammar(gBuilder);

            recEngine.LoadGrammarAsync(grammar);
            recEngine.SetInputToDefaultAudioDevice();
            recEngine.SpeechRecognized += recEngine_SpeechRecognized;
        }

        void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch(e.Result.Text)
            {
                case "hello":
                    synthesizer.SpeakAsync("hello! is it me you looking for?");
                    MessageBox.Show("Hello, How are you? What's you doing?");
                    
                    break;
                case"who am i":
                    synthesizer.SpeakAsync("you are ankur");
                    break;
                case "Go":
                    serialPort1.Write("fwd");
                    synthesizer.SpeakAsync("forward");
                    richTextBox1.Text += "\n Forward";
                    break;
                case "left":
                    synthesizer.SpeakAsync("left");
                    serialPort1.Write("left");
                    richTextBox1.Text += "\n Left";
                    break;
                case "right":
                    synthesizer.SpeakAsync("right");
                    serialPort1.Write("right");
                    richTextBox1.Text += "\n Right";
                    break;
                case "back":
                    synthesizer.SpeakAsync("fall back");
                    serialPort1.Write("bwd");
                    richTextBox1.Text += "\n back";
                    break;
                case "stop":
                    synthesizer.SpeakAsync("no motion");
                    serialPort1.Write("s");
                    richTextBox1.Text += "\n car stop";
                    break;
                

            }
            //throw new NotImplementedException();
        }
    }
}
