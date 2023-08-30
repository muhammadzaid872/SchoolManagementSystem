using Presentation.Models;

namespace Presentation.Service.Interface
{
    public interface ICourseService
    {
        ResponseDTO GetCouresList();
        ResponseDTO Details(int id);
        ResponseDTO AddUpdate(CourseDTO course);
        ResponseDTO Delete(int id);

    }
}
