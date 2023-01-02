using System;
using System.Threading.Tasks;
using AdoFrameWork.Core.Service;
using Moq;
using NUnit.Framework;

namespace Ado.Test;
[TestFixture]
public class ConnectionTest
{

    [Test]
    public async Task OpenConnectionFaild()
    {
        var connection = new Mock<ConnectionImpl>();
        connection.Setup(x => x.BuildConnection("any queryString")).ReturnsAsync(false);
        Assert.That(false, Is.EqualTo(connection.Object.BuildConnection("any queryString").Result));
    }
    [Test]
    public async Task OpenConnectionSuccess()
    {
        var connection = new Mock<ConnectionImpl>();
        connection.Setup(x => x.BuildConnection("Server=.;Database=master;Trusted_Connection=True;")).ReturnsAsync(true);
        Assert.That(true, Is.EqualTo(connection.Object.BuildConnection("Server=.;Database=master;Trusted_Connection=True;").Result));
    }
}