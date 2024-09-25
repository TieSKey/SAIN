using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAIN.Helpers
{
    public class WeightedRandomSelector<T>
    {

        private class WeightedOption
        {

            public T Option { get; set; }
            public int CutoutValue { get; set; }

        }

        private List<WeightedOption> _options;
        private int _totalWeight;
        private int _highestWeight;
        private Random _random;

        public WeightedRandomSelector()
        {
            _options = new List<WeightedOption>();
            _totalWeight = 0;
            _random = new Random();
        }

        public void AddOption(T option, int weight)
        {
            if (weight <= 0)
                throw new ArgumentException("Weight must be greater than zero.");

            _totalWeight += weight;
            _options.Add(new WeightedOption { Option = option, CutoutValue = _totalWeight });
        }

        public void ClearOptions()
        {
            _options.Clear();
            _totalWeight = 0;
        }

        public void Test(int iterations = 1000)
        {
            var results = new Dictionary<T, int>();
            foreach (var option in _options) {
                if (results.ContainsKey(option.Option)) {
                    Logger.LogWarning($"Copy of {option.Option} in list");
                    continue;
                }

                results.Add(option.Option, 0);
            }

            for (int i = 0; i < iterations; i++) {
                T option = GetRandomOption();
                results[option]++;
            }

            StringBuilder sb = new StringBuilder();
            foreach (var option in results) {
                sb.AppendLine($"{option.Key} : {option.Value}");
            }

            Console.WriteLine(sb);
            Logger.LogInfo(sb.ToString());
        }

        public T GetRandomOption(int maxIterations = 5)
        {
            if (_options.Count == 0)
                throw new InvalidOperationException("No options available.");

            int random = _random.Next(0, _totalWeight);
            foreach (var option in _options) {
                if (option.CutoutValue > random) {
                    return option.Option;
                }
            }

            // Fallback in case of rounding errors, although it should never happen with ints 
            return _options.Last().Option;
        }

    }
}