using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Models;

namespace IService
{
    public interface IComment : ILeafRepository<Comment>
    {
    }
}
