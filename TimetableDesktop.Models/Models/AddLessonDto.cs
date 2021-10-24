using System;

namespace TimetableDesktop.Models
{
    public class AddLessonDto
    {
        
        public int LessonNumber { get; set; }
        public string GroupNumber { get; set; }
        public int LessonInDayNumber { get; set; }
        public string DisciplineName { get; set; }
        public string LessonType { get; set; }
        public string LecturalName { get; set; }
        public DateTime LessonDate { get; set; }
        public string AuditoreNumber { get; set; }
    }
}