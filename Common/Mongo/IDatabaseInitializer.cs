using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Mongo
{
    public interface IDatabaseInitializer
    {
        Task InitializeAsync();
    }
}
