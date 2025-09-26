namespace CRUD.DAL.Repositories.Abstract
{
    public interface ICanBeSoftDeletedModel
    {
        public bool IsDeleted { get; set; }
    }
}
