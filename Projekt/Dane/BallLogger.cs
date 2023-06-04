using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dane
{
    public class BallLogger
    {
        private bool run;
        private object fileLock = new object();
        private Stopwatch stopwatch = new Stopwatch();

        public BallLogger()
        {
        }

        public async Task StartLogging(ConcurrentQueue<BallAPI> queue)
        {
            run = true;
            await Log(queue);
        }
        public void StopLogging()
        {
            run = false;
        }

        private async Task Log(ConcurrentQueue<BallAPI> queue)
        {
            while (run)
            {
                stopwatch.Restart();
                if (queue.TryDequeue(out BallAPI ball))
                {
                    string log = "{" + String.Format("\n\t\"Date\": \"{0}\",\n\t\"Info\":{1}\n", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"), JsonSerializer.Serialize(ball)) + "}";

                    lock (fileLock)
                    {
                        using (var writer = new StreamWriter("C:\\Users\\adamb\\Desktop\\plik\\Log.json", true, Encoding.UTF8))
                        {
                            writer.WriteLine(log);
                        }
                    }
                }
                stopwatch.Stop();
                await Task.Delay(Math.Max((int)stopwatch.ElapsedMilliseconds, 100));
            }
        }
    }
}