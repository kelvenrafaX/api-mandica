namespace Domain.Helpers
{
    public class JsonResponse<T> 
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public T Entity { get; set; }
    }
}
