using org.huage.Admin.Configuration.Entity.Entity;

namespace org.huage.Admin.Configuration.Entity.Service;

public interface ITApilogServices
{
    Task InsertAsync(TApilog apilog);
}