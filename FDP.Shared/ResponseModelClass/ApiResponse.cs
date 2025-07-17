namespace FDP.Shared
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }           
        public string Message { get; set; } = "";     
        public T? Data { get; set; }                  
        public bool Success => StatusCode is >= 200 and < 300; 
    }

}
