namespace DocumentosCurriculoService.Entity
{
    public class Propriedade
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PropertyType { get; set; }
        public bool RelatedList { get; set; }
        public int NaturezaId { get; set; }
        public dynamic[] Values { get; set; }
    }
}
