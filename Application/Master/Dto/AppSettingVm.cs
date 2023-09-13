namespace Application.Master.Dto
{
    public class AppSettingVm
    {
        public int Id { get; set; }

        public string ReferenceKey { get; set; } = string.Empty;

        public string Value { get; set; } = string.Empty;

        public string? Description { get; set; }
        
        public string Type { get; set; } = string.Empty;
    }
}
