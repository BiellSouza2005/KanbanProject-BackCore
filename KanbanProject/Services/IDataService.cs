using Microsoft.Data.SqlClient;

namespace KanbanProject.Services
{
    public interface IDataService
    {
        public string ConnectionString { get; set; }

        public SqlDataReader ReturnDataReader();
    }
}
