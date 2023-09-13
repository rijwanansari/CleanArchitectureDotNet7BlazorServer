namespace Domain.Master
{
    public class ReferenceField : AuditableWithBaseEntity<long>
    {
        public string Title { get; set; } = null!;
        
        public string ReferenceType { get; set; } = null!;

    }
}
