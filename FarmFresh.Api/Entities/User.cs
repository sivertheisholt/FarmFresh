namespace FarmFresh.Api.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public required string UserUUID { get; set; }
        public double Balance { get; set; }
    }
}