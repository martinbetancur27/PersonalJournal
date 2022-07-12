using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IService
{
    public interface IPost
    {
        /*private int Id { get; set; }
        private DateTime CreateDate { get; set; }
        private string Message { get => return Message;  }
        private int IdUserCreator { get;  }*/
        //public int PostRoot { get; set; }

        //public bool Create(IPost post); para mi no existe

        public bool DeletePost(int? idPost);
        

    }
}