using InhlwathiTutors.Models;

namespace InhlwathiTutors.Services.Interfaces
{
    public interface IBookingService
    {
        Task<Booking> CreateBookingAsync(string studentId, BookingCreateDto dto);
    }
}
