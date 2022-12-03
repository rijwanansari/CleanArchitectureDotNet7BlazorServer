namespace Application.Master.Dto
{
    public class ReferenceFieldVm
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ReferenceType { get; set; }
        public string Author { get; set; }
        public string Created { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? Modified { get; set; }
        public string Editor { get; set; }
    }
}
