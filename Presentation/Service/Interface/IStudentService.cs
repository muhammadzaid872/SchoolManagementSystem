using Presentation.Models;

namespace Presentation.Service.Interface
{
    public interface IStudentService
    {
        ResponseDTO GetStudentsList();
        ResponseDTO Details(int id);
        ResponseDTO AddUpdate(StudentDTO student);
        ResponseDTO Delete(int id);
    }
}
