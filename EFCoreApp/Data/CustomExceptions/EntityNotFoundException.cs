namespace Data.CustomExceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string message)
        : base(message)
    {
    }
}

public class EntityNotFoundException<T> : EntityNotFoundException
{
    public EntityNotFoundException(int id)
        : base($"Entity {typeof(T).Name} with id {id} was not found.")
    {
    }
}
