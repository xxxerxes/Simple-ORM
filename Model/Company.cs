using Framework.Mapping;

namespace Model;

[Table("Company")]
public class Company : BaseModel
{
    [Column("Name")]
    public string Name { get; set; }

    public DateTime CreateTime { get; set; }

    public int CreatorId { get; set; }

    public int? LastModifierId { get; set; }

    public DateTime? LastModifyTime { get; set; }
}

