using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace POC_ORM_NetCore
{
    internal class PerformanceMonitor
    {
        public PerformanceMonitor()
        {
            _stopwatches = new Dictionary<string, Dictionary<int, Dictionary<string, Stopwatch>>>();
        }

        private readonly Dictionary<string, Dictionary<int, Dictionary<string, Stopwatch>>> _stopwatches;
        private string _currentTestCase;

        private int _numberOfPasses;
        private int _currentPass;

        internal void Use(string testCase)
        {
            _currentTestCase = testCase;
            _stopwatches.Add(testCase, new Dictionary<int, Dictionary<string, Stopwatch>>());
        }

        internal PerformanceMonitor Passes(int passes)
        {
            _numberOfPasses = passes;
            return this;
        }

        internal void ForEach<T>(IEnumerable<Type> testCases, Action<T> action, Action<T> warmup = null)
        {
            foreach (var testCaseType in testCases)
            {
                Use(testCaseType.Name);
                for (_currentPass = 1; _currentPass <= _numberOfPasses; _currentPass++)
                {
                    var testCase = (T)Activator.CreateInstance(testCaseType);

                    warmup?.Invoke(testCase);

                    _stopwatches[_currentTestCase].Add(_currentPass, new Dictionary<string, Stopwatch>());

                    // Start("Total Execution Time");
                    action.Invoke(testCase);
                    // Stop("Total Execution Time");
                }
            }
        }

        internal Profile Start(string key)
        {
            var stopwatch = new Stopwatch();
            _stopwatches[_currentTestCase][_currentPass].Add(key, stopwatch);
            return new Profile(stopwatch);
        }

        internal void Stop(string key)
        {
            _stopwatches[_currentTestCase][_currentPass][key].Stop();
        }

        internal void Write(string outputPath)
        {
            if (!outputPath.EndsWith(".csv"))
                outputPath += ".csv";

            var sb = new StringBuilder();

            // Headers
            foreach (var sw in _stopwatches)
                sb.Append($@",""{sw.Key}""");

            sb.AppendLine();

            // Body
            foreach (var key in _stopwatches.First().Value.Values.First().Keys)
            {
                sb.Append($@"""{key}""");

                foreach (var testCase in _stopwatches)
                {
                    double averageTime;
                    var executionTimes = testCase.Value.Select(c => c.Value[key]);

                    if (_numberOfPasses > 3)
                    {
                        var quartile = (int)Math.Floor((double)executionTimes.Count() / 4);
                        averageTime = executionTimes
                            .Skip(quartile)
                            .Take(executionTimes.Count() - (quartile * 2))
                            .Average(c => c.ElapsedMilliseconds);
                    }
                    else
                    {
                        averageTime = executionTimes.Average(c => c.ElapsedMilliseconds);
                    }

                    sb.Append("," + Math.Round(averageTime, 2));
                }

                sb.AppendLine();
            }

            File.WriteAllText(outputPath, sb.ToString());
        }

        internal class Profile : IDisposable
        {
            private readonly Stopwatch _stopwatch;

            public Profile(Stopwatch stopwatch)
            {
                _stopwatch = stopwatch;
                _stopwatch.Start();
            }

            public void Dispose()
            {
                _stopwatch.Stop();
            }
        }
    }
}