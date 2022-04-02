using Framework.Mapping;
using Framework.Validate;

namespace Model;

[Table("Company")]
public class Company : BaseModel
{
    [NoNull]
    [Column("Name")]
    [Length(1,4)]
    public string Name { get; set; }

    public DateTime CreateTime { get; set; }

    [IntValue(1,2)]
    public int CreatorId { get; set; }

    public int? LastModifierId { get; set; }

    public DateTime? LastModifyTime { get; set; }
}

