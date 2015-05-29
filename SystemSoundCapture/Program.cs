using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using CSCore;
using CSCore.SoundIn;
using CSCore.Codecs.WAV;

namespace SystemSoundCapture
{
    class Program
    {
        static void Main(string[] args)
        {
            startRec();
            listen();
        }

        private static void startRec()
        {
            string filename = "./" + DateTime.Now.ToString("d_MMM_yyyy_HH_mm_ssff") + ".wav";
            using (WasapiCapture capture = new WasapiLoopbackCapture())
            {
                capture.Initialize();

                using (WaveWriter w = new WaveWriter(filename, capture.WaveFormat))
                {
                    capture.DataAvailable += (s, e) =>
                    {
                        w.Write(e.Data, e.Offset, e.ByteCount);
                    };

                    Console.ReadKey();
                    capture.Start();
                    Console.ReadKey();
                    capture.Stop();
                }
            }
        }

        private static void listen()
        {

        }
    }
}
