using System.Threading.Tasks;

namespace Detachable.Project.WebApi.Consistency
{
    public interface IJobTask
    {
        Task Invoke();
        string Cron { get; }
    }
}
