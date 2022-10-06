
namespace IService
{
    public interface IAuthorizeOwner
    {
        public bool IsOwnerJournal(int idJournal, string idUser);
        public bool IsOwnerNote(int idNote, string idUser);
    }
}
