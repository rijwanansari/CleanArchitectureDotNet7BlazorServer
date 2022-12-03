namespace Domain.Master
{
    public class ReferenceField : AuditableWithBaseEntity<long>
    {
        public string Title { get; set; }
        public string ReferenceType { get; set; }

    }
}
