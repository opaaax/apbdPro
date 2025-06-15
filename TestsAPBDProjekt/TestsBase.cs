using APBDProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace TestProject1;

public class TestsBase
{
    protected Context CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new Context(options);
    }

}