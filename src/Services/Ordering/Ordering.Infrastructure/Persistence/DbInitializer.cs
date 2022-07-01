using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence;
public class DbInitializer
{
    private readonly ModelBuilder _modelBuilder;

    public DbInitializer(ModelBuilder modelBuilder)
    {
        this._modelBuilder = modelBuilder;
    }

}
