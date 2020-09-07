using System.Text.Json.Serialization;

namespace TrackerApplication.Contracts
{
    public enum ServiceResponseType
    {
        Unassigned,
        Succeed,
        Failed
    }

    public class ServiceResponse
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ServiceResponseType Type { get; set; }
        public string Message { get; set; }

        public static ServiceResponse CreateSucceed()
        {
            return new ServiceResponse { Type = ServiceResponseType.Succeed };
        }

        public static ServiceResponse CreateFailed(string message)
        {
            return new ServiceResponse
            {
                Type = ServiceResponseType.Failed,
                Message = message
            };
        }
    }

    public class ServiceResponse<TData> : ServiceResponse
    {
        public TData Data { get; set; }

        public static ServiceResponse<TData> CreateSucceed(TData data)
        {
            return new ServiceResponse<TData>
            {
                Type = ServiceResponseType.Succeed,
                Data = data
            };
        }

        public new static ServiceResponse<TData> CreateFailed(string message)
        {
            return new ServiceResponse<TData>
            {
                Type = ServiceResponseType.Failed,
                Message = message
            };
        }
    }
}
