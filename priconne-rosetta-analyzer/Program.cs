using System;
using System.IO;
using Vosk;
using NAudio.Wave;

namespace priconne_rosetta_analyzer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string modelPath = "model";
            const int sampleRate = 44100;
            //const int sampleRate = 16000;

            if (!Directory.Exists(modelPath)) {
                Console.WriteLine("here");
                return;
            }

            Vosk.Vosk.SetLogLevel(0);
            var model = new Model(modelPath);
            var rec = new VoskRecognizer(model, sampleRate);

            var waveIn = new WaveInEvent();
            const int deviceNumber = 0;
            waveIn.DeviceNumber = deviceNumber;
            waveIn.WaveFormat = new WaveFormat(sampleRate, WaveIn.GetCapabilities(deviceNumber).Channels);
            waveIn.DataAvailable += (_, e) =>
            {
                if (rec.AcceptWaveform(e.Buffer, e.BytesRecorded))
                {
                    Console.WriteLine(rec.Result());
                }
                else
                {
                    Console.WriteLine(rec.PartialResult());
                }
            };
            waveIn.StartRecording();
            while (true)
            {
                ;
            }
        }
    }
}

