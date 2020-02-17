using System;
using System.Collections.Generic;
using System.Text;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            this.Type = Enums.GradeBookType.Ranked;
        }

        /// <summary>
        /// The first 20% with highest grade get A
        /// The next 20% get B and so on until F
        /// As we need to consider 20% of total , we need
        /// atleast 5 students to grade 
        /// </summary>
        /// <param name="averageGrade"></param>
        /// <returns></returns>
        public override char GetLetterGrade(double averageGrade)
        {
            char grade = 'F';
            if (Students.Count < 5)
            {
                throw new InvalidOperationException("For ranked grading the total number of students needs to be atleast 5");
            }
            int studentsPerGrade = Convert.ToInt32(Math.Round(Convert.ToDouble(((Students.Count * 20) / 100))));
            
            //Sort students by average grade
            Students.Sort(delegate (Student x,Student y)
            {
                return x.AverageGrade.CompareTo(y.AverageGrade);
            });

            Students.Reverse();//Desending by Average grade
            
            for (int i = 0; i < Students.Count; i++)
            {
                if (Students[i].AverageGrade <= averageGrade)
                {
                    if (i == 0)
                    {
                        grade = 'A';
                        break;
                    }
                    else
                    {
                        if (Students[i - 1].AverageGrade > averageGrade)
                        {
                            int position = i+1;
                            int gradeRank = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal((position / studentsPerGrade))));
                            switch (gradeRank)
                            {
                                case 1:
                                    grade = 'A';
                                    break;
                                case 2:
                                    grade = 'B';
                                    break;
                                case 3:
                                    grade = 'C';
                                    break;
                                case 4:
                                    grade = 'D';
                                    break;
                                default:
                                    grade = 'F';
                                    break;



                            }
                        }
                    }
                }
            }


            return grade;
;            
        }

    }
}
