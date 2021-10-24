using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TimetableDesktop.Models;
using TimetableDesktop.Models.Interfaces;

namespace TimetableDesktop.BusinesLogic
{
    public class BusinesLogicClass : IBusinessLogic
    {
        private readonly ITimetableService _timetableService;
        public BusinesLogicClass(ITimetableService timetableService)
        {
            _timetableService = timetableService;
        }

        public Task<LessonDto> AddLesson(AddLessonDto model, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateTimetableFromFile(string filePath, CancellationToken cancellationToken = default)
        {
            string groupNumber = String.Empty;
            string disciplineName = String.Empty;
            string lecturalName = String.Empty;
            DateTime lessonDate = DateTime.Now;
            int lessonInDayNumber = 0;
            string lessonType = String.Empty;
            int lessonNumber = 0;
            string auditoreNumber = String.Empty;
            List<AddLessonDto> lessons = new List<AddLessonDto>();

            using (var document = SpreadsheetDocument.Open(filePath, true))
            {
                WorkbookPart wbPart = document.WorkbookPart;
                int worksheetcount = document.WorkbookPart.Workbook.Sheets.Count();
                Sheet mysheet = (Sheet)document.WorkbookPart.Workbook.Sheets.ChildElements.GetItem(0);
                Worksheet Worksheet = ((WorksheetPart)wbPart.GetPartById(mysheet.Id)).Worksheet;
                IEnumerable<Row> Rows = Worksheet.GetFirstChild<SheetData>().Descendants<Row>();

                foreach (var row in Rows)
                {
                    if (row.RowIndex.Value != 0)
                    {
                        int idx = 1; int idy = 0;
                        foreach (Cell cell in row.Descendants<Cell>())
                        {

                            var val = GetValue(document, cell);
                            if (val == "Неделя")
                                break;
                            if (idx < 8)
                            {
                                if (idx == 4)
                                {
                                    int day, month, year;
                                    string[] z = val.Split(' ');
                                    Int32.TryParse(z[0], out day);
                                    if (z[2].Length == 4)
                                        Int32.TryParse(z[2], out year);
                                    else
                                    {
                                        Int32.TryParse(z[2].Substring(0, 3), out year);
                                    }
                                    switch (z[1])
                                    {
                                        case "СЕНТЯБРЯ":
                                            month = 09; break;

                                        case "ОКТЯБРЯ":
                                            month = 10; break;

                                        case "НОЯБРЯ":
                                            month = 11; break;

                                        case "ДЕКАБРЯ":
                                            month = 12; break;

                                        case "ЯНВАРЯ":
                                            month = 1; break;

                                        case "ФЕВРАЛЯ":
                                            month = 2; break;

                                        case "МАРТА":
                                            month = 3; break;

                                        case "АПРЕЛЯ":
                                            month = 4; break;

                                        case "МАЯ":
                                            month = 5; break;

                                        case "ИЮНЯ":
                                            month = 6; break;

                                        case "ИЮЛЯ":
                                            month = 7; break;

                                        case "АВГУСТА":
                                            month = 8; break;
                                        default:
                                            month = 0; break;

                                    }
                                    DateTime dateTime = new DateTime(year, month, day);
                                    lessonDate = dateTime;
                                }
                                if (idx == 5)
                                {
                                    int x;
                                    Int32.TryParse(val, out x);
                                    lessonInDayNumber = x;
                                }
                                if (idx == 7)
                                {
                                    int x;
                                    Int32.TryParse(val, out x);
                                    if (x != 4)
                                        break;
                                }
                            }
                            else
                            {

                                idy++;
                                if (idy == 1)
                                {
                                    groupNumber = val;
                                }
                                if (idy == 2)
                                {
                                    disciplineName = val;
                                }

                                if (idy == 3)
                                {
                                    int x = 0;
                                    if (val != "" && val != "ОХРАНА" && val != "ПРАЗДНИК")
                                    {
                                        string[] z = val.Split(' ');
                                        lessonType = z[0];
                                        Int32.TryParse(z[z.Length - 1], out x);
                                        lessonNumber = x;
                                    }
                                }
                                if (idy == 4)
                                {
                                    lecturalName = val;
                                }
                                if (idy == 5)
                                {
                                    if (val != null)
                                        auditoreNumber = val;
                                    else
                                        auditoreNumber = "";
                                }
                                if (idy == 7)
                                {
                                    idy = 0;
                                    if (groupNumber == "434" || groupNumber == "435Г" ||
                                        groupNumber == "442" || groupNumber == "443")
                                    {

                                        if (lecturalName != "" && disciplineName != "")
                                            try
                                            {

                                                lessons.Add(new AddLessonDto()
                                                {
                                                    AuditoreNumber = auditoreNumber,
                                                    DisciplineName = disciplineName,
                                                    GroupNumber = groupNumber,
                                                    LecturalName = lecturalName,
                                                    LessonDate = lessonDate,
                                                    LessonInDayNumber = lessonInDayNumber,
                                                    LessonNumber = lessonNumber,
                                                    LessonType = lessonType
                                                });


                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine(ex.Message);
                                            }
                                    }
                                }
                            }
                            idx++;
                        }
                    }
                }
            }
            try
            {
                var cnt = await _timetableService.InsertManyLessons(lessons);
                return cnt.Count();
            }
            catch (Exception xe)
            {
                Console.WriteLine(xe.Message);
                return 0;
            }
        }

        public Task<LessonDto> DeleteLesson(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IEnumerable<LessonDto>>> GetFilteredTimetable(LessonFilter lessonFilter, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<LessonDto> UpdateLesson(Guid lessonId, LessonDto lessonDto, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        private static string GetValue(SpreadsheetDocument doc, Cell cell)
        {
            string value = cell.CellValue.InnerText;
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText;
            }
            return value;
        }
    }
}
