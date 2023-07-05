namespace Dddreams.Domain.Common;

public interface IHasKey<T>
{
    public T Id { get; set; }
}