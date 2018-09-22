namespace OrganizacaoTrabalho1.Models
{
    public sealed class DataRegistry : IRegistry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; }
    }
}
