using System.ComponentModel.DataAnnotations.Schema;

namespace Restauracja.Services.ProductApi.Model;

public abstract class BaseEntity
{
    [Column(nameof(Id), Order = 0)]
    public virtual long Id { get; set; }
}
