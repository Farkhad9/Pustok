namespace PustokApp.Models
{
    public abstract class BaseEntity
    {
        public Guid Id { get; init; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}
