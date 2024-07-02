namespace NadinSoft.Domain.Entities
{
    public interface IBaseEntity<Tkey>
    {
        Tkey Id { get; set; }

        DateTime CreatedAt { get; set; }
        DateTime? ModifiedAt{ get; set; }
        DateTime? RemovedAt{ get; set; }
    }
}
