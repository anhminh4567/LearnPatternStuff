namespace TestConceptPattern.CQRS
{
	public class Result<TValue,TError>	
	{
		public bool IsSuccess { get; set; }
		public bool IsError { get; set; }
		private readonly TValue? _value;
		private readonly TError? _error;

		public Result( TValue? value)
		{
			IsError = false;
			_value = value;
			_error = default;
		}
	}
}
