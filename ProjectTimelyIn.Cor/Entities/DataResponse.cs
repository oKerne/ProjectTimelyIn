namespace ProjectTimelyIn.Core.Entities
{
    public class DataResponse<T>
    {
        public int StatusCode { get; set; }
        public string ErroMsg { get; set; }
        public T Data { get; set; }
        public DataResponse(int success, string message, T data)
        {
            StatusCode = success;
            ErroMsg = message;
            Data = data;
        }

        public DataResponse()
        {
        }

    }
}
