using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IService
{
    public interface IAuthorizeOwner
    {
        public bool IsOwnerJournal(int idJournal, string idUser);

        public bool IsOwnerNote(int idNote, string idUser);


    }
}
