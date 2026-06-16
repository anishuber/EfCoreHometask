namespace Data.CustomExceptions;

public class EntityNotFoundException<T> : Exception
{
    public EntityNotFoundException(int id)
        : base($"Entity {typeof(T)} with id {id} was not found.")
    {
    }
}
