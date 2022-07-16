namespace UnitOfMeasures.Domain.Models.BaseEntities
{
    public interface IBaseEntity<Key>
    {
        Key Id { get; set; }
    }
}
