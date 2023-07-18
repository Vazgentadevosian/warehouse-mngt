namespace WRMNGT.Infrastructure.Models.Response.Errors
{
    public class ErrorModel
    {
        public ErrorModel() { }

        public ErrorModel(int code, string title)
        {
            Code = code;
            Title = title;
        }

        public int Code { get; set; }

        public string Title { get; set; }
    }
}
