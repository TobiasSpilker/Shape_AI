using System;
using System.Collections.Generic;
using System.Linq;

namespace Shape_AI
{
	public class Network
	{
		public double LearnRate { get; set; }
		public double Momentum { get; set; }
		public List<Node> InputLayer { get; set; }
		public List<List<Node>> HiddenLayers { get; set; }
		public List<Node> OutputLayer { get; set; }

		private static readonly Random Random = new Random();

		public Network()
		{
			LearnRate = 0;
			Momentum = 0;
			InputLayer = new List<Node>();
			HiddenLayers = new List<List<Node>>();
			OutputLayer = new List<Node>();
		}

		public Network(int inputSize, int[] hiddenSizes, int outputSize, double? learnRate = null, double? momentum = null)
		{
			LearnRate = learnRate ?? .4;
			Momentum = momentum ?? .9;
			InputLayer = new List<Node>();
			HiddenLayers = new List<List<Node>>();
			OutputLayer = new List<Node>();

			for (var i = 0; i < inputSize; i++)
				InputLayer.Add(new Node());

			var firstHiddenLayer = new List<Node>();
			for (var i = 0; i < hiddenSizes[0]; i++)
				firstHiddenLayer.Add(new Node(InputLayer));

			HiddenLayers.Add(firstHiddenLayer);

			for (var i = 1; i < hiddenSizes.Length; i++)
			{
				var hiddenLayer = new List<Node>();
				for (var j = 0; j < hiddenSizes[i]; j++)
					hiddenLayer.Add(new Node(HiddenLayers[i - 1]));
				HiddenLayers.Add(hiddenLayer);
			}

			for (var i = 0; i < outputSize; i++)
				OutputLayer.Add(new Node(HiddenLayers.Last()));
		}

		public void Train(List<DataSetMaker> dataSets, int numEpochs)
		{
			for (var i = 0; i < numEpochs; i++)
			{
				foreach (var dataSet in dataSets)
				{
					ForwardPropagate(dataSet.Values);
					BackPropagate(dataSet.Targets);
				}
			}
		}

		public void Train(List<DataSetMaker> dataSets, double minimumError)
		{
			var error = 1.0;
			var numEpochs = 0;

			while (error > minimumError && numEpochs < int.MaxValue)
			{
				var errors = new List<double>();
				foreach (var dataSet in dataSets)
				{
					ForwardPropagate(dataSet.Values);
					BackPropagate(dataSet.Targets);
					errors.Add(CalculateError(dataSet.Targets));
				}
				error = errors.Average();
				numEpochs++;
			}
		}

		private void ForwardPropagate(params double[] inputs)
		{
			var i = 0;
			InputLayer.ForEach(a => a.Value = inputs[i++]);
			HiddenLayers.ForEach(a => a.ForEach(b => b.CalculateValue()));
			OutputLayer.ForEach(a => a.CalculateValue());
		}

		private void BackPropagate(params double[] targets)
		{
			var i = 0;
			OutputLayer.ForEach(a => a.CalculateGradient(targets[i++]));
			HiddenLayers.Reverse();
			HiddenLayers.ForEach(a => a.ForEach(b => b.CalculateGradient()));
			HiddenLayers.ForEach(a => a.ForEach(b => b.UpdateWeights(LearnRate, Momentum)));
			HiddenLayers.Reverse();
			OutputLayer.ForEach(a => a.UpdateWeights(LearnRate, Momentum));
		}

		public double[] Compute(params double[] inputs)
		{
			ForwardPropagate(inputs);
			return OutputLayer.Select(a => a.Value).ToArray();
		}

		private double CalculateError(params double[] targets)
		{
			var i = 0;
			return OutputLayer.Sum(a => Math.Abs(a.CalculateError(targets[i++])));
		}

		public static double GetRandom()
		{
			return 2 * Random.NextDouble() - 1;
		}

	}

	public enum TrainingType
	{
		Epoch,
		MinimumError
	}

}