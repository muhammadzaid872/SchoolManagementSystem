using Presentation.Models;

namespace Presentation.Service.Interface
{
    public interface IEnrollmentService
    {
        ResponseDTO GetEnrollmentsList();
        ResponseDTO AddUpdate(EnrollmentModel enrollment);
        ResponseDTO Delete(int id);
    }
}
