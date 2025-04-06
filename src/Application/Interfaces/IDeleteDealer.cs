namespace Application.Interfaces
{
    public interface IDeleteDealer
    {
        Task<bool> Execute(int id);
    }
}
