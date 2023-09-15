namespace Application.Master.Dto
{
    public class ReferenceFieldVm
    {
        public long Id { get; set; }

        public required string Title { get; set; }

        public required string ReferenceType { get; set; }

        public required string Author { get; set; }

        public required string Created { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? Modified { get; set; }

        public required string Editor { get; set; }
    }
}
