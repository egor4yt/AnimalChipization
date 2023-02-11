namespace AnimalChipization.Core.Exceptions;

public interface IApiException
{
    string ApiMessage { get; }
}