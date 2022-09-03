namespace Shape_AI
{
	public class DataSetMaker
	{

		public double[] Values { get; set; }
		public double[] Targets { get; set; }

		public DataSetMaker(double[] values, double[] targets)
		{
			Values = values;
			Targets = targets;
		}

	}
}