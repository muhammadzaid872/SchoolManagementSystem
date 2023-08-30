using Presentation.Models;

namespace Presentation.Service.Interface
{
    public interface ITeacherService
    {
        ResponseDTO GetTeachersList();
        ResponseDTO Details(int id);
        ResponseDTO AddUpdate(TeacherDTO teacher);
        ResponseDTO Delete(int id);
    }
}
