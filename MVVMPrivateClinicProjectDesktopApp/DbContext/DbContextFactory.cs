using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MVVMPrivateClinicProjectDesktopApp.DbContext;

public class DbContextFactory(string connectionString) {
    public PrivateClinicContext CreateDbContext() {
        var options = new DbContextOptionsBuilder<PrivateClinicContext>()
            .UseSqlServer(connectionString)
            .Options;
        
        return new PrivateClinicContext(options);
    }
}