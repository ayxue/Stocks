using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.Audio
{
    public class WinRecorder: IDisposable
    {
        public static IList<WaveInCapabilities> Devices
        {
            get;
            private set;
        }

        static WinRecorder()
        {
            Devices = Enumerable.Range(-1, WaveIn.DeviceCount + 1).Select(n => WaveIn.GetCapabilities(n)).ToArray();
        }


        private WaveFileWriter _writer;
        public WaveIn CaptureDevice
        {
            get; private set;
        }

        public string OutPutFolder
        {
            get;private set;
        }

        public int MaxRecordingSeconds
        {
            get; private set;
        }

        public WinRecorder(WaveInCapabilities device, string outputFolder, int maxRecordingSeconds = 25)
        {
            var deviceNumber = Devices.IndexOf(device);
            var waveFormat = new WaveFormat(11025, 1);

            CaptureDevice = new WaveIn() { DeviceNumber = deviceNumber };      
            CaptureDevice.WaveFormat = waveFormat;
            CaptureDevice.DataAvailable += OnDataAvailable;
            OutPutFolder = outputFolder;
            MaxRecordingSeconds = maxRecordingSeconds;
        }

        public void Start()
        {
            var fileName = Path.Combine(OutPutFolder, DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".wav");
            _writer = new WaveFileWriter(fileName, CaptureDevice.WaveFormat);
            CaptureDevice.StartRecording();
        }

        void StopRecording()
        {
            _writer?.Close();
            _writer?.Dispose();
            _writer = null;
            CaptureDevice.StopRecording();
        }

        public void Dispose()
        {
            StopRecording();
            CaptureDevice.DataAvailable -= OnDataAvailable;
            CaptureDevice.Dispose();
            CaptureDevice = null;
        }

        private void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            if (_writer == null)
                return;

            _writer.Write(e.Buffer, 0, e.BytesRecorded);
            int secondsRecorded = (int)(_writer.Length / _writer.WaveFormat.AverageBytesPerSecond);
            if (secondsRecorded >= MaxRecordingSeconds)
                StopRecording();
        }
    }
}
