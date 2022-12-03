namespace Application.Common.Model
{
    public class ResponseModel
    {
        internal ResponseModel(bool success, string message, dynamic output)
        {
            Success = success;
            Message = message;
            Output = output;
        }
        internal ResponseModel(bool success, string message)
        {
            Success = success;
            Message = message;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public dynamic? Output { get; set; }

        public static ResponseModel SuccessResponse(string message, dynamic output)
        {
            return new ResponseModel(true, message, output);
        }
        public static ResponseModel FailureResponse(string message)
        {
            return new ResponseModel(false, message);
        }

    }
}
