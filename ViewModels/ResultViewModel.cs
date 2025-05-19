namespace LivrariaApi.ViewModels;

public class ResultViewModel<T>
{
    public ResultViewModel(T data , List<string> errors)
    {
        Success = false;
        Data = data;
        Errors = errors;
    }

    public ResultViewModel(T data)
    {
        Data = data;
        Success = true;
    }

    public ResultViewModel(List<string> errors)
    {
        Success = false;
        Errors = errors;
    }

    public ResultViewModel(string error)
    {
        Success = false;
        Errors.Add(error);
    }

     


    public T Data { get; private set; }
    public List<string> Errors { get; private set; } = new();
    public bool Success { get; private set; }
}