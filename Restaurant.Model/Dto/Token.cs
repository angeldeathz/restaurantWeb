namespace Restaurant.Model.Dto
{
    public class Token
    {
        public Token() 
        {
            access_token = string.Empty;
            token_type = string.Empty;
            expires_in = 0;
        }
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }
}
