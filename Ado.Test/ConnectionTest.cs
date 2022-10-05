using System;
using System.Threading.Tasks;
using AdoFrameWork.Core.Service;
using NUnit.Framework;

namespace Ado.Test;
[TestFixture]
public class ConnectionTest
{

    [Test]
    public async Task OpenConnectionFaild()
    {
        var connection = new ConnectionImpl();
        var result = await connection.BuildConnection("any queryString");
        Assert.That(false, Is.EqualTo(result));
    }
    [Test]
    public async Task OpenConnectionSuccess()
    {
        var connection = new ConnectionImpl();
        var result = await connection.BuildConnection("Server=.;Database=SportsStore;Trusted_Connection=True;");
        Assert.That(true, Is.EqualTo(result));
    }
}