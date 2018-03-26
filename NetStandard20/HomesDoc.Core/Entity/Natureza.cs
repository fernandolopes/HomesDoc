using System.Collections.Generic;

namespace HomesDoc.Core.Entity
{
    public class Natureza
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool AllowTags { get; set; }
        public bool ValidationRequired { get; set; }
        public int GrupoId { get; set; }
        public string Permission { get; set; }
        public bool AllowCircular { get; set; }
        public int ExpirationDays { get; set; }
        public bool AllowEmptyExpirationDate { get; set; }
        public IEnumerable<Propriedade> Properties { get; set; }
    }
}
