namespace GrpcGreeter
{
    public class User
    {
        public long id { get; set; }
        public string name { get; set; }
        public long balance { get; set; }
        public override string ToString()
        {
            return ($"id:{id} , {name} -- {balance}");
        }
    }
}