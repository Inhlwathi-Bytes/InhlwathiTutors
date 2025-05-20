using InhlwathiTutors.Data;
using InhlwathiTutors.Models;
using InhlwathiTutors.Services.Interfaces;

namespace InhlwathiTutors.Services.Implementation
{
    public class BookingService : IBookingService
    {
        private readonly AppDbContext _context;

        public BookingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Booking> CreateBookingAsync(string studentId, BookingCreateDto dto)
        {
            var booking = new Booking
            {
                StudentId = studentId,
                TutorshipSubjectId = dto.TutorshipSubjectId,
                ScheduledAt = dto.ScheduledAt,
                DurationMinutes = dto.DurationMinutes,
                Note = dto.Note,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }
    }

}
