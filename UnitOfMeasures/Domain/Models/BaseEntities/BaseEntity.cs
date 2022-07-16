namespace UnitOfMeasures.Domain.Models.BaseEntities
{
    public class BaseEntity<Key> : IBaseEntity<Key>
    {
        public Key Id { get; set; }
        public string Name { get; set; }    
        public string Code { get; set; }
    }
}
