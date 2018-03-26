namespace HomesDoc.Core.Entity
{
    public class Grupo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public dynamic[] Permissions { get; set; }
    }
}
