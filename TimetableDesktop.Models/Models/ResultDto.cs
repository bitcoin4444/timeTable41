using System.Collections.Generic;

namespace TimetableDesktop.Models
{
    public class ResultDto<TModel>
    {
        public TModel Value { get; set; }

        public List<string> Errors { get; set; }
    }
}
