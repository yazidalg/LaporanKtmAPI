namespace lapora_ktm_api.Dtos.Response
{
	public class DefaultResponse<T>
	{
		public int StatusCode { get; set; }
		public string Message { get; set; }
		public T Data { get; set; }
	}
}

