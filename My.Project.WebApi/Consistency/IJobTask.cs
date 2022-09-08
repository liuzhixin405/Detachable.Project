using System.Threading.Tasks;

namespace My.Project.WebApi.Consistency
{
    public interface IJobTask
    {
        Task Invoke();
        string Cron { get; }
    }
}
