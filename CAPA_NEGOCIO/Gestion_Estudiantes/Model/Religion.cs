using APPCORE;
namespace DataBaseModel
{
    public class Religiones : EntityClass
    {
        [PrimaryKey(Identity = true)]
        public int? Id { get; set; }
        public string? Idtreligion { get; set; }
        public string? Texto { get; set; }
    }
}