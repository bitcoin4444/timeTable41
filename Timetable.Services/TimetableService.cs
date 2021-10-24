using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TimetableDesktop.Models;
using TimetableDesktop.Models.Interfaces;

namespace TimetableDesktop.Services
{
    public class TimetableService : ITimetableService
    {
        public Task<LessonDto> AddLesson(AddLessonDto model, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }


        public Task<LessonDto> DeleteLesson(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTimetable(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IEnumerable<LessonDto>>> GetFilteredTimetable(LessonFilter lessonFilter, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<LessonDto> GetLessonById(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LessonDto>> GetTimetable(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<List<LessonDto>> InsertManyLessons(List<AddLessonDto> addLessonDtos, CancellationToken cancellationToken = default)
        {
            if (addLessonDtos.Count == 0 || addLessonDtos is null)
                throw new ArgumentException();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://k41timetablebot.herokuapp.com");
                var json = JsonConvert.SerializeObject(addLessonDtos);
                var result = await client.PostAsync("/api/Timetable/lesson/addlessonsList",
                                new StringContent(json, Encoding.UTF8, "application/json"));
                string resultContent = await result.Content.ReadAsStringAsync();
                try

                {
                    var lessons = JsonConvert.DeserializeObject<ResultDto<List<LessonDto>>>(resultContent);
                    return lessons.Value;
                }
                catch (Exception ex)
                { 
                    Console.WriteLine(ex.Message);
                    return null;
                }
            };
        }

        public Task<LessonDto> UpdateLesson(Guid lessonId, LessonDto lessonDto, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
