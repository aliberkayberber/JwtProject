namespace JwtExample.Types
{
    public class ServiceMassage
    {
        public bool IsSucced { get; set; }

        public string Massage {  get; set; }
    }

    public class ServiceMessage<T>
    {
        public bool IsSucceed { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }

    }
}
