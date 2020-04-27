namespace TusLibros.API
{
    public class CreateCardResponse
    {
        public bool Ok { get; }
        public string Data { get; }

        public CreateCardResponse(string apiResponse)
        {
            var splittedResponse = apiResponse.Split('|');
            Ok = splittedResponse[0] == "0";
            Data = splittedResponse[1];
        }
    }
}