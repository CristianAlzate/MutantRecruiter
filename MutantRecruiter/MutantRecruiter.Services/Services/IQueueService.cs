using MutantRecruiter.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutantRecruiter.Services.Services
{
    public interface IQueueService<T> where T : class
    {
        void QueueStack(T messageObject);
    }
}
