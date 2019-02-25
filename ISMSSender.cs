using System.Threading.Tasks;

namespace SMSSender
{
    public interface ISMSSender
    {
        Task SendAsync(string destination, string message);
    }
}