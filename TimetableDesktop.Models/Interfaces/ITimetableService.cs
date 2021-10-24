using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TimetableDesktop.Models.Interfaces
{
    public interface ITimetableService
    {
        Task<LessonDto> AddLesson(AddLessonDto model, CancellationToken cancellationToken = default);       
        Task<LessonDto> GetLessonById(Guid id, CancellationToken cancellationToken = default);
        Task<LessonDto> DeleteLesson(Guid id, CancellationToken cancellationToken = default);
        Task DeleteTimetable(CancellationToken cancellationToken = default);
        Task<IEnumerable<IEnumerable<LessonDto>>> GetFilteredTimetable(LessonFilter lessonFilter, CancellationToken cancellationToken = default);
       
        Task<LessonDto> UpdateLesson(Guid lessonId, LessonDto lessonDto, CancellationToken cancellationToken = default);
        Task<List<LessonDto>> InsertManyLessons(List<AddLessonDto> lessonDtos, CancellationToken cancellationToken = default);
    }
}
