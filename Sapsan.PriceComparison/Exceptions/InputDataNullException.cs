namespace Sapsan.PriceComparison.Exceptions;

public sealed class InputDataNullException : Exception
{
  public InputDataNullException() {}
  public InputDataNullException(string message) : base(message){}
  
}
