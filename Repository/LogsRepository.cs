using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class LogsRepository : RepositoryBase<Log>, ILogsRepository 
    {
        public LogsRepository(RepositoryContext repositoryContext)
           : base(repositoryContext)
        {

        }

        public void AddLog(Log log)
        {
            Create(log);
        }
    }
}
