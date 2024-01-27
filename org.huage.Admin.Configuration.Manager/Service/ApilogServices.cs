using org.huage.Admin.Configuration.Entity.Entity;
using org.huage.Admin.Configuration.Entity.Service;

namespace org.huage.Admin.Configuration.Manager.Service;

public class ApilogServices : ITApilogServices
{
    /**
     * 存入mongo
     */
    public async Task InsertAsync(TApilog apilog)
    {
        await Task.Delay(1000);
    }
}