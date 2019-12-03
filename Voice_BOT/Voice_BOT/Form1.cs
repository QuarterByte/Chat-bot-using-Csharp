using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Diagnostics;

namespace Voice_BOT
{
    public partial class Form1 : Form
    {

        SpeechSynthesizer s = new SpeechSynthesizer();
        Choices list = new Choices();
        SpeechRecognitionEngine rec = new SpeechRecognitionEngine();
        bool wake = true;



        String[] greetings = new String[3] { "hello", "Hello, I am EDITH", "How can I help you" };

        public String greet_action()
        {
            Random r = new Random();
            return greetings[r.Next(3)];
        }


        public Form1()
        {


            list.Add(new string[] 
            {
                "hello", "how are you" , "what time is it","what is your name", "what is today","open google",
                "sleep","wake","open office", "close office","hay, edith", "minimize" , "maximize" ,
                "tell me a joke" 
            });

            Grammar gr = new Grammar(new GrammarBuilder(list));

            


            s.SelectVoiceByHints(VoiceGender.Female);
            s.Speak(greet_action());


            try
            {
                rec.RequestRecognizerUpdate();
                rec.LoadGrammarAsync(gr);
                rec.SetInputToDefaultAudioDevice();
                rec.RecognizeAsync(RecognizeMode.Multiple);
                rec.SpeechRecognized += rec_SpeechRecognized;
                
            }
            catch
            {
                return;
            }
            
           

            InitializeComponent();
        }



        public void say(string h)
        {
            s.Speak(h);

            textBox1.AppendText(h + "\n");

        }


        public static void killprog(string s)
        {

            System.Diagnostics.Process[] procs = null;

            try
            {
                procs = Process.GetProcessesByName(s);
                Process Prog = procs[0];

                if (!Prog.HasExited)
                {
                    Prog.Kill();
                }

            }
            finally
            {
                if (procs != null)
                {
                    foreach (Process p in procs)
                    {
                        p.Dispose();
                    }
                }
            }
            procs = null;
        }


        String[] jokes = new String[4] 
        {
            "I tried to change my password to penis, but they said it was too short",
            "What do you call the soft tissue between a shark's teeth, A slow swimmer",
            "Why do bees hum?  becouse They don't remember the text!",
            "What do you call a boomerang that doesn't come back?  stick."
        };

        public String tell_jokes()
        {
            Random rr = new Random();
            return jokes[rr.Next(4)];
            
        }

        private void rec_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {

            string r = e.Result.Text;

            textBox2.AppendText(r + "\n");
            if (r == "hay, edith")
            {
                wake = true;
            }
            if (r == "wake")
            {
                wake = true;
            }
            if (r == "sleep")
            {
                wake = false;
            }

            if (wake == true)
            {

                if (r == "tell me a joke")
                {
                    say(tell_jokes());
                }


                if (r == "minimize")
                {
                    this.WindowState = FormWindowState.Minimized;
                }
                if (r == "maximize")
                {
                    this.WindowState = FormWindowState.Normal;
                }


                if (r == "open office")
                {
                    Process.Start(@"C:\Program Files (x86)\Microsoft Office\root\Office16\WINWORD.exe");
                }

                if (r == "close office")
                {
                    killprog("WINWORD");
                }

                if (r == "hello")
                {
                    say("hi");
                }
                if (r == "how are you")
                {
                    say("I am fine");
                }
                if (r == "what is your name")
                {
                    say("I am EDITH");
                }
                if (r == "what time is it")
                {
                    say(DateTime.Now.ToString("h m tt"));
                }
                if (r == "what is today")
                {
                    say(DateTime.Now.ToString("M/d/yyyy"));
                }
                if (r == "open google")
                {
                    say("oppening google");
                    Process.Start("http://www.google.co.in");
                }
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


    }
}






