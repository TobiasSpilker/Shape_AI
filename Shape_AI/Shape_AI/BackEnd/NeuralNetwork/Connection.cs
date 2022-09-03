using System;

namespace Shape_AI
{
	public class Connection
	{
		public Guid Id { get; set; }
		public Node InputNeuron { get; set; }
		public Node OutputNeuron { get; set; }
		public double Weight { get; set; }
		public double WeightDelta { get; set; }

		public Connection() { }

		public Connection(Node inputNeuron, Node outputNeuron)
		{
			Id = Guid.NewGuid();
			InputNeuron = inputNeuron;
			OutputNeuron = outputNeuron;
			Weight = Network.GetRandom();
		}

	}
}