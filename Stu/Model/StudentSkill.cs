using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stu.Model
{
    public class StudentSkill
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int SkillGroupId { get; set; }
        public SkillGroup SkillGroup { get; set; }
    }

}
