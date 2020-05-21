namespace GrpcGreeter
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public int balance { get; set; }
        public override string ToString()
        {
            return ($"id:{id} , {name} -- {balance}");
        }
    }
}